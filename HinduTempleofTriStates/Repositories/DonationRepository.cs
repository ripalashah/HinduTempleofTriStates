using HinduTempleofTriStates.Data;
using HinduTempleofTriStates.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HinduTempleofTriStates.Repositories
{
    public class DonationRepository : IDonationRepository
    {
        private readonly ApplicationDbContext _context;

        public DonationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all donations (simplified) - includes related LedgerAccount
        public async Task<List<Donation>> GetDonationsAsync()
        {
            return await _context.Donations
                                 .Include(d => d.LedgerAccount) // Include related LedgerAccount
                                 .ToListAsync();
        }

        // Get a donation by its ID
        public async Task<Donation?> GetDonationByIdAsync(Guid id)
        {
            return await _context.Donations
                                 .Include(d => d.LedgerAccount) // Include related LedgerAccount
                                 .FirstOrDefaultAsync(d => d.Id == id);
        }

        // Add a new donation
        public async Task AddDonationAsync(Donation donation)
        {
            await _context.Donations.AddAsync(donation);
            await _context.SaveChangesAsync();
        }

        // Update an existing donation
        public async Task UpdateDonationAsync(Donation donation)
        {
            _context.Donations.Update(donation);
            await _context.SaveChangesAsync();
        }

        // Delete a donation
        public async Task DeleteDonationAsync(Donation donation)
        {
            _context.Donations.Remove(donation);
            await _context.SaveChangesAsync();
        }

        // Check if a donation exists by ID
        public async Task<bool> DonationExistsAsync(Guid id)
        {
            return await _context.Donations.AnyAsync(e => e.Id == id);
        }

        // Implementation for GetAllDonationsAsync (for fetching all donations)
        public async Task<IEnumerable<Donation>> GetAllDonationsAsync()
        {
            return await _context.Donations
                                 .Include(d => d.LedgerAccount) // Include LedgerAccount if required
                                 .ToListAsync();
        }
    }
}
