namespace HinduTempleofTriStates.Models
{
    public class TrialBalanceModel
    {
        public List<TrialBalanceEntry> TrialBalanceEntries { get; set; } = new List<TrialBalanceEntry>();
    }

    public class TrialBalanceEntry
    {
        public string AccountName { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
    }
}
