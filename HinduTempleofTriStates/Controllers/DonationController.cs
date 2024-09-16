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
        private readonly EmailService _emailService; // Inject email service
        private readonly ILogger<DonationController> _logger;
        private readonly LedgerService _ledgerService;
        private readonly QuickBooksService _quickBooksService;

        public DonationController(ApplicationDbContext context, LedgerService ledgerService, IDonationService donationService, EmailService emailService, QuickBooksService quickBooksService, ILogger<DonationController> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _ledgerService = ledgerService;
            _donationService = donationService ?? throw new ArgumentNullException(nameof(donationService));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _quickBooksService = quickBooksService ?? throw new ArgumentNullException(nameof(quickBooksService));  // Inject QuickBooksService
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
        // Method to generate receipt number
        private async Task<string> GenerateReceiptNumberAsync()
        {
            var lastDonation = await _context.Donations
                .OrderByDescending(d => d.ReceiptNumber)
                .FirstOrDefaultAsync();

            if (lastDonation != null && !string.IsNullOrEmpty(lastDonation.ReceiptNumber))
            {
                var lastReceiptNumber = int.Parse(lastDonation.ReceiptNumber.Substring(1));
                var newReceiptNumber = lastReceiptNumber + 1;
                return $"D{newReceiptNumber:D4}";
            }

            return "D0001";
        }
        // Display form to create a donation
        [HttpGet]
        [Route("Create")]
        public async Task<IActionResult> Create()
        {
            try
            {
                var ledgerAccounts = await _context.LedgerAccounts
                    .Where(l => !l.IsDeleted) // Filter out deleted ledger accounts
                    .ToListAsync();

                if (ledgerAccounts == null || !ledgerAccounts.Any())
                {
                    // Handle the case where there are no LedgerAccounts
                    _logger.LogWarning("No ledger accounts available.");
                    ModelState.AddModelError(string.Empty, "No ledger accounts available. Please create a ledger account first.");
                    return View(new Donation());
                }

                ViewBag.LedgerAccounts = new SelectList(ledgerAccounts, "Id", "AccountName");
                return View(new Donation());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching ledger accounts for donation creation.");
                ModelState.AddModelError(string.Empty, "Error loading ledger accounts. Please try again later.");
                return View(new Donation());
            }
        }


        // Handle the post request to create a new donation
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
                    // Generate and assign the receipt number
                    donation.ReceiptNumber = await GenerateReceiptNumberAsync();
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
                   

                    // Create corresponding GeneralLedgerEntry and CashTransaction
                    var ledgerEntry = new GeneralLedgerEntry
                    {
                        Id = Guid.NewGuid(),
                        DonationId = donation.Id,
                        Date = DateTime.UtcNow,
                        Description = $"Donation entry from {donation.DonorName}",
                        Debit = 0,
                        Credit = (decimal)donation.Amount,
                        LedgerAccountId = donation.LedgerAccountId ?? Guid.Empty,
                    };

                    var cashTransaction = new CashTransaction
                    {
                        Id = Guid.NewGuid(),
                        DonationId = donation.Id,
                        Amount = (decimal)donation.Amount,
                        Date = DateTime.UtcNow,
                        Description = $"Donation from {donation.DonorName}",
                        LedgerAccountId = donation.LedgerAccountId ?? Guid.Empty,
                        TransactionType = TransactionType.Credit // Ensure the donation is treated as a credit
                    };

                    await _donationService.AddDonationAsync(donation, true); // Pass 'true' for isAddition

                    // Use LedgerService to update the balance
                    await _ledgerService.UpdateLedgerAccountBalanceAsync(donation.LedgerAccountId, donation.Amount, true);
                    // Add and save GeneralLedgerEntry and CashTransaction
                    _context.GeneralLedgerEntries.Add(ledgerEntry);
                    _context.CashTransactions.Add(cashTransaction);
                    await _context.SaveChangesAsync();
                    // Sync donation with QuickBooks
                    await _quickBooksService.SyncDonationToQuickBooksAsync(donation);
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

        // Method to generate email body
        private string GenerateEmailBody(Donation donation)
        {
            return $@"
            <table style='width: 100%; border: 1px solid black; border-collapse: collapse; font-family: Arial, sans-serif;'>
                <thead>
                    <tr>
                        <th colspan='2' style='text-align: center; font-size: 14px;'>HINDU TEMPLE OF TRI STATE</th>
                    </tr>
                    <tr>
                        <td colspan='2' style='text-align: center; font-size: 10px;'>390 North Street White Plains, NY</td>
                    </tr>
                    <tr>
                        <td colspan='2' style='text-align: center; font-size: 10px;'>Phone: (914) 909-5550</td>
                    </tr>
                    <tr>
                        <td colspan='2' style='text-align: center; font-size: 10px;'>TAX ID: #26-4265251</td>
                    </tr>
                </thead>
                <tbody>
                    <tr><td colspan='2' style='border-top: 1px solid black;'></td></tr>
                    <tr>
                        <td><strong>Date:</strong></td><td>{donation.Date.ToShortDateString()}</td>
                    </tr>
                    <tr>
                        <td><strong>Receipt Number:</strong></td><td>{donation.ReceiptNumber}</td>
                    </tr>
                    <tr>
                        <td><strong>Amount:</strong></td><td>{donation.Amount.ToString("C")}</td>
                    </tr>
                    <tr>
                        <td><strong>Donation Type:</strong></td><td>{donation.DonationType}</td>
                    </tr>
                    <tr>
                        <td colspan='2' style='font-size: 10px;'>No goods or services were provided in exchange for this donation.</td>
                    </tr>
                    <tr><td colspan='2' style='text-align: center; font-size: 20px; color: lightgray;'>Thank You!</td></tr>
                </tbody>
            </table>";
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

        // Send donation receipt via email
        [HttpPost]
        [Route("SendReceipt")]
        public async Task<IActionResult> SendReceipt(Guid id, string email)
        {
            var donation = await _context.Donations.FindAsync(id);
            if (donation == null)
            {
                return NotFound();
            }

            // Prepare the email body (HTML format)
            var emailBody = $@"
            <table style='width: 100%; border: 1px solid black; border-collapse: collapse; font-family: Arial, sans-serif;'>
                <thead>
                    <tr>
                        <th colspan='2' style='text-align: center; font-size: 14px;'>HINDU TEMPLE OF TRI STATE</th>
                    </tr>
                    <tr>
                        <td colspan='2' style='text-align: center; font-size: 10px;'>390 North Street White Plains, NY</td>
                    </tr>
                    <tr>
                        <td colspan='2' style='text-align: center; font-size: 10px;'>Phone: (914) 909-5550</td>
                    </tr>
                    <tr>
                        <td colspan='2' style='text-align: center; font-size: 10px;'>TAX ID: #26-4265251</td>
                    </tr>
                </thead>
                <tbody>
                    <tr><td colspan='2' style='border-top: 1px solid black;'></td></tr>
                    <tr>
                        <td><strong>Date:</strong></td><td>{donation.Date.ToShortDateString()}</td>
                    </tr>
                    <tr>
                        <td><strong>Location:</strong></td><td>White Plains, NY</td>
                    </tr>
                    <tr>
                        <td><strong>Received By:</strong></td><td></td>
                    </tr>
                    <tr><td colspan='2' style='border-top: 1px solid black;'></td></tr>
                    <tr>
                        <td colspan='2' style='text-align: center; font-weight: bold;'>DONATIONS</td>
                    </tr>
                    <tr>
                        <td><strong>Receipt Number:</strong></td><td>{donation.ReceiptNumber}</td>
                    </tr>
                    <tr>
                        <td><strong>Amount:</strong></td><td>{donation.Amount.ToString("C")}</td>
                    </tr>
                    <tr>
                        <td><strong>Donation Type:</strong></td><td>{donation.DonationType}</td>
                    </tr>
                    <tr><td colspan='2' style='border-top: 1px solid black;'></td></tr>
                    <tr>
                        <td colspan='2' style='font-weight: bold;'>Donor Information</td>
                    </tr>
                    <tr>
                        <td><strong>Name:</strong></td><td>{donation.DonorName}</td>
                    </tr>
                    <tr>
                        <td><strong>City:</strong></td><td>{donation.City}</td>
                    </tr>
                    <tr>
                        <td><strong>State:</strong></td><td>{donation.State}</td>
                    </tr>
                    <tr>
                        <td><strong>Phone:</strong></td><td>{donation.Phone}</td>
                    </tr>
                    <tr><td colspan='2' style='border-top: 1px solid black;'></td></tr>
                    <tr>
                        <td colspan='2' style='font-size: 10px;'>This organization is a current and valid 501(c)(3) non-profit organization in accordance with the standards and regulations of the IRS.</td>
                    </tr>
                    <tr>
                        <td colspan='2' style='font-size: 10px;'>No goods or services were provided in exchange for this donation.</td>
                    </tr>
                    <tr>
                        <td colspan='2' style='text-align: center; font-size: 20px; color: lightgray;'>Thank You!</td>
                    </tr>
                </tbody>
            </table>";

            // Send the email
            await _emailService.SendEmailAsync(email, "Your Donation Receipt", emailBody);

            return RedirectToAction(nameof(Index));
        }

        // Edit Donation (Admin only)
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
                                cashTransaction.LedgerAccountId = donation.LedgerAccountId ?? Guid.Empty;
                            }
                        }

                        // Update GeneralLedgerEntry
                        if (originalDonation.GeneralLedgerEntries != null && originalDonation.GeneralLedgerEntries.Any())
                        {
                            foreach (var ledgerEntry in originalDonation.GeneralLedgerEntries)
                            {
                                ledgerEntry.Credit = (decimal)donation.Amount;
                                ledgerEntry.Description = $"Donation entry from {donation.DonorName}";
                                ledgerEntry.LedgerAccountId = donation.LedgerAccountId ?? Guid.Empty;
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

        // Delete Donation (Admin only)
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
                if (donation.GeneralLedgerEntries != null && donation.GeneralLedgerEntries.Any())
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
        [HttpPost]
        [Route("SyncDonation/{id}")]
        public async Task<IActionResult> SyncDonation(Guid id)
        {
            var donation = await _context.Donations.FindAsync(id);
            if (donation == null)
            {
                return NotFound();
            }

            // Call the sync method from QuickBooksService
            await _quickBooksService.SyncDonationToQuickBooksAsync(donation);

            return RedirectToAction(nameof(Index));
        }

        // Helper method to check if a donation exists
        private async Task<bool> DonationExists(Guid id)
        {
            return await _context.Donations.AnyAsync(e => e.Id == id);
        }
    }
}
