namespace HinduTempleofTriStates.Services
{
    internal class LedgerEntry
    {
        public required string AccountName { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public decimal Balance { get; set; }
    }
}