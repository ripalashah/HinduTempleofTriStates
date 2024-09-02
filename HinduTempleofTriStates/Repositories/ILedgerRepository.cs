using HinduTempleofTriStates.Models;

namespace HinduTempleofTriStates.Repositories
{
    public interface ILedgerRepository
    {
        Task<IEnumerable<LedgerAccount>> GetAllAccountsAsync();
        Task<LedgerAccount?> GetAccountByIdAsync(int id); // Allow nullable return type
        Task AddAccountAsync(LedgerAccount account);
        Task UpdateAccountAsync(LedgerAccount account);
        Task DeleteAccountAsync(int id);
        Task AddTransactionAsync(Transaction transaction);
        Task<IEnumerable<Transaction>> GetTransactionsByAccountIdAsync(int accountId);
    }
}
