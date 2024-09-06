using HinduTempleofTriStates.Models;
using Microsoft.AspNetCore.Mvc;
using HinduTempleofTriStates.Services;
using System;
using System.Threading.Tasks;

namespace HinduTempleofTriStates.Controllers
{
    public class LedgerController : Controller
    {
        private readonly LedgerService _ledgerService;

        public LedgerController(LedgerService ledgerService)
        {
            _ledgerService = ledgerService;
        }

        // GET: Ledger/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ledger/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LedgerAccount ledgerAccount)
        {
            if (ModelState.IsValid)
            {
                await _ledgerService.AddAccountAsync(ledgerAccount);
                return RedirectToAction(nameof(Index));
            }
            return View(ledgerAccount);
        }

        // GET: Ledger/Index
        public async Task<IActionResult> Index()
        {
            var accounts = await _ledgerService.GetAllAccountsAsync();
            return View(accounts);
        }

        // GET: /ledger
        [HttpGet]
        public async Task<IActionResult> GetAllAccounts()
        {
            var accounts = await _ledgerService.GetAllAccountsAsync();
            return Ok(accounts); // For API purposes, returning Ok with data
        }

        // GET: /ledger/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccount(Guid id)
        {
            var account = await _ledgerService.GetAccountByIdAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            return Ok(account);
        }

        // POST: /ledger
        [HttpPost]
        public async Task<IActionResult> AddAccount([FromBody] LedgerAccount account)
        {
            if (account == null)
            {
                return BadRequest("Account cannot be null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _ledgerService.AddAccountAsync(account);
            return CreatedAtAction(nameof(GetAccount), new { id = account.Id }, account);
        }

        // POST: /ledger/transaction
        [HttpPost("transaction")]
        public async Task<IActionResult> AddTransaction([FromBody] Transaction transaction)
        {
            if (transaction == null)
            {
                return BadRequest("Transaction cannot be null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _ledgerService.AddTransactionAsync(transaction);
            return Ok();
        }

        // PUT: /ledger/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccount(Guid id, [FromBody] LedgerAccount account)
        {
            if (account == null || id != account.Id)
            {
                return BadRequest("Invalid account data.");
            }

            var existingAccount = await _ledgerService.GetAccountByIdAsync(id);
            if (existingAccount == null)
            {
                return NotFound();
            }

            await _ledgerService.UpdateAccountAsync(account);
            return NoContent();
        }

        // DELETE: /ledger/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(Guid id)
        {
            var account = await _ledgerService.GetAccountByIdAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            await _ledgerService.DeleteAccountAsync(id);
            return NoContent();
        }

        // GET: /ledger/{id}/transactions
        [HttpGet("{id}/transactions")]
        public async Task<IActionResult> GetTransactionsByAccountId(Guid id)
        {
            var transactions = await _ledgerService.GetTransactionsByAccountIdAsync(id);
            if (transactions == null)
            {
                return NotFound();
            }
            return Ok(transactions);
        }
    }
}
