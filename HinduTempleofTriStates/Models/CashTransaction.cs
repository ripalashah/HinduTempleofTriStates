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
        public DateTime Date { get; set; }

        [Required]
        public required string Description { get; set; }

        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        [DataType(DataType.Currency)]
        public decimal Income { get; set; }

        [DataType(DataType.Currency)]
        public decimal Expense { get; set; }

        public CashTransactionType Type { get; set; }

        [Required]
        public required string TransactionType { get; set; } // "Income" or "Expense"

        // Foreign Key to LedgerAccount
        public Guid? LedgerAccountId { get; set; }

        [ForeignKey("LedgerAccountId")]
        public LedgerAccount? LedgerAccount { get; set; } // Navigation property

        // Parameterized constructor
        public CashTransaction(DateTime date, string description, decimal income, decimal expense, string type)
        {
            Date = date;
            Description = description;
            Income = income;
            Expense = expense;
            TransactionType = type;
        }

        // Default constructor for EF Core
        public CashTransaction() { }
    }
}
