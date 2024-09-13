using HinduTempleofTriStates.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HinduTempleofTriStates.Services
{
    public interface IReportService
    {
        Task<GeneralLedgerModel> GenerateGeneralLedgerAsync();
        Task<ProfitLossModel> GenerateProfitLossAsync(DateTime startDate, DateTime endDate);
        Task<TrialBalanceModel> GenerateTrialBalanceAsync();
        Task<CashIncomeExpensesModel> GetCashIncomeExpensesAsync();
        

    }
}
