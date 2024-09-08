using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HinduTempleofTriStates.Models;
using HinduTempleofTriStates.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using HinduTempleofTriStates.Repositories;
using Microsoft.Extensions.Logging;

namespace HinduTempleofTriStates.Controllers
{
    [Route("controller")]
    public class AccountsController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountService _accountService;
        private readonly LedgerService _ledgerService;
        private readonly ILogger<AccountsController> _logger;

        // Consolidated constructor for Dependency Injection (DI)
        public AccountsController(IAccountRepository accountRepository, IAccountService accountService, LedgerService ledgerService, ILogger<AccountsController> logger)
        {
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
            _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
            _ledgerService = ledgerService ?? throw new ArgumentNullException(nameof(ledgerService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // GET: Accounts
        [HttpGet]
        [Route("")]
        [Route("Index")]
        public async Task<IActionResult> Index(string search)
        {
            var accounts = await _accountRepository.GetAllAccountsAsync();

            // If search query is provided, filter the accounts by AccountName
            if (!string.IsNullOrEmpty(search))
            {
                accounts = accounts.Where(a => a.AccountName.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            return View(accounts);
        }

        // GET: Accounts/Details/5
        [HttpGet("details/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var account = await _accountRepository.GetAccountByIdAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // GET: Accounts/Create
        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Accounts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<IActionResult> Create([Bind("AccountName,Balance,AccountType")] Account account)
        {
            if (ModelState.IsValid)
            {
                account.Id = Guid.NewGuid();  // Generate a new Guid for the account
                try
                {
                    await _accountRepository.AddAccountAsync(account);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error creating account: {ex.Message}");
                }
            }
            return View(account);
        }

        // GET: Accounts/Edit/5
        [HttpGet]
        [Route("Edit/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var account = await _accountRepository.GetAccountByIdAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            return View(account);
        }

        // POST: Accounts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,AccountName,Balance,AccountType")] Account account)
        {
            if (id != account.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _accountRepository.UpdateAccountAsync(account);
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await AccountExistsAsync(account.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(account);
        }

        // GET: Accounts/Delete/5
        [HttpGet]
        [Route("Delete/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var account = await _accountRepository.GetAccountByIdAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            return View(account);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var account = await _accountRepository.GetAccountByIdAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            try
            {
                await _accountRepository.DeleteAccountAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("", $"Unable to delete account: {ex.Message}");
            }
            return View(account);
        }

        // GET: Accounts/CreateFund
        [HttpGet]
        public IActionResult CreateFund()
        {
            return View();
        }

        // POST: Accounts/CreateFund
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFund(Fund fund)
        {
            if (ModelState.IsValid)
            {
                await _accountRepository.AddFundAsync(fund);
                return RedirectToAction(nameof(ManageFunds));
            }
            return View(fund);
        }

        // GET: Accounts/ManageFunds
        [HttpGet]
        public async Task<IActionResult> ManageFunds()
        {
            var funds = await _accountRepository.GetFundsAsync();
            return View(funds);
        }

        // GET: Accounts/Reports
        [HttpGet("Reports")]
        public async Task<IActionResult> Reports()
        {
            var accounts = await _accountService.GetAccountsAsync();
            if (accounts == null)
            {
                _logger.LogError("Accounts data is null");
                return NotFound("No accounts found.");
            }

            return View("~/Views/Accounts/Reports/Reports.cshtml", accounts);
        }
        
        // POST: Accounts/Reconcile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reconcile(Guid transactionId)
        {
            await _accountRepository.ReconcileTransactionAsync(transactionId);
            return RedirectToAction(nameof(Reconciliation));
        }

        // GET: Accounts/Reconciliation
        [HttpGet]
        public async Task<IActionResult> Reconciliation()
        {
            var transactions = await _accountRepository.GetUnreconciledTransactionsAsync();
            return View(transactions);
        }

        private async Task<bool> AccountExistsAsync(Guid id)
        {
            return await _accountRepository.AccountExistsAsync(id);
        }
    }
}
