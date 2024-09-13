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

            // Summing donation amounts for profit
            foreach (var donation in donations)
            {
                model.ProfitLossItems.Add(new ProfitLossItem
                {
                    Description = $"Donation from {donation.DonorName}",
                    Amount = (decimal)donation.Amount // Ensure this is a decimal type in your model
                });
            }

            // Handling transactions for profit and loss
            foreach (var transaction in transactions)
            {
                var amount = transaction.TransactionType == TransactionType.Credit ? transaction.Amount : -transaction.Amount;
                model.ProfitLossItems.Add(new ProfitLossItem
                {
                    Description = transaction.Description,
                    Amount = amount
                });
            }

            // Calculate totals (if required in the model)
            model.TotalProfit = model.ProfitLossItems.Where(x => x.Amount > 0).Sum(x => x.Amount);
            model.TotalLoss = model.ProfitLossItems.Where(x => x.Amount < 0).Sum(x => x.Amount);

            return model;
        }
    }
}
