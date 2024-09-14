using HinduTempleofTriStates.Models;
using Microsoft.AspNetCore.Mvc;
using HinduTempleofTriStates.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HinduTempleofTriStates.Controllers
{
    [Route("Ledger")]
    public class LedgerController : Controller
    {
        private readonly LedgerService _ledgerService;
        private readonly ILogger<LedgerController> _logger;
        private readonly IDonationService _donationService;

        public LedgerController(LedgerService ledgerService, IDonationService donationService, ILogger<LedgerController> logger)
        {
            _ledgerService = ledgerService ?? throw new ArgumentNullException(nameof(ledgerService));
            _donationService = donationService ?? throw new ArgumentNullException(nameof(donationService)); // No casting required
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
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
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Model state is invalid.");
                foreach (var error in ModelState)
                {
                    _logger.LogWarning($"Key: {error.Key}, Errors: {string.Join(", ", error.Value.Errors.Select(e => e.ErrorMessage))}");
                }
                return View(ledgerAccount);
            }

            ledgerAccount.CreatedBy = "System";  // Replace with actual user identity if available
            ledgerAccount.UpdatedBy = "System";
            ledgerAccount.CreatedDate = DateTime.UtcNow;
            ledgerAccount.UpdatedDate = DateTime.UtcNow;
            ledgerAccount.Balance = ledgerAccount.Balance != 0 ? ledgerAccount.Balance : 0;

            try
            {
                // Save the new ledger account
                await _ledgerService.AddAccountAsync(ledgerAccount);
                _logger.LogInformation("Ledger account created successfully.");

                // Check if any donation exists for the account and update balance accordingly
                var donations = await _donationService.GetDonationsByLedgerAccountIdAsync(ledgerAccount.Id);
                if (donations.Any())
                {
                    decimal balance = 0;

                    foreach (var donation in donations)
                    {
                        // Adjust balance based on donation type
                        if (donation.DonationType == "Credit")
                        {
                            balance += (decimal)donation.Amount;
                        }
                        else if (donation.DonationType == "Debit")
                        {
                            balance -= (decimal)donation.Amount;
                        }
                    }

                    // Update ledger account balance
                    ledgerAccount.Balance = balance;

                    // Save updated balance
                    await _ledgerService.UpdateLedgerAccountAsync(ledgerAccount);
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating ledger account.");
                return View(ledgerAccount);
            }
        }

        // GET: Ledger/Index
        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var accounts = await _ledgerService.GetAllAccountsAsync();
            return View(accounts);
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

        // DELETE: /ledger/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(Guid id)
        {
            var account = await _ledgerService.GetLedgerAccountByIdAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            try
            {
                // Manually delete associated donations before soft-deleting the ledger account
                await _ledgerService.DeleteAssociatedDonationsAsync(id);

                // Soft delete the ledger account
                await _ledgerService.SoftDeleteLedgerAccountAsync(id);
                _logger.LogInformation($"Ledger account with ID {id} was soft-deleted.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting ledger account with ID {id}.");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
