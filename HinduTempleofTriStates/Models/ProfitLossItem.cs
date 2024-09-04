﻿namespace HinduTempleofTriStates.Models
{
    public class ProfitLossModel
    {
        public List<ProfitLossItem> ProfitLossItems { get; set; } = new List<ProfitLossItem>();
    }

    public class ProfitLossItem
    {
        public required string Description { get; set; }
        public decimal Amount { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
