using HinduTempleofTriStates.Data;
using HinduTempleofTriStates.Models;
using HinduTempleofTriStates.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace HinduTempleofTriStates.Services
{
    public class ReportService : IReportService
    {
        private readonly ApplicationDbContext _context;

        public ReportService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Cash Income and Expenses Report (Debit and Credit)
        public async Task<CashIncomeExpensesModel> GetCashIncomeExpensesAsync()
        {
            var startDate = new DateTime(DateTime.Now.Year, 1, 1); // Start of the current year
            var endDate = DateTime.UtcNow; // Current date and time

            // Fetch income (credit) and expense (debit) totals
            var totalIncome = await _context.CashTransactions
                .Where(t => t.TransactionType == TransactionType.Credit && t.Date >= startDate && t.Date <= endDate)
                .SumAsync(t => t.Amount);

            var totalExpense = await _context.CashTransactions
                .Where(t => t.TransactionType == TransactionType.Debit && t.Date >= startDate && t.Date <= endDate)
                .SumAsync(t => t.Amount);

            // Fetch all transactions for the given period
            var transactions = await _context.CashTransactions
                .Include(t => t.LedgerAccount) // Eagerly load related LedgerAccount
                .Where(t => t.Date >= startDate && t.Date <= endDate)
                .ToListAsync();

            return new CashIncomeExpensesModel
            {
                TotalIncome = totalIncome,
                TotalExpense = totalExpense,
                CashTransactions = transactions
            };
        }

        // General Ledger Report
        public async Task<GeneralLedgerModel> GenerateGeneralLedgerAsync()
        {
            var ledger = new GeneralLedgerModel
            {
                // Map to the GeneralLedgerEntryModel ViewModel
                LedgerEntries = await _context.LedgerAccounts
                    .Include(a => a.GeneralLedgerEntries) // Eagerly load related GeneralLedgerEntries
                    .Select(account => new GeneralLedgerEntryModel
                    {
                        AccountName = account.AccountName,
                        Debit = account.GeneralLedgerEntries != null
                            ? account.GeneralLedgerEntries
                                .Where(e => e.Debit > 0)
                                .Sum(e => e.Debit)
                            : 0,
                        Credit = account.GeneralLedgerEntries != null
                            ? account.GeneralLedgerEntries
                                .Where(e => e.Credit > 0)
                                .Sum(e => e.Credit)
                            : 0
                    })
                    .ToListAsync()

            };

            ledger.TotalDebits = ledger.LedgerEntries.Sum(e => e.Debit);
            ledger.TotalCredits = ledger.LedgerEntries.Sum(e => e.Credit);

            return ledger;
        }

        // Profit and Loss Report
        public async Task<ProfitLossModel> GenerateProfitLossAsync(DateTime startDate, DateTime endDate)
        {
            // Fetch the cash transactions between the startDate and endDate
            var transactions = await _context.CashTransactions
                .Where(t => t.Date >= startDate && t.Date <= endDate)
                .ToListAsync();

            // Calculate total income (credits)
            var totalIncome = transactions
                .Where(t => t.TransactionType == TransactionType.Credit)
                .Sum(t => t.Amount);

            // Calculate total expenses (debits)
            var totalExpenses = transactions
                .Where(t => t.TransactionType == TransactionType.Debit)
                .Sum(t => t.Amount);

            // Map the transactions to the ProfitLossItem (Model)
            var profitLossItems = transactions.Select(t => new ProfitLossItem
            {
                Description = t.Description,
                Amount = t.Amount,
                IsIncome = t.TransactionType == TransactionType.Credit // true for income (credit), false for expense (debit)
            }).ToList();

            // Return the populated ProfitLossModel
            return new ProfitLossModel
            {
                TotalIncome = totalIncome,
                TotalExpenses = totalExpenses,
                ProfitLossItems = profitLossItems
            };
        }

        // Trial Balance Report
        public async Task<TrialBalanceModel> GenerateTrialBalanceAsync()
        {
            var trialBalanceAccounts = await _context.LedgerAccounts
                .Include(a => a.CashTransactions) // Eagerly load related CashTransactions
                .Select(account => new TrialBalanceAccount
                {
                    AccountName = account.AccountName,
                    DebitTotal = account.CashTransactions != null
                        ? account.CashTransactions
                            .Where(t => t.TransactionType == TransactionType.Debit)
                            .Sum(t => t.Amount)
                        : 0,
                    CreditTotal = account.CashTransactions != null
                        ? account.CashTransactions
                            .Where(t => t.TransactionType == TransactionType.Credit)
                            .Sum(t => t.Amount)
                        : 0,
                    NetBalance = account.CashTransactions != null
                        ? account.CashTransactions
                            .Sum(t => t.TransactionType == TransactionType.Credit ? t.Amount : -t.Amount)
                        : 0
                })
                .ToListAsync();

            return new TrialBalanceModel
            {
                TrialBalanceAccounts = trialBalanceAccounts
            };
        }

    }
}
