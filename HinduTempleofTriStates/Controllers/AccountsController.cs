using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HinduTempleofTriStates.Data;
using HinduTempleofTriStates.Models;
using System.Threading.Tasks;
using HinduTempleofTriStates.Repositories;
using HinduTempleofTriStates.Services;

namespace HinduTempleofTriStates.Controllers
{
    public class AccountsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IAccountRepository _accountRepository;
        private readonly LedgerService _ledgerService;

        // Single constructor to satisfy DI container
        public AccountsController(ApplicationDbContext context, IAccountRepository accountRepository, LedgerService ledgerService)
        {
            _context = context;
            _accountRepository = accountRepository;
            _ledgerService = ledgerService;
        }

        // GET: Accounts
        public async Task<IActionResult> Index()
        {
            var accounts = await _context.Accounts.ToListAsync();
            return View(accounts);
        }

        // GET: Accounts/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var account = await _context.Accounts
                .FirstOrDefaultAsync(m => m.Id == id);

            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // GET: Accounts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Accounts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AccountName,Balance")] Account account)
        {
            if (ModelState.IsValid)
            {
                _context.Add(account);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(account);
        }

        // GET: Accounts/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            return View(account);
        }

        // POST: Accounts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AccountName,Balance")] Account account)
        {
            if (id != account.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(account);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(account.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(account);
        }

        // GET: Accounts/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var account = await _context.Accounts
                .FirstOrDefaultAsync(m => m.Id == id);

            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
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
                return RedirectToAction("ManageFunds");
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

        // POST: Accounts/Reconcile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reconcile(Guid transactionId)
        {
            await _accountRepository.ReconcileTransactionAsync(transactionId);
            return RedirectToAction("Reconciliation");
        }

        // GET: Accounts/Reconciliation
        [HttpGet]
        public async Task<IActionResult> Reconciliation()
        {
            var transactions = await _accountRepository.GetUnreconciledTransactionsAsync();
            return View(transactions);
        }

        private bool AccountExists(int id)
        {
            return _context.Accounts.Any(e => e.Id == id);
        }
    }
}
