using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using HinduTempleofTriStates.Models;
using HinduTempleofTriStates.Repositories;
using HinduTempleofTriStates.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HinduTempleofTriStates.Controllers
{
    [Route("transaction")]
    public class CashTransactionsController : Controller
    {
        private readonly ICashTransactionRepository _transactionRepository;
        private readonly LedgerService _ledgerService;

        public CashTransactionsController(ICashTransactionRepository transactionRepository, LedgerService ledgerService)
        {
            _transactionRepository = transactionRepository;
            _ledgerService = ledgerService;
        }

        // GET: CashTransactions
        [HttpGet]
        [Route("")]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var transactions = await _transactionRepository.GetAllCashTransactionsAsync();
            return View(transactions);
        }

        // GET: CashTransactions/Create
        [HttpGet]
        [Route("Create")]
        public async Task<IActionResult> Create()
        {
            var ledgerAccounts = await _ledgerService.GetAllAccountsAsync();
            ViewBag.LedgerAccounts = new SelectList(ledgerAccounts, "Id", "AccountName");
            return View();
        }

        // POST: CashTransactions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<IActionResult> Create([Bind("Date,Description,Amount,Type,LedgerAccountId")] CashTransaction transaction)
        {
            if (ModelState.IsValid)
            {
                transaction.Id = Guid.NewGuid();
                await _transactionRepository.AddCashTransactionAsync(transaction);
                return RedirectToAction(nameof(Index));
            }

            var ledgerAccounts = await _ledgerService.GetAllAccountsAsync();
            ViewBag.LedgerAccounts = new SelectList(ledgerAccounts, "Id", "AccountName");

            return View(transaction);
        }

        // GET: CashIncomeExpenses
        [HttpGet]
        [Route("IncomeExpenses")]
        public async Task<IActionResult> IncomeExpenses()
        {
            var transactions = await _transactionRepository.GetAllCashTransactionsAsync();
            var model = new CashIncomeExpensesModel
            {
                CashTransactions = transactions.ToList()
            };

            return View(model);
        }
    }
}
