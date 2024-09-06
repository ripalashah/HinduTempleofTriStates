using Microsoft.AspNetCore.Mvc;
using HinduTempleofTriStates.Models;
using HinduTempleofTriStates.Repositories;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace HinduTempleofTriStates.Controllers
{
    public class CashTransactionsController : Controller
    {
        private readonly ICashTransactionRepository _transactionRepository;

        public CashTransactionsController(ICashTransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        // GET: CashTransactions
        public async Task<IActionResult> Index()
        {
            var transactions = await _transactionRepository.GetAllCashTransactionsAsync();
            return View(transactions);
        }

        // GET: CashTransactions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CashTransactions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Date,Description,Amount,Type,LedgerAccountId")] CashTransaction transaction)
        {
            if (ModelState.IsValid)
            {
                transaction.Id = Guid.NewGuid();
                await _transactionRepository.AddCashTransactionAsync(transaction);
                return RedirectToAction(nameof(Index));
            }
            return View(transaction);
        }

        // GET: CashIncomeExpenses
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
