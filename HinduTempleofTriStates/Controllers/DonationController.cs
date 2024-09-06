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
    public class DonationController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IDonationService _donationService;  // Use interface for better abstraction
        private readonly ILogger<DonationController> _logger;
        
        public DonationController(ApplicationDbContext context, IDonationService donationService, ILogger<DonationController> logger)
        {
            _context = context;
            _donationService = donationService;
            _logger = logger;
        }

        // List all donations
        public async Task<IActionResult> Index()
        {
            try
            {
                var donations = await _donationService.GetDonationsAsync();
                return View(donations);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching donations");
                return BadRequest("Unable to load donations. Please try again later.");
            }
        }

        // Display form to create a donation
        public IActionResult Create()
        {
            ViewData["LedgerAccountId"] = new SelectList(_context.LedgerAccounts, "Id", "AccountName");
            return View();
        }

        // Handle the post request to create a new donation
        [HttpPost]
        [ValidateAntiForgeryToken]
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
            ViewData["LedgerAccountId"] = new SelectList(_context.LedgerAccounts, "Id", "AccountName", donation.LedgerAccountId);
            return View(donation);
        }

        // Display confirmation after successful donation
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
        public async Task<IActionResult> Edit(Guid id)
        {
            var donation = await _donationService.GetDonationByIdAsync(id);
            if (donation == null)
            {
                return NotFound();
            }
            ViewData["LedgerAccountId"] = new SelectList(_context.LedgerAccounts, "Id", "AccountName", donation.LedgerAccountId);
            return View(donation);
        }

        // Handle the post request to update the donation
        [HttpPost]
        [ValidateAntiForgeryToken]
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
            ViewData["LedgerAccountId"] = new SelectList(_context.LedgerAccounts, "Id", "AccountName", donation.LedgerAccountId);
            return View(donation);
        }

        // Display donation receipt for printing
        public async Task<IActionResult> PrintReceipt(Guid id)
        {
            var donation = await _donationService.GetDonationByIdAsync(id);
            if (donation == null)
            {
                return NotFound();
            }
            return View(donation);
        }

        // Display confirmation page to delete a donation
        public async Task<IActionResult> Delete(Guid id)
        {
            var donation = await _donationService.GetDonationByIdAsync(id);
            if (donation == null)
            {
                return NotFound();
            }
            return View(donation);
        }

        // Handle the post request to delete a donation
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                var donation = await _donationService.GetDonationByIdAsync(id);
                if (donation == null)
                {
                    return NotFound();
                }

                await _donationService.DeleteDonationAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting donation");
                return BadRequest("An error occurred while deleting the donation.");
            }
        }

        // Check if a donation exists by ID
        private bool DonationExists(Guid id)
        {
            return _context.Donations.Any(e => e.Id == id);
        }
    }
}
