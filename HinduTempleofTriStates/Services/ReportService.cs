using HinduTempleofTriStates.Data;
using HinduTempleofTriStates.Models;
using HinduTempleofTriStates.Views.Reports;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HinduTempleofTriStates.Services
{
    public class ReportService : IReportService
    {
        private readonly ApplicationDbContext _context;

        public ReportService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Generate General Ledger Report
        public async Task<Models.GeneralLedgerModel> GenerateGeneralLedgerAsync()
        {
            var generalLedgerEntries = await _context.GeneralLedgerEntries
                .Include(e => e.LedgerAccount)
                .ToListAsync();

            return new Models.GeneralLedgerModel
            {
                GeneralLedgerEntries = generalLedgerEntries.Select(entry => new GeneralLedgerEntryModel
                {
                    Date = entry.Date,
                    Description = entry.Description,
                    AccountName = entry.LedgerAccount.AccountName,
                    Debit = entry.Debit,
                    Credit = entry.Credit
                }).ToList(),
                TotalDebit = generalLedgerEntries.Sum(e => e.Debit),
                TotalCredit = generalLedgerEntries.Sum(e => e.Credit),
                TotalBalance = generalLedgerEntries.Sum(e => e.Debit - e.Credit)
            };
        }

        // Generate Profit and Loss Report
        public async Task<Models.ProfitLossModel> GenerateProfitLossAsync()
        {
            var profitLossItems = await _context.Transactions
                .Select(t => new ProfitLossItem
                {
                    Description = t.Description,
                    Amount = t.Debit > 0 ? t.Debit : -t.Credit
                }).ToListAsync();

            return new Models.ProfitLossModel
            {
                ProfitLossItems = profitLossItems,
                TotalProfit = profitLossItems.Sum(p => p.Amount > 0 ? p.Amount : 0),
                TotalLoss = profitLossItems.Sum(p => p.Amount < 0 ? -p.Amount : 0)
            };
        }

        // Generate Trial Balance Report
        public async Task<Models.TrialBalanceModel> GenerateTrialBalanceAsync()
        {
            var trialBalanceAccounts = await _context.LedgerAccounts
                .Select(account => new TrialBalanceAccount
                {
                    AccountName = account.AccountName,
                    DebitBalance = account.Transactions.Where(t => t.Debit > 0).Sum(t => t.Debit),
                    CreditBalance = account.Transactions.Where(t => t.Credit > 0).Sum(t => t.Credit)
                }).ToListAsync();

            return new Models.TrialBalanceModel
            {
                TrialBalanceAccounts = trialBalanceAccounts
                
            };
        }
    }
}
