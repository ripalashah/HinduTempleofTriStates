using HinduTempleofTriStates.Data;
using HinduTempleofTriStates.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HinduTempleofTriStates.Services
{
    public class FinancialReportService
    {
        private readonly ApplicationDbContext _context;

        public FinancialReportService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Method to get all cash transactions
        public async Task<List<CashTransaction>> GetCashTransactionsAsync()
        {
            return await _context.CashTransactions.ToListAsync();
        }

        // Method to calculate total income from a list of cash transactions
        public decimal CalculateTotalIncome(List<CashTransaction> cashTransactions)
        {
            return cashTransactions.Where(ct => ct.Income > 0).Sum(ct => ct.Income);
        }

        // Method to calculate total expenses from a list of cash transactions
        public decimal CalculateTotalExpenses(List<CashTransaction> cashTransactions)
        {
            return cashTransactions.Where(ct => ct.Expense > 0).Sum(ct => ct.Expense);
        }
        // Method for generating the Trial Balance report
        public async Task<TrialBalanceModel> GetTrialBalanceAsync()
        {
            var accounts = await _context.LedgerAccounts.ToListAsync();
            var trialBalanceModel = new TrialBalanceModel();

            foreach (var account in accounts)
            {
                var debitTotal = await _context.Transactions
                    .Where(t => t.LedgerAccountId == account.Id && t.TransactionType == TransactionType.Debit)
                    .SumAsync(t => t.Amount);

                var creditTotal = await _context.Transactions
                    .Where(t => t.LedgerAccountId == account.Id && t.TransactionType == TransactionType.Credit)
                    .SumAsync(t => t.Amount);

                trialBalanceModel.TrialBalanceAccounts.Add(new TrialBalanceAccount
                {
                    Id = account.Id,
                    AccountName = account.AccountName,
                    DebitBalance = debitTotal,
                    CreditBalance = creditTotal
                });
            }
            
            return trialBalanceModel;
        }

        // Method for generating the General Ledger report
        public async Task<GeneralLedgerModel> GetGeneralLedgerAsync()
        {
            var generalLedgerEntries = await _context.GeneralLedgerEntries
                .Include(gl => gl.LedgerAccount) // Include the LedgerAccount for each entry
                .Select(gl => new GeneralLedgerEntryModel
                {
                    Date = gl.Date,
                    Description = gl.Description,
                    AccountName = gl.LedgerAccount.AccountName,
                    Debit = gl.Debit,
                    Credit = gl.Credit,
                }).ToListAsync();

            var totalDebit = generalLedgerEntries.Sum(e => e.Debit);
            var totalCredit = generalLedgerEntries.Sum(e => e.Credit);
            var totalBalance = totalDebit - totalCredit;

            return new GeneralLedgerModel
            {
                GeneralLedgerEntries = generalLedgerEntries,
                TotalDebit = totalDebit,
                TotalCredit = totalCredit,
                TotalBalance = totalBalance
            };
        }

        // Method for generating the Profit and Loss report
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
                    Amount = transaction.TransactionType == TransactionType.Credit ? transaction.Amount : -transaction.Amount
                });
            }

            model.TotalIncome = model.ProfitLossItems.Where(p => p.Amount > 0).Sum(p => p.Amount);
            model.TotalExpenses = model.ProfitLossItems.Where(p => p.Amount < 0).Sum(p => p.Amount);
            model.NetProfitLoss = model.TotalIncome + model.TotalExpenses;

            return model;
        }

        // Additional methods for generating other reports can be added here
    }
}
