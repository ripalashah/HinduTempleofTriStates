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
        private readonly DonationService _donationService;
        private readonly ILogger<DonationController> _logger;

        public DonationController(ApplicationDbContext context, DonationService donationService, ILogger<DonationController> logger)
        {
            _context = context;
            _donationService = donationService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var donations = await _donationService.GetDonationsAsync();
            return View(donations);
        }

        public IActionResult Create()
        {
            ViewData["LedgerAccountId"] = new SelectList(_context.LedgerAccounts, "Id", "AccountName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DonorName,Amount,DonationCategory,DonationType,Date,Phone,City,State,Country,LedgerAccountId")] Donation donation)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    donation.Id = Guid.NewGuid();
                    var ledgerAccount = await _context.LedgerAccounts.FindAsync(donation.LedgerAccountId);

                    if (ledgerAccount != null)
                    {
                        _context.Add(donation);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Confirmation), new { id = donation.Id });
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating donation");
                }
            }
            ViewData["LedgerAccountId"] = new SelectList(_context.LedgerAccounts, "Id", "AccountName", donation.LedgerAccountId);
            return View(donation);
        }

        public async Task<IActionResult> Confirmation(Guid id)
        {
            var donation = await _donationService.GetDonationByIdAsync(id);
            if (donation == null)
            {
                return NotFound();
            }
            return View(donation);
        }

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
                    var existingDonation = await _context.Donations.AsNoTracking().FirstOrDefaultAsync(d => d.Id == id);

                    if (existingDonation != null)
                    {
                        var oldAccount = await _context.LedgerAccounts.FindAsync(existingDonation.LedgerAccountId);
                        var newAccount = await _context.LedgerAccounts.FindAsync(donation.LedgerAccountId);

                        if (oldAccount != null && newAccount != null)
                        {
                            _context.Update(donation);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(PrintReceipt), new { id = donation.Id });
                        }
                    }
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

        public async Task<IActionResult> PrintReceipt(Guid id)
        {
            var donation = await _donationService.GetDonationByIdAsync(id);
            if (donation == null)
            {
                return NotFound();
            }
            return View(donation);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var donation = await _donationService.GetDonationByIdAsync(id);
            if (donation == null)
            {
                return NotFound();
            }
            return View(donation);
        }

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

        private bool DonationExists(Guid id)
        {
            return _context.Donations.Any(e => e.Id == id);
        }
    }
}
