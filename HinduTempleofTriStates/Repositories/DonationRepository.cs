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

        public async Task<List<Donation>> GetDonationsAsync()
        {
            return await _context.Donations.Include(d => d.LedgerAccount).ToListAsync();
        }

        public async Task<Donation?> GetDonationByIdAsync(Guid id)
        {
            return await _context.Donations.Include(d => d.LedgerAccount).FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task AddDonationAsync(Donation donation)
        {
            await _context.Donations.AddAsync(donation);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDonationAsync(Donation donation)
        {
            _context.Donations.Update(donation);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDonationAsync(Donation donation)
        {
            _context.Donations.Remove(donation);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DonationExistsAsync(Guid id)
        {
            return await _context.Donations.AnyAsync(e => e.Id == id);
        }

        public Task<IEnumerable<Donation>> GetAllDonationsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
