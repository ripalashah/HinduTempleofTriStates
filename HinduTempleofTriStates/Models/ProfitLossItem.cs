namespace HinduTempleofTriStates.Models
{
    public class ProfitLossModel
    {
        public decimal TotalIncome { get; set; } // Total credit (income)
        public decimal TotalExpenses { get; set; } // Total debit (expenses)
        public decimal NetProfit => TotalIncome - TotalExpenses; // Net profit or loss

        public List<ProfitLossItem> ProfitLossItems { get; set; } = new List<ProfitLossItem>(); // List of individual profit/loss items
        public decimal TotalProfit { get; internal set; }
        public decimal TotalLoss { get; internal set; }
    }

    public class ProfitLossItem
    {
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public bool IsIncome { get; set; } // Flag to indicate if it's income (credit) or expense (debit)
    }
}
