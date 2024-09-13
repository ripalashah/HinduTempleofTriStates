using HinduTempleofTriStates.Data;
using HinduTempleofTriStates.Models;
using HinduTempleofTriStates.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HinduTempleofTriStates.Controllers
{
    [Route("donation")]
    public class DonationController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IDonationService _donationService;
        private readonly ILogger<DonationController> _logger;

        public DonationController(ApplicationDbContext context, IDonationService donationService, ILogger<DonationController> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _donationService = donationService ?? throw new ArgumentNullException(nameof(donationService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // List all donations
        [HttpGet]
        [Route("")]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            try
            {
                var donations = await _context.Donations
                    .Include(d => d.LedgerAccount)
                    .Include(d => d.CashTransactions)
                    .ToListAsync();
                _logger.LogInformation("Fetched donation list successfully.");
                return View(donations);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching donations");
                return BadRequest("Unable to load donations. Please try again later.");
            }
        }

        // Display form to create a donation
        [HttpGet]
        [Route("Create")]
        public async Task<IActionResult> Create()
        {
            var ledgerAccounts = await _context.LedgerAccounts.ToListAsync();
            ViewBag.LedgerAccounts = new SelectList(ledgerAccounts, "Id", "AccountName");
            return View();
        }

        // Handle the post request to create a new donation
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<IActionResult> Create(Donation donation)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Assign a new unique Id to the donation
                    donation.Id = Guid.NewGuid();
                    donation.Date = DateTime.UtcNow;

                    // Fetch the related LedgerAccount
                    var ledgerAccount = await _context.LedgerAccounts.FindAsync(donation.LedgerAccountId);
                    if (ledgerAccount == null)
                    {
                        _logger.LogWarning("Ledger account not found with ID {LedgerAccountId}", donation.LedgerAccountId);
                        ModelState.AddModelError("LedgerAccountId", "Ledger account not found.");
                        return View(donation);
                    }

                    // Save the Donation first
                    _context.Donations.Add(donation);
                    await _context.SaveChangesAsync();

                    // Create corresponding GeneralLedgerEntry and CashTransaction
                    var ledgerEntry = new GeneralLedgerEntry
                    {
                        Id = Guid.NewGuid(),
                        DonationId = donation.Id,
                        Date = DateTime.UtcNow,
                        Description = $"Donation entry from {donation.DonorName}",
                        Debit = 0,
                        Credit = (decimal)donation.Amount,
                        LedgerAccountId = donation.LedgerAccountId,
                    };

                    var cashTransaction = new CashTransaction
                    {
                        Id = Guid.NewGuid(),
                        DonationId = donation.Id,
                        Amount = (decimal)donation.Amount,
                        Date = DateTime.UtcNow,
                        Description = $"Donation from {donation.DonorName}",
                        LedgerAccountId = donation.LedgerAccountId,
                        TransactionType = TransactionType.Credit // Ensure the donation is treated as a credit
                    };

                    // Add and save GeneralLedgerEntry and CashTransaction
                    _context.GeneralLedgerEntries.Add(ledgerEntry);
                    _context.CashTransactions.Add(cashTransaction);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating donation and related entities");
                    return View(donation);
                }
            }

            return View(donation);
        }

        // Display confirmation after successful donation
        [HttpGet]
        [Route("Confirmation/{id:guid}")]
        public async Task<IActionResult> Confirmation(Guid id)
        {
            var donation = await _donationService.GetDonationByIdAsync(id);
            if (donation == null)
            {
                return NotFound();
            }
            return View(donation);
        }

        // Route for Donation Reports
        [HttpGet("Reports")]
        public async Task<IActionResult> Reports(string search)
        {
            var donations = await _donationService.GetDonationsAsync();

            if (!string.IsNullOrEmpty(search))
            {
                donations = donations.Where(d => d.DonorName.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            return View("~/Views/Donation/Reports/Reports.cshtml", donations);
        }

        // Display donation receipt for printing
        [HttpGet]
        [Route("PrintReceipt/{id:guid}")]
        public async Task<IActionResult> PrintReceipt(Guid id)
        {
            var donation = await _donationService.GetDonationByIdAsync(id);
            if (donation == null)
            {
                return NotFound();
            }
            return View(donation);
        }

        // Edit Donation
        [Authorize(Roles = "Admin")]
        [HttpGet("edit/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var donation = await _donationService.GetDonationByIdAsync(id);
            if (donation == null)
            {
                return NotFound();
            }

            var ledgerAccounts = await _context.LedgerAccounts.ToListAsync();
            ViewBag.LedgerAccounts = new SelectList(ledgerAccounts, "Id", "AccountName", donation.LedgerAccountId);

            return View(donation);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,DonorName,Amount,DonationCategory,DonationType,Date,Phone,City,State,Country,LedgerAccountId")] Donation donation)
        {
            if (id != donation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var ledgerAccount = await _context.LedgerAccounts.FindAsync(donation.LedgerAccountId);
                    if (ledgerAccount == null)
                    {
                        _logger.LogWarning("The selected ledger account does not exist. LedgerAccountId: {LedgerAccountId}", donation.LedgerAccountId);
                        ModelState.AddModelError("LedgerAccountId", "The selected ledger account does not exist.");
                        return View(donation);
                    }

                    // Fetch the original donation, including associated CashTransaction and GeneralLedgerEntry
                    var originalDonation = await _context.Donations
                        .Include(d => d.CashTransactions)
                        .Include(d => d.GeneralLedgerEntries)
                        .FirstOrDefaultAsync(d => d.Id == id);

                    if (originalDonation != null)
                    {
                        // Update the donation
                        originalDonation.DonorName = donation.DonorName;
                        originalDonation.Amount = donation.Amount;
                        originalDonation.DonationCategory = donation.DonationCategory;
                        originalDonation.DonationType = donation.DonationType;
                        originalDonation.Date = donation.Date;
                        originalDonation.LedgerAccountId = donation.LedgerAccountId;

                        // Update CashTransaction(s)
                        if (originalDonation.CashTransactions != null && originalDonation.CashTransactions.Any())
                        {
                            foreach (var cashTransaction in originalDonation.CashTransactions)
                            {
                                cashTransaction.Amount = (decimal)donation.Amount;
                                cashTransaction.Description = $"Donation from {donation.DonorName}";
                                cashTransaction.LedgerAccountId = donation.LedgerAccountId;
                            }
                        }

                        // Update GeneralLedgerEntry
                        if (originalDonation.GeneralLedgerEntries != null && originalDonation.GeneralLedgerEntries.Any())
                        {
                            foreach (var ledgerEntry in originalDonation.GeneralLedgerEntries)
                            {
                                ledgerEntry.Credit = (decimal)donation.Amount;
                                ledgerEntry.Description = $"Donation entry from {donation.DonorName}";
                                ledgerEntry.LedgerAccountId = donation.LedgerAccountId;
                            }
                        }

                        await _context.SaveChangesAsync();
                    }

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    _logger.LogError(ex, "Error occurred while updating donation with ID {Id}", id);

                    if (!await DonationExists(donation.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            var ledgerAccounts = await _context.LedgerAccounts.ToListAsync();
            ViewBag.LedgerAccounts = new SelectList(ledgerAccounts, "Id", "AccountName", donation.LedgerAccountId);

            return View(donation);
        }


        // Delete Donation
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("Delete/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var donation = await _donationService.GetDonationByIdAsync(id);
            if (donation == null)
            {
                return NotFound();
            }
            return View(donation);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("DeleteConfirmed/{id}")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var donation = await _context.Donations
                .Include(d => d.CashTransactions)
                .Include(d => d.GeneralLedgerEntries)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (donation != null)
            {
                if (donation.GeneralLedgerEntries.Any())
                {
                    _context.GeneralLedgerEntries.RemoveRange(donation.GeneralLedgerEntries);
                }

                if (donation.CashTransactions != null && donation.CashTransactions.Any())
                {
                    _context.CashTransactions.RemoveRange(donation.CashTransactions);
                }

                _context.Donations.Remove(donation);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> DonationExists(Guid id)
        {
            return await _context.Donations.AnyAsync(e => e.Id == id);
        }
    }
}
