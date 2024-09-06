using System.Collections.Generic;
using System.Linq;

namespace HinduTempleofTriStates.Models
{
    public class CashIncomeExpensesModel
    {
        // List of all Cash Transactions
        public List<CashTransaction> CashTransactions { get; set; } = new List<CashTransaction>();

        // Calculate total income (sum of all income-type transactions)
        public decimal TotalIncome => CashTransactions
            .Where(t => t.Type == CashTransactionType.Income)
            .Sum(t => t.Amount);

        // Calculate total expense (sum of all expense-type transactions)
        public decimal TotalExpense => CashTransactions
            .Where(t => t.Type == CashTransactionType.Expense)
            .Sum(t => t.Amount);

        // Calculate net balance (Total Income - Total Expense)
        public decimal NetBalance => TotalIncome - TotalExpense;
    }
}
