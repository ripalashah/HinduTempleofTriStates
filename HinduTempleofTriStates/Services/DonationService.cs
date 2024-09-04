using HinduTempleofTriStates.Data;
using HinduTempleofTriStates.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HinduTempleofTriStates.Services
{
    public class DonationService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<DonationService> _logger;

        public DonationService(ApplicationDbContext context, ILogger<DonationService> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Get all donations
        public async Task<List<Donation>> GetDonationsAsync()
        {
            try
            {
                return await _context.Donations.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching donations");
                return new List<Donation>();
            }
        }

        // Get a specific donation by ID
        public async Task<Donation?> GetDonationByIdAsync(Guid id)
        {
            try
            {
                return await _context.Donations.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error fetching donation with ID {id}");
                return null;
            }
        }

        // Add a new donation with validation
        public async Task<bool> AddDonationAsync(Donation donation)
        {
            if (ValidateDonation(donation))
            {
                try
                {
                    _context.Donations.Add(donation);
                    await _context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error adding donation");
                }
            }
            return false;
        }

        // Update an existing donation with validation
        public async Task<bool> UpdateDonationAsync(Donation donation)
        {
            if (ValidateDonation(donation))
            {
                try
                {
                    _context.Donations.Update(donation);
                    await _context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error updating donation");
                }
            }
            return false;
        }

        // Delete a donation by ID
        public async Task<bool> DeleteDonationAsync(Guid id)
        {
            var donation = await GetDonationByIdAsync(id);
            if (donation != null)
            {
                try
                {
                    _context.Donations.Remove(donation);
                    await _context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error deleting donation");
                }
            }
            return false;
        }

        // Validate donation data
        private bool ValidateDonation(Donation donation)
        {
            return donation != null && !string.IsNullOrWhiteSpace(donation.DonorName) && donation.Amount > 0;
        }
    }
}
