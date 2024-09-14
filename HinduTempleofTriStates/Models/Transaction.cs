using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HinduTempleofTriStates.Models
{
    public enum TransactionType
    {
        Debit,
        Credit
    }

    public class Transaction
    {
        public Guid Id { get; set; }

        [Required]
        public Guid AccountId { get; set; }

        [Required]
        public Guid? LedgerAccountId { get; set; }

        // DonationId Foreign Key
        public Guid? DonationId { get; set; }

        // Link to Donation entity
        [ForeignKey("DonationId")]
        public virtual Donation? Donation { get; set; }

        [Required]
        public DateTime Date { get; set; } = DateTime.UtcNow;

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(200)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public TransactionType TransactionType { get; set; }

        public decimal Debit => TransactionType == TransactionType.Debit ? Amount : 0;
        public decimal Credit => TransactionType == TransactionType.Credit ? Amount : 0;

        [Required]
        public virtual LedgerAccount? LedgerAccount { get; set; }

        public bool Reconciled { get; set; } = false;
        public DateTime? ReconciliationDate { get; set; }

        [Required]
        public string CreatedBy { get; set; } = string.Empty;

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
