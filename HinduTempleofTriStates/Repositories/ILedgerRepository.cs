using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HinduTempleofTriStates.Models;

namespace HinduTempleofTriStates.Repositories
{
    public interface ILedgerRepository
    {
        Task<LedgerAccount> GetAccountByIdAsync(Guid id);
        Task<IEnumerable<LedgerAccount>> GetAllAccountsAsync();
        Task AddAccountAsync(LedgerAccount account);
        Task UpdateAccountAsync(LedgerAccount account);
        
        Task<IEnumerable<Transaction>> GetTransactionsByAccountIdAsync(Guid accountId);
        Task AddTransactionAsync(Transaction transaction);
        Task<IList<LedgerAccount>> GetAllLedgerAccountsAsync();
        Task SoftDeleteLedgerAccountAsync(Guid id);  // This is the soft delete method
    }
}
