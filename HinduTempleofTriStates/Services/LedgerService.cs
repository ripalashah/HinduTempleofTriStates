using HinduTempleofTriStates.Models;
using HinduTempleofTriStates.Repositories;
using TempleManagementSystem.Models;

namespace TempleManagementSystem.Services
{
    public class LedgerService
    {
        private readonly ILedgerRepository _ledgerRepository;

        public LedgerService(ILedgerRepository ledgerRepository)
        {
            _ledgerRepository = ledgerRepository;
        }

        public async Task<IEnumerable<LedgerAccount>> GetAllAccountsAsync()
        {
            return await _ledgerRepository.GetAllAccountsAsync();
        }

        public async Task<LedgerAccount> GetAccountByIdAsync(int id)
        {
            return await _ledgerRepository.GetAccountByIdAsync(id);
        }

        public async Task AddAccountAsync(LedgerAccount account)
        {
            await _ledgerRepository.AddAccountAsync(account);
        }

        public async Task AddTransactionAsync(Transaction transaction)
        {
            await _ledgerRepository.AddTransactionAsync(transaction);
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByAccountIdAsync(int accountId)
        {
            return await _ledgerRepository.GetTransactionsByAccountIdAsync(accountId);
        }
    }
}
