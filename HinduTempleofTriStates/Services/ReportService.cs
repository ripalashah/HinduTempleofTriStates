using HinduTempleofTriStates.Data;
using HinduTempleofTriStates.Models;
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

        // General Ledger Report
        public async Task<GeneralLedgerModel> GenerateGeneralLedgerAsync()
        {
            var generalLedgerEntries = await _context.GeneralLedgerEntries
                .Include(e => e.LedgerAccount)
                .ToListAsync();

            return new GeneralLedgerModel
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

        // Profit and Loss Report
        public async Task<ProfitLossModel> GenerateProfitLossAsync()
        {
            var profitLossItems = await _context.Transactions
                .Select(t => new ProfitLossItem
                {
                    Description = t.Description,
                    Amount = t.Debit > 0 ? t.Debit : -t.Credit
                }).ToListAsync();

            return new ProfitLossModel
            {
                ProfitLossItems = profitLossItems,
                TotalProfit = profitLossItems.Sum(p => p.Amount > 0 ? p.Amount : 0),
                TotalLoss = profitLossItems.Sum(p => p.Amount < 0 ? -p.Amount : 0)
            };
        }

        // Trial Balance Report
        public async Task<TrialBalanceModel> GenerateTrialBalanceAsync()
        {
            var trialBalanceAccounts = await _context.LedgerAccounts
                .Include(a => a.Transactions)
                .Select(account => new TrialBalanceAccount
                {
                    AccountName = account.AccountName,
                    DebitBalance = account.Transactions.Where(t => t.TransactionType == TransactionType.Debit).Sum(t => t.Amount),
                    CreditBalance = account.Transactions.Where(t => t.TransactionType == TransactionType.Credit).Sum(t => t.Amount)
                }).ToListAsync();

            return new TrialBalanceModel
            {
                TrialBalanceAccounts = trialBalanceAccounts
            };
        }

        // Cash Income and Expenses Report
        public async Task<CashIncomeExpensesModel> GetCashIncomeExpensesAsync()
        {
            var cashTransactions = await _context.CashTransactions.ToListAsync();

            return new CashIncomeExpensesModel
            {
                CashTransactions = cashTransactions
            };
        }
    }

}
