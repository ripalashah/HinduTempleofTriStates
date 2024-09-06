using HinduTempleofTriStates.Models;

public interface ICashTransactionRepository
{
    Task<IEnumerable<CashTransaction>> GetAllCashTransactionsAsync();
    Task<CashTransaction?> GetCashTransactionByIdAsync(Guid id);
    Task AddCashTransactionAsync(CashTransaction transaction);
    Task UpdateCashTransactionAsync(CashTransaction transaction);
    Task DeleteCashTransactionAsync(Guid id);
    Task<bool> CashTransactionExistsAsync(Guid id);
}
