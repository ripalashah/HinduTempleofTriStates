using HinduTempleofTriStates.Data;
using HinduTempleofTriStates.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HinduTempleofTriStates.Services
{
    public class TrialBalanceService
    {
        private readonly ApplicationDbContext _context;

        public TrialBalanceService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TrialBalanceModel> GenerateTrialBalanceAsync()
        {
            var accounts = await _context.LedgerAccounts.ToListAsync();
            var model = new TrialBalanceModel();

            foreach (var account in accounts)
            {
                var debit = await _context.Transactions
                    .Where(t => t.LedgerAccountId == account.Id && t.TransactionType == TransactionType.Debit)
                    .SumAsync(t => t.Amount);

                var credit = await _context.Transactions
                    .Where(t => t.LedgerAccountId == account.Id && t.TransactionType == TransactionType.Credit)
                    .SumAsync(t => t.Amount);

                model.TrialBalanceAccounts.Add(new TrialBalanceAccount
                {
                    Id = account.Id,
                    AccountName = account.AccountName,
                    DebitTotal = debit,
                    CreditTotal = credit
                });
            }

            return model;
        }
    }
}
