using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TempleManagementSystem.Data;
using TempleManagementSystem.Models;

namespace TempleManagementSystem.Controllers
{
    public class DonationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DonationController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Donation/Index
        public async Task<IActionResult> Index()
        {
            return View(await _context.Donations.ToListAsync());
        }

        // GET: Donation/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Donation/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DonorName,Amount,DonationCategory,DonationType,Date,Phone,City,State,Country")] Donation donation)
        {
            if (ModelState.IsValid)
            {
                donation.Id = Guid.NewGuid();
                _context.Add(donation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(donation);
        }

        // GET: Donation/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var donation = await _context.Donations.FindAsync(id);
            if (donation == null)
            {
                return NotFound();
            }
            return View(donation);
        }

        // POST: Donation/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,DonorName,Amount,DonationCategory,DonationType,Date,Phone,City,State,Country")] Donation donation)
        {
            if (id != donation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(donation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DonationExists(donation.Id))
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
            return View(donation);
        }

        private bool DonationExists(Guid id)
        {
            throw new NotImplementedException();
        }

        // GET: Donation/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var donation = await _context.Donations
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var donation = await _context.Donations.FindAsync(id);
            if (donation == null)
            {
                return NotFound();
            }

            _context.Donations.Remove(donation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
