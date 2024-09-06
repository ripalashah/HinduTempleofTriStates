using System;
using System.ComponentModel.DataAnnotations;

namespace HinduTempleofTriStates.Models
{
    public enum TransactionType
    {
        Debit,
        Credit
    }

    public class Transaction
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid AccountId { get; set; } // Foreign key for the related Account or Fund

        [Required]
        public Guid LedgerAccountId { get; set; } // Foreign key for LedgerAccount

        [Required]
        public DateTime Date { get; set; } = DateTime.UtcNow; // Transaction date

        [Required]
        [Range(0.01, Double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        public decimal Amount { get; set; } // Transaction amount

        [Required]
        [StringLength(200, ErrorMessage = "Description cannot be longer than 200 characters.")]
        public string Description { get; set; } = string.Empty; // Description of the transaction

        [Required]
        public TransactionType TransactionType { get; set; } // Type of transaction (Debit or Credit)

        public decimal Debit => TransactionType == TransactionType.Debit ? Amount : 0;
        public decimal Credit => TransactionType == TransactionType.Credit ? Amount : 0;

        public virtual required LedgerAccount LedgerAccount { get; set; } // Associated ledger account

        public bool Reconciled { get; set; } = false; // Reconciliation status
        public DateTime? ReconciliationDate { get; set; } // Date of reconciliation, if applicable

        // Metadata for tracking creation and modification
        [Required]
        public string CreatedBy { get; set; } = string.Empty;

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
