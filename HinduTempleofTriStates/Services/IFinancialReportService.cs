using System.Threading.Tasks;
using HinduTempleofTriStates.Models;

namespace HinduTempleofTriStates.Services
{
    public interface IFinancialReportService
    {
        Task<CashIncomeExpensesModel> GetCashIncomeExpensesAsync();
        Task<TrialBalanceModel> GetTrialBalanceAsync();
        Task<GeneralLedgerModel> GetGeneralLedgerAsync();

        Task<List<CashTransaction>> GetCashTransactionsAsync(); 
        decimal CalculateTotalIncome(List<CashTransaction> transactions);
        decimal CalculateTotalExpenses(List<CashTransaction> transactions);

        // You can add other financial report methods here if needed
    }
}
