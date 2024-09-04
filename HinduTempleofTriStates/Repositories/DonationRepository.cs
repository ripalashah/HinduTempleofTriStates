using HinduTempleofTriStates.Data;
using HinduTempleofTriStates.Models;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<Donation>> GetAllDonationsAsync()
        {
            return await _context.Donations.ToListAsync();
        }

        public async Task<Donation?> GetDonationByIdAsync(Guid id)
        {
            return await _context.Donations.FindAsync(id);
        }

        public async Task AddDonationAsync(Donation donation)
        {
            if (donation == null)
            {
                throw new ArgumentNullException(nameof(donation));
            }

            _context.Donations.Add(donation);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDonationAsync(Donation donation)
        {
            if (donation == null)
            {
                throw new ArgumentNullException(nameof(donation));
            }

            var existingDonation = await _context.Donations.FindAsync(donation.Id);
            if (existingDonation == null)
            {
                throw new KeyNotFoundException($"Donation with ID {donation.Id} not found.");
            }

            _context.Entry(existingDonation).CurrentValues.SetValues(donation);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDonationAsync(Guid id)
        {
            var donation = await GetDonationByIdAsync(id);
            if (donation != null)
            {
                _context.Donations.Remove(donation);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"Donation with ID {id} not found.");
            }
        }
    }
}
