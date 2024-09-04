using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HinduTempleofTriStates.Models;
using HinduTempleofTriStates.Data;

namespace HinduTempleofTriStates.Repositories
{
    public interface ILedgerRepository
    {
        Task<LedgerAccount> GetAccountByIdAsync(Guid id);
        Task<IEnumerable<LedgerAccount>> GetAllAccountsAsync();
        Task AddAccountAsync(LedgerAccount account);
        Task UpdateAccountAsync(LedgerAccount account);
        Task DeleteAccountAsync(Guid id);
        Task<IEnumerable<Transaction>> GetTransactionsByAccountIdAsync(Guid accountId);
        Task AddTransactionAsync(Transaction transaction);
    }
}
