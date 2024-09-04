using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HinduTempleofTriStates.Models
{
    public class GeneralLedgerEntry
    {
        public Guid Id { get; set; }
        [NotMapped] // This attribute excludes the property from being mapped to the database
        public Guid AccountId { get; set; }
        public required object EntryId { get; set; }
        public required string AccountName { get; set; }
        public DateTime Date { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public decimal Balance { get; set; } // Add this property
        public required string Description { get; set; }

        public Guid LedgerAccountId { get; set; }

        
    }
}
