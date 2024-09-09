using HinduTempleofTriStates.Data;
using HinduTempleofTriStates.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HinduTempleofTriStates.Services
{
    public class LedgerService
    {
        private readonly ApplicationDbContext _context;

        public LedgerService(ApplicationDbContext context)
        {
            _context = context;
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
        }

        internal void DeleteAccount(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
