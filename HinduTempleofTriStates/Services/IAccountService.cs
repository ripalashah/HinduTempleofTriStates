using HinduTempleofTriStates.Models;

namespace HinduTempleofTriStates.Services
{
    public interface IAccountService
    {
        Task<Account> GetAccountByIdAsync(Guid accountId);
        Task UpdateAccountBalanceAsync(Guid accountId, decimal amount, bool isCredit);
        Task<bool> ValidateTransactionAsync(Guid accountId, decimal amount);
        Task AddTransactionAsync(Guid accountId, Transaction transaction);
        Task AddAccountAsync(Account account);
        Task UpdateAccountAsync(Account account);

    }
}
