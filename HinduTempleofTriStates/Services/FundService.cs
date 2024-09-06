using HinduTempleofTriStates.Models;
using HinduTempleofTriStates.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HinduTempleofTriStates.Services
{
    public class FundService
    {
        private readonly IFundRepository _fundRepository;

        public FundService(IFundRepository fundRepository)
        {
            _fundRepository = fundRepository;
        }

        public async Task<IEnumerable<Fund>> GetAllFundsAsync()
        {
            return await _fundRepository.GetFundsAsync();
        }

        public async Task<Fund?> GetFundByIdAsync(Guid id)
        {
            return await _fundRepository.GetFundByIdAsync(id);
        }

        public async Task AddFundAsync(Fund fund)
        {
            await _fundRepository.AddFundAsync(fund);
        }

        public async Task UpdateFundAsync(Fund fund)
        {
            await _fundRepository.UpdateFundAsync(fund);
        }

        public async Task DeleteFundAsync(Guid id)
        {
            await _fundRepository.DeleteFundAsync(id);
        }
    }
}
