namespace HinduTempleofTriStates.Models
{
    public class GeneralLedgerEntry
    {
        public int Id { get; set; }
        public required string AccountName { get; set; }
        public DateTime Date { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public decimal Balance { get; set; } // Add this property
        public required string Description { get; set; }
    }
}
