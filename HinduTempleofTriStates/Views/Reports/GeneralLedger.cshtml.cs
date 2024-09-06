using HinduTempleofTriStates.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HinduTempleofTriStates.Views.Reports;
using HinduTempleofTriStates.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HinduTempleofTriStates.Views.Reports
{
    public class GeneralLedgerModel : PageModel
    {
        private readonly FinancialReportService _financialReportService;

        public GeneralLedgerModel(FinancialReportService financialReportService)
        {
            _financialReportService = financialReportService;
        }

        public List<GeneralLedgerEntry> GeneralLedgerEntries { get; set; }
        public decimal TotalDebit { get; set; }
        public decimal TotalCredit { get; set; }
        public decimal TotalBalance { get; set; }

        public async Task OnGetAsync()
        {
            var ledgerData = await _financialReportService.GetGeneralLedgerAsync();
            GeneralLedgerEntries = ledgerData.GeneralLedgerEntries;
            TotalDebit = ledgerData.TotalDebit;
            TotalCredit = ledgerData.TotalCredit;
            TotalBalance = ledgerData.TotalBalance;
        }
    }
}
