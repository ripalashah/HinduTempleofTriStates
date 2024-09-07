using HinduTempleofTriStates.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HinduTempleofTriStates.Services
{
    public interface IDonationService
    {
        Task<List<Donation>> GetDonationsAsync();
        Task<Donation?> GetDonationByIdAsync(Guid id);
        Task<bool> AddDonationAsync(Donation donation);
        Task<bool> UpdateDonationAsync(Donation donation);
        Task<bool> DeleteDonationAsync(Guid id);
    }
}
