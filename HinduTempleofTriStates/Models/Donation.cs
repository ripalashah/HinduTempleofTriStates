using System.ComponentModel.DataAnnotations;

namespace HinduTempleofTriStates.Models
{
    public class Donation
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string DonorName { get; set; } = string.Empty;

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public double Amount { get; set; }

        [Required]
        public string DonationCategory { get; set; } = string.Empty;

        [Required]
        public string DonationType { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Phone]
        public string Phone { get; set; } = string.Empty;

        [StringLength(100)]
        public string City { get; set; } = string.Empty;

        [StringLength(100)]
        public string State { get; set; } = string.Empty;

        [StringLength(100)]
        public string Country { get; set; } = string.Empty;

        // Add the IsDeleted property here
        public bool IsDeleted { get; set; } = false;

        // Foreign Keys
        public Guid? LedgerAccountId { get; set; } // Allow nullable FK for relationships
        public virtual LedgerAccount? LedgerAccount { get; set; }
        public Guid? CashTransactionId { get; set; }

        // Navigation property
        public CashTransaction? CashTransaction { get; set; }

        public  ICollection<CashTransaction> CashTransactions { get; set; } = new List<CashTransaction>();
        public  ICollection<GeneralLedgerEntry> GeneralLedgerEntries { get; set; } = new List<GeneralLedgerEntry>();
        public string? ReceiptNumber { get; set; }
        // Add the IsSynced property to track synchronization status
        public bool IsSynced { get; set; } = false; // Default value is false

    }
}