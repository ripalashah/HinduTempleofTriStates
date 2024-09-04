using HinduTempleofTriStates.Models;
using HinduTempleofTriStates.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HinduTempleofTriStates.Repositories
{
    public interface IDonationRepository
    {
        Task<IEnumerable<Donation>> GetAllDonationsAsync();
        Task<Donation?> GetDonationByIdAsync(Guid id);
        Task AddDonationAsync(Donation donation);
        Task UpdateDonationAsync(Donation donation);
        Task DeleteDonationAsync(Guid id);
    }
}
