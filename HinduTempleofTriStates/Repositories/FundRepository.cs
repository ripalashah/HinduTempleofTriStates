using HinduTempleofTriStates.Data;
using HinduTempleofTriStates.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HinduTempleofTriStates.Repositories
{
    public class FundRepository : IFundRepository
    {
        private readonly ApplicationDbContext _context;

        public FundRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Fund>> GetFundsAsync()
        {
            return await _context.Funds.ToListAsync();
        }

        public async Task<Fund?> GetFundByIdAsync(Guid id)
        {
            return await _context.Funds.FindAsync(id);
        }

        public async Task AddFundAsync(Fund fund)
        {
            _context.Funds.Add(fund);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateFundAsync(Fund fund)
        {
            _context.Funds.Update(fund);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFundAsync(Guid id)
        {
            var fund = await _context.Funds.FindAsync(id);
            if (fund != null)
            {
                _context.Funds.Remove(fund);
                await _context.SaveChangesAsync();
            }
        }
    }
}
