namespace HinduTempleofTriStates.Models
{
    public class ProfitLossModelBase
    {
  
        public List<ProfitLossItem> ProfitLossItems { get; set; } = [];

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string? ToString()
        {
            return base.ToString();
        }
    }
}