using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HinduTempleofTriStates.Models
{
    public enum AccountTypeEnum
    {
        Asset,
        Liability,
        Equity,
        Revenue,
        Expense,
        Checking
    }

    public class LedgerAccount
    {
        [Key]
        public Guid Id { get; set; }

        public Guid AccountId { get; set; }

        [Required]
        public required string AccountName { get; set; }

        [Required]
        public AccountTypeEnum AccountType { get; set; }

        // Balance calculation (total credits minus total debits)
        

        public decimal DebitTotal => Transactions.Sum(t => t.Debit);
        public decimal CreditTotal => Transactions.Sum(t => t.Credit);

        // Navigation properties
        public virtual ICollection<Donation> Donations { get; set; } = new List<Donation>();
        public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
        public virtual ICollection<CashTransaction> CashTransactions { get; set; } = new List<CashTransaction>();
        public virtual ICollection<GeneralLedgerEntry> GeneralLedgerEntries { get; set; } = new List<GeneralLedgerEntry>();

        // Audit fields
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
        public required string CreatedBy { get; set; }
        public required string UpdatedBy { get; set; }
        public decimal Balance { get; set; }
    }
}
