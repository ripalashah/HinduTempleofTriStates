namespace TempleManagementSystem.Models
{
    public class GeneralLedgerModel
    {
        public List<GeneralLedgerEntry> GeneralLedgerEntries { get; set; } = [];
    }

    public class GeneralLedgerEntry
    {
        public required string AccountName { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
    }
}
