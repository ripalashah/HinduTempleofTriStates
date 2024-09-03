using HinduTempleofTriStates.Models;
using HinduTempleofTriStates.Pages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HinduTempleofTriStates.Repositories
{
    public interface IAccountRepository
    {
        // CRUD operations for Account
        Task<IEnumerable<Account>> GetAllAccountsAsync();
        Task<Account?> GetAccountByIdAsync(int id);
        Task AddAccountAsync(Account account);
        Task UpdateAccountAsync(Account account);
        Task DeleteAccountAsync(int id);

        // Additional methods for Funds
        Task AddFundAsync(Fund fund);
        Task<IEnumerable<Fund>> GetFundsAsync();

        // Additional methods for Transactions
        Task ReconcileTransactionAsync(Guid transactionId);
        Task<IEnumerable<Transaction>> GetUnreconciledTransactionsAsync();
        Task<IEnumerable<GeneralLedgerEntry>> GetAllGeneralLedgerEntriesAsync();
        Task<GeneralLedgerEntry> GetGeneralLedgerEntryByIdAsync(int id);
    }
}
