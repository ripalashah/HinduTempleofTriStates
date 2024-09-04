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

        // GET: Donation/Index
        public async Task<IActionResult> Index()
        {
            var donations = await _donationService.GetDonationsAsync();
            return View(donations);
        }

        // GET: Donation/Create
        public IActionResult Create()
        {
            ViewData["AccountId"] = new SelectList(_context.Accounts, "Id", "AccountName");
            return View();
        }

        // POST: Donation/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DonorName,Amount,DonationCategory,DonationType,Date,Phone,City,State,Country,AccountId")] Donation donation)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    donation.Id = Guid.NewGuid(); // Generate a new Guid for the donation
                    var account = await _context.Accounts.FindAsync(donation.AccountId);
                    if (account != null)
                    {
                        account.Balance += donation.Amount; // Update account balance
                        _context.Update(account);
                    }
                    _context.Add(donation);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Confirmation), new { id = donation.Id });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating donation");
                    // Optionally add a user-friendly message or redirect to an error page
                }
            }
            ViewData["AccountId"] = new SelectList(_context.Accounts, "Id", "AccountName", donation.AccountId);
            return View(donation);
        }

        // GET: Donation/Confirmation
        public async Task<IActionResult> Confirmation(Guid id)
        {
            var donation = await _donationService.GetDonationByIdAsync(id);
            if (donation == null)
            {
                return NotFound();
            }
            return View(donation);
        }

        // GET: Donation/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var donation = await _donationService.GetDonationByIdAsync(id);
            if (donation == null)
            {
                return NotFound();
            }
            ViewData["AccountId"] = new SelectList(_context.Accounts, "Id", "AccountName", donation.AccountId);
            return View(donation);
        }

        // POST: Donation/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,DonorName,Amount,DonationCategory,DonationType,Date,Phone,City,State,Country,AccountId")] Donation donation)
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
                        var account = await _context.Accounts.FindAsync(existingDonation.AccountId);
                        if (account != null)
                        {
                            account.Balance -= existingDonation.Amount; // Revert previous amount
                            _context.Update(account);
                        }

                        account = await _context.Accounts.FindAsync(donation.AccountId);
                        if (account != null)
                        {
                            account.Balance += donation.Amount; // Update new amount
                            _context.Update(account);
                        }
                    }

                    _context.Update(donation);
                    await _context.SaveChangesAsync();
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
            ViewData["AccountId"] = new SelectList(_context.Accounts, "Id", "AccountName", donation.AccountId);
            return View(donation);
        }

        // GET: Donation/PrintReceipt/5
        public async Task<IActionResult> PrintReceipt(Guid id)
        {
            var donation = await _donationService.GetDonationByIdAsync(id);
            if (donation == null)
            {
                return NotFound();
            }
            return View(donation);
        }

        // GET: Donation/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var donation = await _donationService.GetDonationByIdAsync(id);
            if (donation == null)
            {
                return NotFound();
            }
            return View(donation);
        }

        // POST: Donation/Delete/5
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
