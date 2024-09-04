using System.Collections.Generic;
using HinduTempleofTriStates.Models;

namespace HinduTempleofTriStates.Pages.Reports.Reports
{
    public class ProfitLossModelBase
    {
        public List<ProfitLossItem> ProfitLossItems { get; set; } = new List<ProfitLossItem>();

        public ProfitLossModelBase() { }

        public ProfitLossModelBase(List<ProfitLossItem> items)
        {
            ProfitLossItems = items;
        }

        public override bool Equals(object? obj)
        {
            if (obj is ProfitLossModelBase other)
            {
                // Compare based on relevant properties
                return ProfitLossItems.SequenceEqual(other.ProfitLossItems);
            }
            return false;
        }

        public override int GetHashCode()
        {
            // Generate a hash code based on ProfitLossItems
            return ProfitLossItems != null ? ProfitLossItems.GetHashCode() : 0;
        }

        public override string? ToString()
        {
            // Provide a string representation of the object
            return $"ProfitLossModelBase with {ProfitLossItems.Count} items";
        }
    }
}
