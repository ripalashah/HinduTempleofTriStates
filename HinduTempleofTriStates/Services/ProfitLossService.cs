using HinduTempleofTriStates.Data;
using HinduTempleofTriStates.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HinduTempleofTriStates.Services
{
    public class ProfitLossService
    {
        private readonly ApplicationDbContext _context;

        public ProfitLossService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ProfitLossModel> GenerateProfitLossReportAsync()
        {
            var donations = await _context.Donations.ToListAsync();
            var transactions = await _context.Transactions.ToListAsync();

            var model = new ProfitLossModel();

            foreach (var donation in donations)
            {
                model.ProfitLossItems.Add(new ProfitLossItem
                {
                    Description = $"Donation from {donation.DonorName}",
                    Amount = (decimal)donation.Amount
                });
            }

            foreach (var transaction in transactions)
            {
                model.ProfitLossItems.Add(new ProfitLossItem
                {
                    Description = transaction.Description,
                    Amount = transaction.Type == TransactionType.Credit ? transaction.Amount : -transaction.Amount
                });
            }

            return model;
        }
    }
}
