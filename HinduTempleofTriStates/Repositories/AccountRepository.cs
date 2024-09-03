using HinduTempleofTriStates.Data;
using HinduTempleofTriStates.Models;
using HinduTempleofTriStates.Pages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;  // Ensure this is included for LINQ methods
using System.Threading.Tasks;

namespace HinduTempleofTriStates.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDbContext _context;

        public AccountRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GeneralLedgerEntry?> GetGeneralLedgerEntryByIdAsync(int id)
        {
            return await _context.GeneralLedgerEntries.FindAsync(id);
        }

        public async Task<Account?> GetAccountByIdAsync(int id)
        {
            return await _context.Accounts.FindAsync(id);
        }

        public async Task<IEnumerable<GeneralLedgerEntry>> GetAllGeneralLedgerEntriesAsync()
        {
            return await _context.GeneralLedgerEntries.ToListAsync();
        }
        public async Task<IEnumerable<Account>> GetAllAccountsAsync()
        {
            return await _context.Accounts.ToListAsync();
        }

        public async Task AddAccountAsync(Account account)
        {
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAccountAsync(Account account)
        {
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAccountAsync(int id)
        {
            var account = await GetAccountByIdAsync(id);
            if (account != null)
            {
                _context.Accounts.Remove(account);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddFundAsync(Fund fund)
        {
            _context.Funds.Add(fund);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Fund>> GetFundsAsync()
        {
            return await _context.Funds.ToListAsync();
        }

        public async Task ReconcileTransactionAsync(Guid transactionId)
        {
            var transaction = await _context.Transactions.FindAsync(transactionId);
            if (transaction != null)
            {
                transaction.Reconciled = true;
                transaction.ReconciliationDate = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Transaction>> GetUnreconciledTransactionsAsync()
        {
            return await _context.Transactions
                .Where(t => !t.Reconciled)
                .ToListAsync();
        }
    }
}
