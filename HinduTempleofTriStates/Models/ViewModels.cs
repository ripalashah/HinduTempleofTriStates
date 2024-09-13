using HinduTempleofTriStates.Models;
using System;
using System.Collections.Generic;

namespace HinduTempleofTriStates.ViewModels
{
    // ViewModel for Cash Income and Expenses Report
    public class CashIncomeExpensesViewModel
    {
        public IEnumerable<CashTransaction>? CashTransactions { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; } // Total expenses
        public decimal NetBalance => TotalIncome - TotalExpense;

        // Additional properties if needed
    }

    // Assuming you have a CashTransaction model defined elsewhere
    public class CashTransactionViewModel
    {
        public DateTime Date { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public CashTransactionType Type { get; set; } // Enum with Credit or Debit
    }

    // ViewModel for General Ledger Report
    public class GeneralLedgerViewModel
    {
        public List<GeneralLedgerEntryModel> LedgerEntries { get; set; } = new List<GeneralLedgerEntryModel>();
        public decimal TotalDebits { get; set; }
        public decimal TotalCredits { get; set; }
        public decimal TotalBalance => TotalDebits - TotalCredits; // Calculated balance

        // This might be unnecessary unless used elsewhere
    }

    public class GeneralLedgerEntryViewModel
    {
        public DateTime Date { get; set; }
        public string Description { get; set; } = string.Empty;
        public string AccountName { get; set; } = string.Empty;
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public decimal Balance { get; set; } // Calculated balance
    }

    // ViewModel for Profit and Loss Report
    public class ProfitLossViewModel
    {
        public decimal TotalIncome { get; set; } // Total income for the period
        public decimal TotalExpenses { get; set; } // Total expenses for the period
        // Calculated properties for TotalProfit and TotalLoss
        public decimal TotalProfit => TotalIncome > TotalExpenses ? TotalIncome - TotalExpenses : 0;
        public decimal TotalLoss => TotalExpenses > TotalIncome ? TotalExpenses - TotalIncome : 0;
        public List<ProfitLossItemViewModel> ProfitLossItems { get; set; } = new List<ProfitLossItemViewModel>();

        // Additional properties if needed
    }

    public class ProfitLossItemViewModel
    {
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public bool IsIncome { get; set; }
    }

    // ViewModel for Trial Balance Report
    public class TrialBalanceViewModel
    {
        public List<TrialBalanceAccountViewModel> TrialBalanceAccounts { get; set; } = new List<TrialBalanceAccountViewModel>();
        public decimal TotalDebits { get; set; } // Sum of all debit totals
        public decimal TotalCredits { get; set; } // Sum of all credit totals
        public decimal NetBalance => TotalDebits - TotalCredits; // Automatically calculated
    }

    public class TrialBalanceAccountViewModel
    {
        public string AccountName { get; set; } = string.Empty; // Account name
        public decimal DebitTotal { get; set; } // Total debits for this account
        public decimal CreditTotal { get; set; } // Total credits for this account
        public decimal NetBalance => DebitTotal - CreditTotal; // Automatically calculated balance
    }

    // ViewModel for displaying account balances (optional but helpful in financial reporting)
    public class AccountBalanceViewModel
    {
        public required string AccountName { get; set; } // The account name
        public decimal DebitTotal { get; set; } // Total debits in the account
        public decimal CreditTotal { get; set; } // Total credits in the account
        public decimal NetBalance => DebitTotal - CreditTotal; // Automatically calculated balance
    }
}
