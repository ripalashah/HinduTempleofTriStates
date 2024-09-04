using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HinduTempleofTriStates.Models
{
    public class LedgerAccount
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public required string AccountName { get; set; }

        [Required]
        public required string AccountType { get; set; } // Asset, Liability, Equity, Revenue, Expense

        public decimal Balance { get; set; }

        // Navigation property for related Donations
        public virtual ICollection<Donation> Donations { get; set; } = new List<Donation>();

        // Navigation property for related Transactions
        public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

        // Navigation property for related CashTransactions
        public virtual ICollection<CashTransaction> CashTransactions { get; set; } = new List<CashTransaction>();
        public required ICollection<LedgerEntry> LedgerEntries { get; set; }
        
        [Required(ErrorMessage = "The AccountId field is required.")]
        public Guid AccountId { get; set; }
    }

    public class LedgerEntry
    {
    }
}
