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

        public async Task<LedgerAccount> GetAccountByIdAsync(Guid id)
        {
            var account = await _context.LedgerAccounts.FindAsync(id);
            return account ?? throw new KeyNotFoundException("Account not found"); // Ensure non-nullable return
        }

        public async Task<IEnumerable<LedgerAccount>> GetAllAccountsAsync()
        {
            return await _context.LedgerAccounts.ToListAsync();
        }

        public async Task AddAccountAsync(LedgerAccount account)
        {
            _context.LedgerAccounts.Add(account);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAccountAsync(LedgerAccount account)
        {
            _context.LedgerAccounts.Update(account);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAccountAsync(Guid id)
        {
            var account = await _context.LedgerAccounts.FindAsync(id);
            if (account != null)
            {
                _context.LedgerAccounts.Remove(account);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByAccountIdAsync(Guid accountId)
        {
            return await _context.Transactions
                .Where(t => t.AccountId == accountId)
                .ToListAsync();
        }

        public async Task AddTransactionAsync(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
        }
    }
}
