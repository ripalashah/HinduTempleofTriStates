using System.Collections.Generic;
using System.Linq;

namespace HinduTempleofTriStates.Models
{
    public class CashIncomeExpensesModel
    {
        public List<CashTransaction> CashTransactions { get; set; } = new List<CashTransaction>();

        // Properties for totals
        public decimal TotalIncome => CashTransactions.Sum(t => t.Income);
        public decimal TotalExpense => CashTransactions.Sum(t => t.Expense);
    }
}
