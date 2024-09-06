using HinduTempleofTriStates.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HinduTempleofTriStates.Repositories
{
    public interface IFundRepository
    {
        Task<IEnumerable<Fund>> GetFundsAsync();
        Task<Fund?> GetFundByIdAsync(Guid id);
        Task AddFundAsync(Fund fund);
        Task UpdateFundAsync(Fund fund);
        Task DeleteFundAsync(Guid id);
    }
}
