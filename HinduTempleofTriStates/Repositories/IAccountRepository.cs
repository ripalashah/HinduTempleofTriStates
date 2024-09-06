using HinduTempleofTriStates.Models;
using HinduTempleofTriStates.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HinduTempleofTriStates.Repositories
{
    public interface IAccountRepository
    {
        // CRUD operations for Account
        Task<IEnumerable<Account>> GetAllAccountsAsync();
        Task<Account?> GetAccountByIdAsync(Guid id); // Changed from int to Guid
        Task<bool> AccountExistsAsync(Guid id); // Add this line to define the method
        Task AddAccountAsync(Account account);
        Task UpdateAccountAsync(Account account);
        Task DeleteAccountAsync(Guid id); // Changed from int to Guid

        // Additional methods for Funds
        Task AddFundAsync(Fund fund);
        Task<IEnumerable<Fund>> GetFundsAsync();

        // Additional methods for Transactions
        Task ReconcileTransactionAsync(Guid transactionId);
        Task<IEnumerable<Transaction>> GetUnreconciledTransactionsAsync();

        // Methods for General Ledger Entries
        Task<IEnumerable<GeneralLedgerEntry>> GetAllGeneralLedgerEntriesAsync();
        Task<GeneralLedgerEntry?> GetGeneralLedgerEntryByIdAsync(Guid id); // Changed from int to Guid
        Task<IEnumerable<GeneralLedgerEntry>> GetEntriesAsync(Guid accountId);
    }
}
