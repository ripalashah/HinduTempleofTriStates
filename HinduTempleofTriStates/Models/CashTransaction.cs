using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HinduTempleofTriStates.Models
{
    public class CashTransaction
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public required string Description { get; set; }
        public decimal Amount { get; set; }
        public decimal Income { get; set; }
        public decimal Expense { get; set; }
        public required string Type { get; set; } // "Income" or "Expense"

        // Foreign Key to LedgerAccount
        public Guid? LedgerAccountId { get; set; }

        [ForeignKey("LedgerAccountId")]
        public LedgerAccount? LedgerAccount { get; set; } // Navigation property

        public CashTransaction(DateTime date, string description, decimal income, decimal expense, string type)
        {
            Date = date;
            Description = description;
            Income = income;
            Expense = expense;
            Type = type;
        }
    }
}
