using Microsoft.AspNetCore.Mvc;
using HinduTempleofTriStates.Repositories;
using HinduTempleofTriStates.Services;
using System.Threading.Tasks;

namespace HinduTempleofTriStates.Controllers
{
    public class ReportsController : Controller
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        // General Ledger Report
        public async Task<IActionResult> GeneralLedger()
        {
            var model = await _reportService.GenerateGeneralLedgerAsync();
            return View(model);
        }

        // Profit and Loss Report
        public async Task<IActionResult> ProfitLoss()
        {
            var model = await _reportService.GenerateProfitLossAsync();
            return View(model);
        }

        // Trial Balance Report
        public async Task<IActionResult> TrialBalance()
        {
            var model = await _reportService.GenerateTrialBalanceAsync();
            return View(model);
        }
    }
}
