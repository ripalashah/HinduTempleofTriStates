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

            // Fetch all transactions for the given period, ensuring each is only counted once.
            var transactions = await _context.CashTransactions
                .Include(t => t.LedgerAccount) // Eagerly load related LedgerAccount
                .Where(t => t.Date >= startDate && t.Date <= endDate)
                .ToListAsync();

            // Calculate income (credit) and expenses (debit) totals from the fetched transactions
            var totalIncome = transactions
                .Where(t => t.TransactionType == TransactionType.Credit)
                .Sum(t => t.Amount);

            var totalExpense = transactions
                .Where(t => t.TransactionType == TransactionType.Debit)
                .Sum(t => t.Amount);

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
            var ledgerEntries = await _context.LedgerAccounts
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
                .ToListAsync();

            var totalDebits = ledgerEntries.Sum(e => e.Debit);
            var totalCredits = ledgerEntries.Sum(e => e.Credit);

            return new GeneralLedgerModel
            {
                LedgerEntries = ledgerEntries,
                TotalDebits = totalDebits,
                TotalCredits = totalCredits
            };
        }

        // Profit and Loss Report
        public async Task<ProfitLossModel> GenerateProfitLossAsync(DateTime startDate, DateTime endDate)
        {
            // Fetch the cash transactions between the startDate and endDate
            var transactions = await _context.CashTransactions
                 .Include(t => t.LedgerAccount)
                .Where(t => t.Date >= startDate && t.Date <= endDate)
                .Distinct() // Ensure distinct transactions
                .ToListAsync();

            // Calculate total income (credits) and total expenses (debits)
            var totalIncome = transactions
                .Where(t => t.TransactionType == TransactionType.Credit)
                .Sum(t => t.Amount);

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
