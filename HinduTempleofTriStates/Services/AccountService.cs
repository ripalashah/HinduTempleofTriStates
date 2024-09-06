using HinduTempleofTriStates.Data;
using HinduTempleofTriStates.Models;
using Microsoft.EntityFrameworkCore;

namespace HinduTempleofTriStates.Services
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _context;

        public AccountService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Fetch an account by ID
        public async Task<Account> GetAccountByIdAsync(Guid accountId)
        {
            var account = await _context.Accounts.FindAsync(accountId);
            if (account == null)
            {
                throw new Exception("Account not found");
            }
            return account;
        }


        // Update account balance by crediting or debiting
        public async Task UpdateAccountBalanceAsync(Guid accountId, decimal amount, bool isCredit)
        {
            var account = await GetAccountByIdAsync(accountId);
            if (account == null) throw new Exception("Account not found");

            if (isCredit)
            {
                account.Balance += amount; // Credit
            }
            else
            {
                account.Balance -= amount; // Debit
            }

            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();
        }

        // Validate a transaction to prevent overdrafts, invalid amounts, etc.
        public async Task<bool> ValidateTransactionAsync(Guid accountId, decimal amount)
        {
            var account = await GetAccountByIdAsync(accountId);
            return account != null && account.Balance >= amount;
        }

        // Add a transaction to the account and ledger
        public async Task AddTransactionAsync(Guid accountId, Transaction transaction)
        {
            var account = await GetAccountByIdAsync(accountId);
            if (account == null) throw new Exception("Account not found");

            await _context.Transactions.AddAsync(transaction);
            await UpdateAccountBalanceAsync(accountId, transaction.Amount, transaction.TransactionType == TransactionType.Credit);
        }

        public Task AddAccountAsync(Account account)
        {
            throw new NotImplementedException();
        }
    }
}
