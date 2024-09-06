using Microsoft.AspNetCore.Mvc.RazorPages;
using HinduTempleofTriStates.Services;
using HinduTempleofTriStates.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HinduTempleofTriStates.Views.Reports
{
    public class CashIncomeExpensesModel : PageModel
    {
        private readonly FinancialReportService _financialReportService;

        public CashIncomeExpensesModel(FinancialReportService financialReportService)
        {
            _financialReportService = financialReportService;
        }

        public List<CashTransaction> CashTransactions { get; set; } = new List<CashTransaction>();

        public decimal TotalIncome { get; set; }
        public decimal TotalExpenses { get; set; }

        public async Task OnGetAsync()
        {
            CashTransactions = await _financialReportService.GetCashTransactionsAsync();
            TotalIncome = _financialReportService.CalculateTotalIncome(CashTransactions);
            TotalExpenses = _financialReportService.CalculateTotalExpenses(CashTransactions);
        }
    }
}
