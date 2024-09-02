using HinduTempleofTriStates.Data;
using HinduTempleofTriStates.Models;
using Microsoft.EntityFrameworkCore;

namespace HinduTempleofTriStates.Repositories
{
    public class LedgerRepository : ILedgerRepository
    {
        private readonly ApplicationDbContext _context;

        public LedgerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LedgerAccount>> GetAllAccountsAsync()
        {
            return await _context.LedgerAccounts.ToListAsync();
        }

        public async Task<LedgerAccount?> GetAccountByIdAsync(int id)
        {
            return await _context.LedgerAccounts.FindAsync(id);
        }

        public async Task AddAccountAsync(LedgerAccount account)
        {
            await _context.LedgerAccounts.AddAsync(account);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAccountAsync(LedgerAccount account)
        {
            _context.LedgerAccounts.Update(account);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAccountAsync(int id)
        {
            var account = await GetAccountByIdAsync(id);
            if (account != null)
            {
                _context.LedgerAccounts.Remove(account);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddTransactionAsync(Transaction transaction)
        {
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByAccountIdAsync(int accountId)
        {
            return await _context.Transactions.Where(t => t.LedgerAccountId == accountId).ToListAsync();
        }
    }
}
