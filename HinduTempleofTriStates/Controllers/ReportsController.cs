using Microsoft.AspNetCore.Mvc;
using HinduTempleofTriStates.Services;
using System.Linq;
using System.Threading.Tasks;
using HinduTempleofTriStates.ViewModels;

namespace HinduTempleofTriStates.Controllers
{
    [Route("Reports")]
    public class ReportsController : Controller
    {
        private readonly IReportService _reportService;

        // Inject a logger
        private readonly ILogger<ReportsController> _logger;

        public ReportsController(IReportService reportService, ILogger<ReportsController> logger)
        {
            _reportService = reportService;
            _logger = logger;
        }

        // Reports Dashboard
        [HttpGet("")]
        public IActionResult Index()
        {
            return View("Index"); // Loads the reports dashboard page
        }

        // Cash Income and Expenses Report
        [HttpGet("CashIncomeExpenses")]
        public async Task<IActionResult> CashIncomeExpenses()
        {
            var model = await _reportService.GetCashIncomeExpensesAsync();
            if (model == null) return View("Error");

            // Map the model to a view model if needed
            var viewModel = new CashIncomeExpensesViewModel
            {
                TotalIncome = model.TotalIncome,
                TotalExpense = model.TotalExpense,
                CashTransactions = model.CashTransactions // Don't forget to map transactions!
            };
            // Inside any action where you handle null models
            if (model == null)
            {
                _logger.LogError("Failed to retrieve report data for Cash Income and Expenses.");
                return View("Error");
            }

            return View(viewModel);
        }

        // General Ledger Report
        [HttpGet("GeneralLedger")]
        public async Task<IActionResult> GeneralLedger()
        {
            var model = await _reportService.GenerateGeneralLedgerAsync();
            if (model == null) return View("Error");

            var viewModel = new GeneralLedgerViewModel
            {
                LedgerEntries = model.LedgerEntries, // Use GeneralLedgerEntryModel
                TotalDebits = model.TotalDebits,
                TotalCredits = model.TotalCredits
            };

            return View(viewModel);
        }

        // Profit and Loss Report
        [HttpGet("ProfitLoss")]
        // Profit and Loss Report
        public async Task<IActionResult> ProfitLoss(DateTime? startDate, DateTime? endDate)
        {
            // If no dates are provided, default to the start and end of the current year
            var start = startDate ?? new DateTime(DateTime.Now.Year, 1, 1); // Start of the current year
            var end = endDate ?? DateTime.UtcNow; // Current date as the end date

            var model = await _reportService.GenerateProfitLossAsync(start, end);
            if (model == null) return View("Error");

            // Correctly map the model to a ProfitLossViewModel
            var viewModel = new ProfitLossViewModel
            {
                TotalIncome = model.TotalIncome,
                TotalExpenses = model.TotalExpenses,
                ProfitLossItems = model.ProfitLossItems
                    .Select(item => new ProfitLossItemViewModel
                    {
                        Description = item.Description,
                        Amount = item.Amount,
                        IsIncome = item.IsIncome // Map each property accordingly
                    }).ToList()
            };

            return View(viewModel);
        }



        // Trial Balance Report
        [HttpGet("TrialBalance")]
        public async Task<IActionResult> TrialBalance()
        {
            var model = await _reportService.GenerateTrialBalanceAsync();
            if (model == null) return View("Error");

            var viewModel = new TrialBalanceViewModel
            {
                TrialBalanceAccounts = model.TrialBalanceAccounts
                    .Select(a => new TrialBalanceAccountViewModel
                    {
                        AccountName = a.AccountName,
                        DebitTotal = a.DebitTotal,
                        CreditTotal = a.CreditTotal
                    }).ToList(),
                            TotalDebits = model.TrialBalanceAccounts.Sum(a => a.DebitTotal),
                            TotalCredits = model.TrialBalanceAccounts.Sum(a => a.CreditTotal) // Do not assign NetBalance
            };


            return View(viewModel);
        }
    }
}
