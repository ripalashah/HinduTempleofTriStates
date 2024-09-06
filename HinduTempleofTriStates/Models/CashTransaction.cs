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
        public Guid Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public required string Description { get; set; }

        [DataType(DataType.Currency)]
        public decimal Amount { get; set; } // Total amount for income or expense

        [DataType(DataType.Currency)]
        public decimal Income => Type == CashTransactionType.Income ? Amount : 0m;

        [DataType(DataType.Currency)]
        public decimal Expense => Type == CashTransactionType.Expense ? Amount : 0m;

        public CashTransactionType Type { get; set; } // Enum for transaction type

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
