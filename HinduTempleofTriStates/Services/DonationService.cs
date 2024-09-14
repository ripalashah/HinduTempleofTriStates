using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using HinduTempleofTriStates.Data;
using HinduTempleofTriStates.Models;
using HinduTempleofTriStates.Repositories;

namespace HinduTempleofTriStates.Services
{
    public class DonationService : IDonationService
    {
        private readonly ApplicationDbContext _context;
        private readonly IDonationRepository _donationRepository;
        private readonly ILogger<DonationService> _logger;

        public DonationService(ApplicationDbContext context, IDonationRepository donationRepository, ILogger<DonationService> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _donationRepository = donationRepository ?? throw new ArgumentNullException(nameof(donationRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // Method to update the ledger account balance
        public async Task UpdateLedgerAccountBalanceAsync(Guid? ledgerAccountId, double amount, bool isAddition = true)
        {
            if (ledgerAccountId.HasValue)
            {
                var ledgerAccount = await _context.LedgerAccounts.FindAsync(ledgerAccountId.Value);
                if (ledgerAccount != null)
                {
                    ledgerAccount.Balance += isAddition ? Convert.ToDecimal(amount) : -Convert.ToDecimal(amount);
                    _context.LedgerAccounts.Update(ledgerAccount);
                    await _context.SaveChangesAsync();

                    _logger.LogInformation("Ledger account {LedgerAccountId} balance updated by {Amount}.", ledgerAccountId, amount);
                }
                else
                {
                    _logger.LogWarning("LedgerAccount with ID {LedgerAccountId} not found.", ledgerAccountId.Value);
                }
            }
            else
            {
                _logger.LogWarning("LedgerAccountId is null.");
            }
        }

        // Method to add a general ledger entry for a donation
        public async Task AddGeneralLedgerEntryForDonationAsync(Donation donation, bool isAddition = true)
        {
            var ledgerAccount = await _context.LedgerAccounts.FindAsync(donation.LedgerAccountId);
            if (ledgerAccount == null)
            {
                throw new InvalidOperationException("LedgerAccount cannot be null.");
            }

            if (donation.LedgerAccountId != Guid.Empty)
            {
                if (!donation.LedgerAccountId.HasValue)
                {
                    // Handle the null case, for example, throw an exception or return an error
                    throw new InvalidOperationException("LedgerAccountId cannot be null when creating a GeneralLedgerEntry.");
                }
                var ledgerEntry = new GeneralLedgerEntry
                {
                    Id = Guid.NewGuid(),
                    LedgerAccountId = donation.LedgerAccountId.Value,
                    Date = donation.Date,
                    Description = $"Donation from {donation.DonorName}",
                    Credit = isAddition ? Convert.ToDecimal(donation.Amount) : 0,
                    Debit = isAddition ? 0 : Convert.ToDecimal(donation.Amount),
                    LedgerAccount = ledgerAccount
                };

                _context.GeneralLedgerEntries.Add(ledgerEntry);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("LedgerAccountId is required.");
            }
        }

        // Method to add a cash transaction for a donation
        public async Task AddCashTransactionForDonationAsync(Donation donation, bool isAddition)
        {
            _logger.LogInformation("Attempting to add cash transaction for donation with ID {DonationId}", donation.Id);

            if (donation.DonationType == "Cash" && donation.LedgerAccountId != Guid.Empty)
            {
                _logger.LogInformation("Donation is of type 'Cash' and LedgerAccountId is {LedgerAccountId}", donation.LedgerAccountId);

                var cashTransaction = new CashTransaction
                {
                    Id = Guid.NewGuid(),
                    Date = donation.Date,
                    Description = $"Donation from {donation.DonorName}",
                    Amount = Convert.ToDecimal(donation.Amount),
                    Type = isAddition ? CashTransactionType.Credit : CashTransactionType.Debit,
                    LedgerAccountId = donation.LedgerAccountId ?? Guid.Empty,
                    DonationId = donation.Id
                };

                _context.CashTransactions.Add(cashTransaction);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Cash transaction successfully added for donation with ID {DonationId}", donation.Id);
            }
            else
            {
                _logger.LogWarning("Donation with ID {DonationId} is not of type 'Cash' or does not have a valid LedgerAccountId", donation.Id);
            }
        }

        // Method to add a donation with related entries (transaction and ledger)
        public async Task<bool> AddDonationAsync(Donation donation)
        {
            // Call the overloaded version with a default value for isAddition (e.g., true)
            return await AddDonationAsync(donation, true);
        }

        public async Task<bool> AddDonationAsync(Donation donation, bool isAddition)
        {
            if (ValidateDonation(donation))
            {
                using (var transaction = await _context.Database.BeginTransactionAsync()) // Begin transaction for atomicity
                {
                    try
                    {
                        // Add donation to the database
                        await _donationRepository.AddDonationAsync(donation);

                        // Create a new Transaction for the donation
                        var accountId = donation.LedgerAccountId;
                        var cashTransaction = new CashTransaction
                        {
                            Id = Guid.NewGuid(),
                            DonationId = donation.Id,
                            LedgerAccountId = donation.LedgerAccountId ?? Guid.Empty,
                            AccountId = accountId ?? Guid.Empty,
                            Amount = (decimal)donation.Amount,
                            Date = DateTime.UtcNow,
                            Description = $"Donation by {donation.DonorName}",
                            TransactionType = isAddition ? TransactionType.Credit : TransactionType.Debit, // Use isAddition here
                            CreatedBy = "System",
                            CreatedAt = DateTime.UtcNow
                        };

                        // Add the transaction to the database
                        _context.CashTransactions.Add(cashTransaction);
                        await _context.SaveChangesAsync();

                        // Add to General Ledger Entry
                        await AddGeneralLedgerEntryForDonationAsync(donation, isAddition);

                        // Update the Cash Transaction
                        await AddCashTransactionForDonationAsync(donation, isAddition);

                        // Commit transaction to ensure all changes are saved
                        await transaction.CommitAsync();

                        return true;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error adding donation");
                        await transaction.RollbackAsync(); // Rollback on failure
                    }
                }
            }

            return false;
        }

        public async Task<IEnumerable<Donation>> GetDonationsByLedgerAccountIdAsync(Guid ledgerAccountId)
        {
            return await _context.Donations
                .Where(d => d.LedgerAccountId == ledgerAccountId && !d.IsDeleted) // Assuming IsDeleted is a soft-delete mechanism
                .ToListAsync();
        }

        // Method to delete a donation and associated ledger and cash entries
        public async Task<bool> DeleteDonationAsync(Guid id)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var donation = await _context.Donations.FindAsync(id);
                    if (donation == null)
                    {
                        _logger.LogWarning("Donation with ID {DonationId} not found.", id);
                        return false;
                    }

                    // Reverse ledger account balance
                    await UpdateLedgerAccountBalanceAsync(donation.LedgerAccountId, donation.Amount, false);
                    _logger.LogInformation("Ledger account balance reversed for donation {DonationId}", donation.Id);

                    // Reverse the ledger entry
                    await AddGeneralLedgerEntryForDonationAsync(donation, false);
                    _logger.LogInformation("General ledger entry reversed for donation {DonationId}", donation.Id);

                    // Reverse cash transaction if it's a cash donation
                    if (donation.DonationType == "Cash")
                    {
                        await AddCashTransactionForDonationAsync(donation, false);
                        _logger.LogInformation("Cash transaction reversed for donation {DonationId}", donation.Id);
                    }

                    // Remove the donation
                    _context.Donations.Remove(donation);
                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();
                    _logger.LogInformation("Donation with ID {DonationId} deleted successfully.", donation.Id);

                    return true;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error deleting donation with ID {DonationId}.", id);
                    await transaction.RollbackAsync();
                    return false;
                }
            }
        }

        // Method to get donations
        public async Task<List<Donation>> GetDonationsAsync()
        {
            return await _context.Donations.ToListAsync();
        }

        public async Task<Donation?> GetDonationByIdAsync(Guid id)
        {
            return await _context.Donations.FindAsync(id);
        }

        // Method to update a donation
        public async Task<bool> UpdateDonationAsync(Donation donation)
        {
            try
            {
                _context.Donations.Update(donation);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating donation.");
                return false;
            }
        }

        // Validation for donation
        private bool ValidateDonation(Donation donation)
        {
            if (donation == null ||
                string.IsNullOrWhiteSpace(donation.DonorName) ||
                donation.Amount <= 0 ||
                donation.Date == DateTime.MinValue ||
                donation.LedgerAccountId == Guid.Empty)
            {
                _logger.LogWarning("Invalid donation data: {DonationData}", donation);
                return false;
            }

            return true;
        }
    }
}
