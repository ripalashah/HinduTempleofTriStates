using HinduTempleofTriStates.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HinduTempleofTriStates.Repositories
{
    public interface IDonationRepository
    {
        Task<IEnumerable<Donation>> GetAllDonationsAsync();
        Task<Donation?> GetDonationByIdAsync(int id);
        Task AddDonationAsync(Donation donation);
        Task UpdateDonationAsync(Donation donation);
        Task DeleteDonationAsync(int id);
    }
}
