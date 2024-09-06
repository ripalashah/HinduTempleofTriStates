using HinduTempleofTriStates.Models;
using HinduTempleofTriStates.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HinduTempleofTriStates.Services
{
    public class DonationService
    {
        private readonly IDonationRepository _donationRepository;
        private readonly ILogger<DonationService> _logger;

        public DonationService(IDonationRepository donationRepository, ILogger<DonationService> logger)
        {
            _donationRepository = donationRepository;
            _logger = logger;
        }

        public async Task<List<Donation>> GetDonationsAsync()
        {
            try
            {
                return await _donationRepository.GetDonationsAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching donations");
                return new List<Donation>();
            }
        }

        public async Task<Donation?> GetDonationByIdAsync(Guid id)
        {
            try
            {
                return await _donationRepository.GetDonationByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error fetching donation with ID {id}");
                return null;
            }
        }

        public async Task<bool> AddDonationAsync(Donation donation)
        {
            if (ValidateDonation(donation))
            {
                try
                {
                    await _donationRepository.AddDonationAsync(donation);
                    return true;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error adding donation");
                }
            }
            return false;
        }

        public async Task<bool> UpdateDonationAsync(Donation donation)
        {
            if (ValidateDonation(donation))
            {
                try
                {
                    await _donationRepository.UpdateDonationAsync(donation);
                    return true;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error updating donation");
                }
            }
            return false;
        }

        public async Task<bool> DeleteDonationAsync(Guid id)
        {
            var donation = await GetDonationByIdAsync(id);
            if (donation != null)
            {
                try
                {
                    await _donationRepository.DeleteDonationAsync(donation);
                    return true;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error deleting donation");
                }
            }
            return false;
        }

        private bool ValidateDonation(Donation donation)
        {
            return donation != null
                   && !string.IsNullOrWhiteSpace(donation.DonorName)
                   && donation.Amount > 0
                   && donation.Date != DateTime.MinValue;
        }
    }
}
