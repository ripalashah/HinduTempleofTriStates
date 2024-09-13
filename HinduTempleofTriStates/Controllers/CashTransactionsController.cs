using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using HinduTempleofTriStates.Models;
using HinduTempleofTriStates.Repositories;
using HinduTempleofTriStates.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging; // Add for logging

namespace HinduTempleofTriStates.Controllers
{
    [Route("transaction")]
    public class CashTransactionsController : Controller
    {
        private readonly ICashTransactionRepository _transactionRepository;
        private readonly LedgerService _ledgerService;
        private readonly ILogger<CashTransactionsController> _logger; // Add logger

        public CashTransactionsController(
            ICashTransactionRepository transactionRepository,
            LedgerService ledgerService,
            ILogger<CashTransactionsController> logger) // Inject logger
        {
            _transactionRepository = transactionRepository;
            _ledgerService = ledgerService;
            _logger = logger;
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
                _logger.LogInformation("Model state is valid, proceeding with transaction creation.");
                transaction.Id = Guid.NewGuid();
                _logger.LogInformation($"Saving transaction: {transaction.Description}, {transaction.Amount}, {transaction.LedgerAccountId}");

                try
                {
                    await _transactionRepository.AddCashTransactionAsync(transaction);
                    _logger.LogInformation("Transaction saved successfully.");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error saving transaction: {ex.Message}");
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            _logger.LogError("Model state is invalid.");
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
