namespace HinduTempleofTriStates.Models
{
    public class ProfitLossModel
    {
        public List<ProfitLossItem> ProfitLossItems { get; set; } = new List<ProfitLossItem>();

        public decimal TotalIncome { get; set; } // This will be set by the service
        public decimal TotalExpenses { get; set; } // This will be set by the service
        public decimal NetProfitLoss { get; set; } // This will be set by the service
        public decimal TotalProfit { get; internal set; }
        public decimal TotalLoss { get; internal set; }
    }

    public class ProfitLossItem
    {
        public required string Description { get; set; }
        public decimal Amount { get; set; }
    }
}
