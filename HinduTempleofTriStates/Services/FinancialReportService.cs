using HinduTempleofTriStates.Data;
using HinduTempleofTriStates.Models;
using HinduTempleofTriStates.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HinduTempleofTriStates.Services
{
    public class FinancialReportService : IFinancialReportService
    {
        private readonly ApplicationDbContext _context;
        private readonly ICashTransactionRepository _cashTransactionRepository;
        private readonly ILogger<FinancialReportService> _logger;

        // Constructor to inject the required services
        public FinancialReportService(ApplicationDbContext context, ICashTransactionRepository cashTransactionRepository, ILogger<FinancialReportService> logger)
        {
            _context = context;
            _cashTransactionRepository = cashTransactionRepository;
            _logger = logger;
        }

        // Method to generate Trial Balance Report
        public async Task<TrialBalanceModel> GetTrialBalanceAsync()
        {
            var trialBalanceAccounts = await _context.LedgerAccounts
                .Include(a => a.Transactions)
                .Select(account => new TrialBalanceAccount
                {
                    AccountName = account.AccountName,
                    DebitTotal = account.Transactions
                        .Where(t => t.TransactionType == TransactionType.Debit)
                        .Sum(t => (decimal?)t.Amount) ?? 0M, // Sum of debits
                    CreditTotal = account.Transactions
                        .Where(t => t.TransactionType == TransactionType.Credit)
                        .Sum(t => (decimal?)t.Amount) ?? 0M, // Sum of credits
                    NetBalance = account.Transactions
                        .Sum(t => t.TransactionType == TransactionType.Credit ? (decimal?)t.Amount : -(decimal?)t.Amount) ?? 0M // Corrected balance calculation
                }).ToListAsync();

            return new TrialBalanceModel
            {
                TrialBalanceAccounts = trialBalanceAccounts,
                TotalDebits = trialBalanceAccounts.Sum(a => a.DebitTotal),
                TotalCredits = trialBalanceAccounts.Sum(a => a.CreditTotal),
                NetBalance = trialBalanceAccounts.Sum(a => a.NetBalance)
            };
        }


        // Method to generate General Ledger Report
        public async Task<GeneralLedgerModel> GetGeneralLedgerAsync()
        {
            // Fetch all general ledger entries and include related ledger accounts
            var generalLedgerEntries = await _context.GeneralLedgerEntries
                .Include(e => e.LedgerAccount)
                .ToListAsync();

            // Return a GeneralLedgerModel, which contains a list of GeneralLedgerEntryModel
            return new GeneralLedgerModel
            {
                LedgerEntries = generalLedgerEntries.Select(entry => new GeneralLedgerEntryModel
                {
                    Date = entry.Date,
                    Description = entry.Description,
                    AccountName = entry.LedgerAccount.AccountName, // Assuming LedgerAccount has AccountName
                    Debit = entry.Debit,
                    Credit = entry.Credit
                }).ToList(),
                TotalDebits = generalLedgerEntries.Sum(e => e.Debit),
                TotalCredits = generalLedgerEntries.Sum(e => e.Credit)
            };
        }


        // Method to get all Cash Transactions
        public async Task<List<CashTransaction>> GetCashTransactionsAsync()
        {
            return await _context.CashTransactions.ToListAsync();
        }

        // Method to calculate total income
        public decimal CalculateTotalIncome(List<CashTransaction> transactions)
        {
            return transactions.Where(t => t.Type == CashTransactionType.Credit).Sum(t => t.Amount);
        }

        // Method to calculate total expenses
        public decimal CalculateTotalExpenses(List<CashTransaction> transactions)
        {
            return transactions.Where(t => t.Type == CashTransactionType.Debit).Sum(t => t.Amount);
        }

        // Method to generate Cash Income and Expenses Report
        public async Task<CashIncomeExpensesModel> GetCashIncomeExpensesAsync()
        {
            // Define start and end date, e.g., the start of the current year to now
            var startDate = new DateTime(DateTime.Now.Year, 1, 1);  // Start of the current year
            var endDate = DateTime.UtcNow;  // Current date and time

            // Fetch all cash transactions within the date range
            var transactions = await _context.CashTransactions
                .Where(t => t.Date >= startDate && t.Date <= endDate)
                .ToListAsync();

            // Calculate total income (credit transactions)
            var totalIncome = transactions
                .Where(t => t.TransactionType == TransactionType.Credit)
                .Sum(t => t.Amount);

            // Calculate total expenses (debit transactions)
            var totalExpense = transactions
                .Where(t => t.TransactionType == TransactionType.Debit)
                .Sum(t => t.Amount);

            // Log information about the report
            _logger.LogInformation("Calculated total income: {TotalIncome}, total expense: {TotalExpense}, net balance: {NetBalance}",
                                   totalIncome, totalExpense, totalIncome - totalExpense);

            // Return the Cash Income and Expenses model
            return new CashIncomeExpensesModel
            {
                CashTransactions = transactions,
                TotalIncome = totalIncome,
                TotalExpense = totalExpense
            };
        }

    }
}
