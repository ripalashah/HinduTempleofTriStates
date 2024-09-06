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
            return await _context.LedgerAccounts.ToListAsync();
        }

        public async Task<LedgerAccount?> GetAccountByIdAsync(Guid id)
        {
            return await _context.LedgerAccounts.FindAsync(id);
        }

        public async Task AddAccountAsync(LedgerAccount account)
        {
            _context.LedgerAccounts.Add(account);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAccountAsync(LedgerAccount account)
        {
            _context.Entry(account).State = EntityState.Modified;
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

        public async Task AddDonationAsync(Donation donation)
        {
            var account = await _context.LedgerAccounts.FindAsync(donation.LedgerAccountId);
            if (account == null) throw new Exception("Account not found");

            account.Balance += (decimal)donation.Amount;
            _context.Donations.Add(donation);
            _context.LedgerAccounts.Update(account);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Donation>> GetDonationsByAccountIdAsync(Guid accountId)
        {
            return await _context.Donations.Where(d => d.LedgerAccountId == accountId).ToListAsync();
        }
    }
}
