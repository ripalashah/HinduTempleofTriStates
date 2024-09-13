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
                // Sum debits for this account
                var debit = await _context.Transactions
                    .Where(t => t.LedgerAccountId == account.Id && t.TransactionType == TransactionType.Debit)
                    .SumAsync(t => (decimal?)t.Amount) ?? 0m;

                // Sum credits for this account
                var credit = await _context.Transactions
                    .Where(t => t.LedgerAccountId == account.Id && t.TransactionType == TransactionType.Credit)
                    .SumAsync(t => (decimal?)t.Amount) ?? 0m;

                // Add the account to the trial balance model
                model.TrialBalanceAccounts.Add(new TrialBalanceAccount
                {
                    Id = account.Id,
                    AccountName = account.AccountName,
                    DebitTotal = debit,
                    CreditTotal = credit
                });
            }

            // Calculate total debits and credits for the trial balance
            model.TotalDebits = model.TrialBalanceAccounts.Sum(a => a.DebitTotal);
            model.TotalCredits = model.TrialBalanceAccounts.Sum(a => a.CreditTotal);

            return model;
        }
    }
}
