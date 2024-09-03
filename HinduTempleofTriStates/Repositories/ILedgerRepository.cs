using HinduTempleofTriStates.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HinduTempleofTriStates.Repositories
{
    public interface ILedgerRepository
    {
        Task<IEnumerable<LedgerAccount>> GetAllAccountsAsync();
        Task<LedgerAccount?> GetAccountByIdAsync(Guid id);
        Task AddAccountAsync(LedgerAccount account);
        Task UpdateAccountAsync(LedgerAccount account);
        Task DeleteAccountAsync(Guid id);
        Task<IEnumerable<Transaction>> GetTransactionsByAccountIdAsync(Guid id);
        Task AddTransactionAsync(Transaction transaction);
    }
}
