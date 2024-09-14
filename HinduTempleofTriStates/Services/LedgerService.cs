using HinduTempleofTriStates.Data;
using HinduTempleofTriStates.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HinduTempleofTriStates.Services
{
    public class LedgerService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<LedgerService> _logger;

        public LedgerService(ApplicationDbContext context, ILogger<LedgerService> logger)
        {
            _context = context;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger)); // Inject logger
        }

        public async Task<IEnumerable<LedgerAccount>> GetAllAccountsAsync()
        {
            return await _context.LedgerAccounts
                .Where(l => !l.IsDeleted)  // Exclude soft-deleted records
                .ToListAsync();
        }

        public async Task<LedgerAccount?> GetLedgerAccountByIdAsync(Guid id)
        {
            return await _context.LedgerAccounts
                .Where(l => l.Id == id && !l.IsDeleted)  // Exclude soft-deleted records
                .FirstOrDefaultAsync();
        }

        public async Task AddAccountAsync(LedgerAccount account)
        {
            _context.LedgerAccounts.Add(account);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateLedgerAccountAsync(LedgerAccount account)
        {
            _context.Entry(account).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        // Soft delete the ledger account
        public async Task SoftDeleteLedgerAccountAsync(Guid id)
        {
            var account = await _context.LedgerAccounts.FindAsync(id);
            if (account != null)
            {
                // Soft delete associated donations
                var donations = await _context.Donations
                    .Where(d => d.LedgerAccountId == id && !d.IsDeleted)
                    .ToListAsync();

                foreach (var donation in donations)
                {
                    donation.IsDeleted = true;
                    _context.Donations.Update(donation);
                }

                // Soft delete the ledger account
                account.IsDeleted = true;
                _context.LedgerAccounts.Update(account);

                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Donation>> GetDonationsByAccountIdAsync(Guid ledgerAccountId)
        {
            return await _context.Donations
                .Where(d => d.LedgerAccountId == ledgerAccountId && !d.IsDeleted)  // Exclude soft-deleted records
                .ToListAsync();
        }

        // Delete associated donations manually
        public async Task DeleteAssociatedDonationsAsync(Guid ledgerAccountId)
        {
            var donations = await _context.Donations
                .Where(d => d.LedgerAccountId == ledgerAccountId)
                .ToListAsync();

            if (donations.Any())
            {
                _context.Donations.RemoveRange(donations);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByAccountIdAsync(Guid id)
        {
            return await _context.Transactions
                .Where(t => t.LedgerAccountId == id)
                .ToListAsync();
        }

        public async Task AddTransactionAsync(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            // Update the ledger account balance
            if (transaction.LedgerAccountId.HasValue)
            {
                await UpdateLedgerBalance(transaction.LedgerAccountId.Value);
            }
        }

        public async Task UpdateTransactionAsync(Transaction transaction)
        {
            _context.Transactions.Update(transaction);
            await _context.SaveChangesAsync();

            // Update the ledger account balance after updating the transaction
            if (transaction.LedgerAccountId.HasValue)
            {
                await UpdateLedgerBalance(transaction.LedgerAccountId.Value);
            }
        }

        public async Task AddDonationAsync(Donation donation)
        {
            _context.Donations.Add(donation);
            await _context.SaveChangesAsync();

            // Update the ledger account balance
            if (donation.LedgerAccountId.HasValue)
            {
                await UpdateLedgerBalance(donation.LedgerAccountId.Value);
            }
        }

        public async Task DeleteTransactionAsync(Guid transactionId)
        {
            var transaction = await _context.Transactions.FindAsync(transactionId);
            if (transaction != null)
            {
                _context.Transactions.Remove(transaction);
                await _context.SaveChangesAsync();

                // Update the ledger account balance after deleting the transaction
                if (transaction.LedgerAccountId.HasValue)
                {
                    await UpdateLedgerBalance(transaction.LedgerAccountId.Value);
                }
            }
        }

        private async Task UpdateLedgerBalance(Guid ledgerAccountId)
        {
            var ledgerAccount = await _context.LedgerAccounts.FindAsync(ledgerAccountId);
            if (ledgerAccount == null)
            {
                throw new KeyNotFoundException($"Ledger account with ID {ledgerAccountId} not found.");
            }

            // Calculate debit and credit totals
            var debitTotal = await _context.Transactions
                .Where(t => t.LedgerAccountId == ledgerAccountId)
                .SumAsync(t => t.Debit);

            var creditTotal = await _context.Transactions
                .Where(t => t.LedgerAccountId == ledgerAccountId)
                .SumAsync(t => t.Credit);

            // Log debit and credit totals
            _logger.LogInformation($"Updating ledger balance for LedgerAccount ID {ledgerAccountId}: DebitTotal = {debitTotal}, CreditTotal = {creditTotal}");

            // Update ledger balance
            ledgerAccount.Balance = debitTotal - creditTotal;

            // Log the updated balance
            _logger.LogInformation($"New balance for LedgerAccount ID {ledgerAccountId}: {ledgerAccount.Balance}");

            // Save changes
            _context.LedgerAccounts.Update(ledgerAccount);
            await _context.SaveChangesAsync();
        }

        internal void DeleteAccount(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
