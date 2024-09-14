using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HinduTempleofTriStates.Models
{
    public enum CashTransactionType
    {
        Credit = 1,
        Debit = 0
    }

    public class CashTransaction
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public DateTime Date { get; set; } = DateTime.UtcNow;

        [Required]
        public required string Description { get; set; }

        [DataType(DataType.Currency)]
        [Required]
        public decimal Amount { get; set; }

        [Required]
        public CashTransactionType Type { get; set; } // CashTransactionType enum

        public Guid LedgerAccountId { get; set; }

        public virtual LedgerAccount? LedgerAccount { get; set; }

        public Guid? DonationId { get; set; }
        public virtual Donation? Donation { get; set; }
        public Guid AccountId { get; internal set; }
        public TransactionType TransactionType { get; set; }
        public string? CreatedBy { get; internal set; }
        public DateTime CreatedAt { get; internal set; }
    }
}