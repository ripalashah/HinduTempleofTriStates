using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using HinduTempleofTriStates.Data;
using HinduTempleofTriStates.Models;
using HinduTempleofTriStates.Repositories;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics;

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

        public async Task<bool> AddDonationAsync(Donation donation, bool isAddition)
        {
            // Validate the donation object
            if (!ValidateDonation(donation))
            {
                _logger.LogWarning("Donation validation failed.");
                return false;
            }

            // Begin a database transaction
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Check if donation already exists within the transaction
                    var existingDonation = await _context.Donations
                        .OrderBy(d => d.Date) // Ensure consistent retrieval of the record
                        .FirstOrDefaultAsync(d => d.Id == donation.Id);

                    if (existingDonation == null)
                    {
                        // Add the donation to the context
                        _context.Donations.Add(donation);
                        _logger.LogInformation("New donation added: {DonationId}", donation.Id);
                    }
                    else
                    {
                        _logger.LogWarning("Duplicate donation detected: {DonationId}", donation.Id);
                        return false; // Exit early to prevent further processing
                    }

                    // Check if a GeneralLedgerEntry with this DonationId already exists within the transaction
                    var existingLedgerEntry = await _context.GeneralLedgerEntries
                        .FirstOrDefaultAsync(le => le.DonationId == donation.Id);

                    if (existingLedgerEntry == null)
                    {
                        // Create the GeneralLedgerEntry associated with the donation
                        var ledgerEntry = new GeneralLedgerEntry
                        {
                            Id = Guid.NewGuid(),
                            DonationId = donation.Id,
                            Date = DateTime.UtcNow,
                            Description = $"Donation entry from {donation.DonorName}",
                            Debit = 0,
                            Credit = (decimal)donation.Amount,
                            LedgerAccountId = donation.LedgerAccountId ?? Guid.Empty,
                        };

                        // Add the ledger entry to the context
                        _context.GeneralLedgerEntries.Add(ledgerEntry);
                        _logger.LogInformation("New ledger entry added: {LedgerEntryId}", ledgerEntry.Id);
                    }
                    else
                    {
                        _logger.LogWarning("Duplicate ledger entry detected: {DonationId}", donation.Id);
                    }

                    // Optionally create a CashTransaction if needed and if it doesn't already exist
                    if (isAddition)
                    {
                        var existingCashTransaction = await _context.CashTransactions
                            .FirstOrDefaultAsync(ct => ct.DonationId == donation.Id);

                        if (existingCashTransaction == null)
                        {
                            var cashTransaction = new CashTransaction
                            {
                                Id = Guid.NewGuid(),
                                DonationId = donation.Id,
                                Amount = (decimal)donation.Amount,
                                Date = DateTime.UtcNow,
                                Description = $"Donation from {donation.DonorName}",
                                LedgerAccountId = donation.LedgerAccountId ?? Guid.Empty,
                                TransactionType = TransactionType.Credit, // Ensure the donation is treated as a credit
                            };

                            // Add the cash transaction to the context
                            _context.CashTransactions.Add(cashTransaction);
                            _logger.LogInformation("New cash transaction added: {CashTransactionId}", cashTransaction.Id);
                        }
                        else
                        {
                            _logger.LogWarning("Duplicate cash transaction detected: {DonationId}", donation.Id);
                        }
                    }

                    // Save all changes to the database
                    await _context.SaveChangesAsync();

                    // Commit the transaction
                    await transaction.CommitAsync();

                    _logger.LogInformation("Donation and related entities were added successfully.");
                    return true;
                }
                catch (Exception ex)
                {
                    // Rollback the transaction in case of an error
                    await transaction.RollbackAsync();
                    _logger.LogError(ex, "Error creating donation and related entities");
                    return false;
                }
            }
        }



        // Check if a cash transaction already exists for a donation to prevent duplicates
        private async Task<bool> CashTransactionExistsForDonation(Guid donationId)
        {
            return await _context.CashTransactions.AnyAsync(ct => ct.DonationId == donationId);
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

        // Method to add a general ledger entry for a donation
        // Method to add a general ledger entry for a donation
        public async Task AddGeneralLedgerEntryForDonationAsync(Donation donation, bool isAddition = true)
        {
            if (!donation.LedgerAccountId.HasValue)
            {
                _logger.LogWarning("Donation's LedgerAccountId is null. Unable to create a General Ledger Entry.");
                throw new InvalidOperationException("LedgerAccountId cannot be null when creating a GeneralLedgerEntry.");
            }

            var ledgerAccount = await _context.LedgerAccounts.FindAsync(donation.LedgerAccountId.Value);
            if (ledgerAccount == null)
            {
                _logger.LogWarning("LedgerAccount with ID {LedgerAccountId} not found.", donation.LedgerAccountId.Value);
                throw new InvalidOperationException("LedgerAccount cannot be null.");
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

            _logger.LogInformation("General ledger entry created for donation with ID {DonationId}", donation.Id);
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

        // Method to validate donation data
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

        // Additional methods for CRUD operations
        public async Task<IEnumerable<Donation>> GetDonationsByLedgerAccountIdAsync(Guid ledgerAccountId)
        {
            return await _context.Donations
                .Where(d => d.LedgerAccountId == ledgerAccountId && !d.IsDeleted)
                .ToListAsync();
        }

        public async Task<Donation?> GetDonationByIdAsync(Guid id)
        {
            return await _context.Donations.FindAsync(id);
        }

        public async Task<List<Donation>> GetDonationsAsync()
        {
            return await _context.Donations.ToListAsync();
        }

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
    }
}
