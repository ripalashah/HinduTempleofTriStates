using HinduTempleofTriStates.Models;
using System.Threading.Tasks;

namespace HinduTempleofTriStates.Services
{
    public interface IReportService
    {
        Task<GeneralLedgerModel> GenerateGeneralLedgerAsync();
        Task<ProfitLossModel> GenerateProfitLossAsync();
        Task<TrialBalanceModel> GenerateTrialBalanceAsync();
        Task<CashIncomeExpensesModel> GetCashIncomeExpensesAsync();
    }
}
