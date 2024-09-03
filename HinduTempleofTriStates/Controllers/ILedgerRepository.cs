using HinduTempleofTriStates.Models;

namespace HinduTempleofTriStates.Controllers
{
    public interface ILedgerRepository
    {
        Task AddAccountAsync(LedgerAccount ledgerAccount);
        Task AddTransactionAsync(Transaction transaction);
        Task GetAccountByIdAsync(Guid id);
    }
}