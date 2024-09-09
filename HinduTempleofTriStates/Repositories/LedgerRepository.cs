using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HinduTempleofTriStates.Data;
using HinduTempleofTriStates.Models;

namespace HinduTempleofTriStates.Repositories
{
    public class LedgerRepository : ILedgerRepository
    {
        private readonly ApplicationDbContext _context;

        public LedgerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get account by id (ensure you check the IsDeleted flag)
        public async Task<LedgerAccount> GetAccountByIdAsync(Guid id)
        {
            var account = await _context.LedgerAccounts
                .Where(a => a.Id == id && !a.IsDeleted)  // Ensure to filter out soft-deleted accounts
                .FirstOrDefaultAsync();

            if (account == null)
            {
                throw new KeyNotFoundException("Account not found");
            }

            return account;
        }

        // Get all accounts (filter out soft-deleted records)
        public async Task<IEnumerable<LedgerAccount>> GetAllAccountsAsync()
        {
            return await _context.LedgerAccounts
                .Where(a => !a.IsDeleted)  // Only fetch accounts that are not soft deleted
                .ToListAsync();
        }

        // Add a new account
        public async Task AddAccountAsync(LedgerAccount account)
        {
            _context.LedgerAccounts.Add(account);
            await _context.SaveChangesAsync();
        }

        // Update an account
        public async Task UpdateAccountAsync(LedgerAccount account)
        {
            _context.LedgerAccounts.Update(account);
            await _context.SaveChangesAsync();
        }

        // Soft delete an account (mark IsDeleted as true)
        public async Task SoftDeleteLedgerAccountAsync(Guid id)
        {
            var ledgerAccount = await _context.LedgerAccounts.FindAsync(id);
            if (ledgerAccount != null)
            {
                ledgerAccount.IsDeleted = true;  // Set the IsDeleted flag to true
                _context.LedgerAccounts.Update(ledgerAccount);
                await _context.SaveChangesAsync(); // Save the changes
            }
        }

        // Get all transactions by account id (no change needed)
        public async Task<IEnumerable<Transaction>> GetTransactionsByAccountIdAsync(Guid accountId)
        {
            return await _context.Transactions
                .Where(t => t.LedgerAccountId == accountId)
                .ToListAsync();
        }

        // Add a transaction
        public async Task AddTransactionAsync(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
        }

        // Helper method to get all ledger accounts (filter out soft-deleted records)
        public async Task<IList<LedgerAccount>> GetAllLedgerAccountsAsync()
        {
            return await _context.LedgerAccounts
                .Where(la => !la.IsDeleted)  // Exclude soft-deleted ledger accounts
                .ToListAsync();
        }
    }
}
