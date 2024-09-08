using HinduTempleofTriStates.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HinduTempleofTriStates.Repositories
{
    public interface IDonationRepository
    {
        // Fetch all donations (list of donations)
        Task<List<Donation>> GetDonationsAsync();

        // Fetch all donations (general method for any use)
        Task<IEnumerable<Donation>> GetAllDonationsAsync();

        // Fetch donation by ID
        Task<Donation?> GetDonationByIdAsync(Guid id);

        // Add a new donation
        Task AddDonationAsync(Donation donation);

        // Update an existing donation
        Task UpdateDonationAsync(Donation donation);

        // Delete a donation
        Task DeleteDonationAsync(Donation donation);

        // Check if a donation exists by ID
        Task<bool> DonationExistsAsync(Guid id);
    }
}
