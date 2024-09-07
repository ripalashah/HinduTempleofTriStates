using HinduTempleofTriStates.Data;
using HinduTempleofTriStates.Models;
using HinduTempleofTriStates.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HinduTempleofTriStates.Controllers
{
    [Route("[controller]")]
    public class DonationController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IDonationService _donationService;
        private readonly ILogger<DonationController> _logger;

        public DonationController(ApplicationDbContext context, IDonationService donationService, ILogger<DonationController> logger)
        {
            _context = context;
            _donationService = donationService;
            _logger = logger;
        }

        // List all donations
        [HttpGet]
        [Route("")]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            try
            {
                var donations = await _donationService.GetDonationsAsync();
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
        public async Task<IActionResult> Create([Bind("DonorName,Amount,DonationCategory,DonationType,Date,Phone,City,State,Country,LedgerAccountId")] Donation donation)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    donation.Id = Guid.NewGuid();  // Assign new GUID to donation
                    _context.Add(donation);  // Add donation to the context
                    await _context.SaveChangesAsync();  // Save changes to the database
                    return RedirectToAction(nameof(Confirmation), new { id = donation.Id });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating donation");
                    ModelState.AddModelError("", "Unable to create donation. Please try again.");
                }
            }

            // Reload the LedgerAccounts list in case of error
            var ledgerAccounts = await _context.LedgerAccounts.ToListAsync();
            ViewBag.LedgerAccounts = new SelectList(ledgerAccounts, "Id", "AccountName", donation.LedgerAccountId);

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

        // Display form to edit an existing donation
        [HttpGet]
        [Route("Edit/{id:guid}")]
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

        // Handle the post request to update the donation
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
                    _context.Update(donation);  // Update the donation in the context
                    await _context.SaveChangesAsync();  // Save the updated donation
                    return RedirectToAction(nameof(PrintReceipt), new { id = donation.Id });
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    _logger.LogError(ex, "Error updating donation");
                    if (!DonationExists(donation.Id))
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

        // Check if a donation exists by ID
        private bool DonationExists(Guid id)
        {
            return _context.Donations.Any(e => e.Id == id);
        }
    }
}
