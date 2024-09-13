using System.Collections.Generic;
using System.Linq;

namespace HinduTempleofTriStates.Models
{
    public class CashIncomeExpensesModel
    {
        // List of all Cash Transactions
        public List<CashTransaction> CashTransactions { get; set; } = new List<CashTransaction>();

        // Total income (sum of all income-type transactions)
        public decimal TotalIncome { get; set; }

        // Total expense (sum of all expense-type transactions)
        public decimal TotalExpense { get; set; }

        // Net balance (Total Income - Total Expense)
        public decimal NetBalance => TotalIncome - TotalExpense; // Net balance (income - expense)

        // Add these properties to resolve the missing references
        public decimal TotalDebit => TotalExpense;  // Alias for total debit
        public decimal TotalCredit => TotalIncome;  // Alias for total credit
    }
}
