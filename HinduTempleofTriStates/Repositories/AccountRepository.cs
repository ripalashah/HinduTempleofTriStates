using HinduTempleofTriStates.Data;
using HinduTempleofTriStates.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        // Fetch GeneralLedgerEntry by its ID
        public async Task<GeneralLedgerEntry?> GetGeneralLedgerEntryByIdAsync(Guid id)
        {
            return await _context.GeneralLedgerEntries.FindAsync(id);
        }

        // Fetch Account by its ID
        public async Task<Account?> GetAccountByIdAsync(Guid id)
        {
            return await _context.Accounts.FindAsync(id);
        }

        // Fetch all GeneralLedgerEntries
        public async Task<IEnumerable<GeneralLedgerEntry>> GetAllGeneralLedgerEntriesAsync()
        {
            return await _context.GeneralLedgerEntries.ToListAsync();
        }

        // Fetch all Accounts
        public async Task<IEnumerable<Account>> GetAllAccountsAsync()
        {
            return await _context.Accounts.ToListAsync();
        }

        // Add a new Account
        public async Task AddAccountAsync(Account account)
        {
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
        }

        // Update an existing Account
        public async Task UpdateAccountAsync(Account account)
        {
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();
        }

        // Delete an Account by its ID
        public async Task DeleteAccountAsync(Guid id)
        {
            var account = await GetAccountByIdAsync(id);
            if (account != null)
            {
                _context.Accounts.Remove(account);
                await _context.SaveChangesAsync();
            }
        }

        // Add a new Fund
        public async Task AddFundAsync(Fund fund)
        {
            _context.Funds.Add(fund);
            await _context.SaveChangesAsync();
        }

        // Fetch all Funds
        public async Task<IEnumerable<Fund>> GetFundsAsync()
        {
            return await _context.Funds.ToListAsync();
        }

        // Reconcile a transaction by its ID
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

        // Fetch all unreconciled transactions
        public async Task<IEnumerable<Transaction>> GetUnreconciledTransactionsAsync()
        {
            return await _context.Transactions
                .Where(t => !t.Reconciled)
                .ToListAsync();
        }

        public Task<IEnumerable<GeneralLedgerEntry>> GetEntriesAsync(Guid accountId)
        {
            throw new NotImplementedException();
        }
    }
}
