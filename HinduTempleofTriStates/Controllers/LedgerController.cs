using HinduTempleofTriStates.Models;
using Microsoft.AspNetCore.Mvc;
using HinduTempleofTriStates.Services;
using System;
using System.Threading.Tasks;

namespace HinduTempleofTriStates.Controllers
{
    [Route("Ledger")]
    public class LedgerController : Controller
    {
        private readonly LedgerService _ledgerService;
        private readonly ILogger<LedgerController> _logger;
        // Ensure logger is injected via the constructor
        public LedgerController(LedgerService ledgerService, ILogger<LedgerController> logger)
        {
            _ledgerService = ledgerService;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger)); // Check for null logger
        }

        // GET: Ledger/Create
        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ledger/Create
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LedgerAccount ledgerAccount)
        {
            // Log the current model state
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Model state is invalid.");
                foreach (var error in ModelState)
                {
                    _logger.LogWarning($"Key: {error.Key}, Errors: {string.Join(", ", error.Value.Errors.Select(e => e.ErrorMessage))}");
                }
                return View(ledgerAccount);
            }

            // Automatically set values for fields that are required but not filled by the form
            ledgerAccount.CreatedBy = "System"; // Or get the logged-in user's name
            ledgerAccount.UpdatedBy = "System"; // Or get the logged-in user's name
            ledgerAccount.CreatedDate = DateTime.UtcNow;
            ledgerAccount.UpdatedDate = DateTime.UtcNow;
            ledgerAccount.Balance = ledgerAccount.Balance != 0 ? ledgerAccount.Balance : 0; // Default balance to 0

            // Proceed if the model is valid
            try
            {
                await _ledgerService.AddAccountAsync(ledgerAccount);
                _logger.LogInformation("Ledger account created successfully.");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating ledger account.");
                return View(ledgerAccount);
            };
        }

        // GET: Ledger/Index
        [HttpGet("Index")]
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
            var account = await _ledgerService.GetLedgerAccountByIdAsync(id);
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

            var existingAccount = await _ledgerService.GetLedgerAccountByIdAsync(id);
            if (existingAccount == null)
            {
                return NotFound();
            }

            await _ledgerService.UpdateLedgerAccountAsync(account);
            return NoContent();
        }

        // DELETE: /ledger/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(Guid id)
        {
            var account = await _ledgerService.GetLedgerAccountByIdAsync(id);
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
