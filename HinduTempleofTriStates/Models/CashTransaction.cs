using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HinduTempleofTriStates.Models
{
    public enum CashTransactionType
    {
        Income,
        Expense
    }

    public class CashTransaction
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public required string Description { get; set; }

        [DataType(DataType.Currency)]
        [Required]
        public decimal Amount { get; set; } // The amount will represent either income or expense.

        [NotMapped]
        [DataType(DataType.Currency)]
        public decimal Income => Type == CashTransactionType.Income ? Amount : 0m;

        [NotMapped]
        [DataType(DataType.Currency)]
        public decimal Expense => Type == CashTransactionType.Expense ? Amount : 0m;

        [Required]
        public CashTransactionType Type { get; set; } // Enum for transaction type (Income or Expense)

        // Foreign Key to LedgerAccount
        public Guid? LedgerAccountId { get; set; }

        [ForeignKey("LedgerAccountId")]
        public LedgerAccount? LedgerAccount { get; set; } // Navigation property

        // Constructor for Income/Expense transaction
        public CashTransaction(DateTime date, string description, decimal amount, CashTransactionType type)
        {
            Date = date;
            Description = description;
            Amount = amount;
            Type = type;
        }

        // Default constructor for EF Core
        public CashTransaction() { }
    }
}
