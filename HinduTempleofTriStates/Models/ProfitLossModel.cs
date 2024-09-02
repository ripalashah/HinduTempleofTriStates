namespace HinduTempleofTriStates.Models
{
    public class ProfitLossModel
    {
        public List<ProfitLossItem> ProfitLossItems { get; set; } = [];
    }

    public class ProfitLossItem
    {
        public string Description { get; set; }
        public decimal Amount { get; set; }
    }
}
