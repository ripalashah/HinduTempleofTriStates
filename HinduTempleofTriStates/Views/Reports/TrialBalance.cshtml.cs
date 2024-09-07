using Microsoft.AspNetCore.Mvc.RazorPages;
using HinduTempleofTriStates.Views.Reports;
using HinduTempleofTriStates.Services;
using HinduTempleofTriStates.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HinduTempleofTriStates.Views.Reports
{
    public class TrialBalanceModel : PageModel
    {
        private readonly FinancialReportService _financialReportService;

        public TrialBalanceModel(FinancialReportService financialReportService)
        {
            _financialReportService = financialReportService;
            TrialBalanceAccounts = new List<TrialBalanceAccount>();
        }

        public List<TrialBalanceAccount> TrialBalanceAccounts { get; set; }
        public decimal TotalDebits { get; set; }
        public decimal TotalCredits { get; set; }
        public decimal NetBalance { get; set; }

        public async Task OnGetAsync()
        {
            var trialBalanceData = await _financialReportService.GetTrialBalanceAsync();
            TrialBalanceAccounts = trialBalanceData.TrialBalanceAccounts;
            TotalDebits = trialBalanceData.TotalDebits;
            TotalCredits = trialBalanceData.TotalCredits;
            NetBalance = trialBalanceData.NetBalance;
        }
    }
}
