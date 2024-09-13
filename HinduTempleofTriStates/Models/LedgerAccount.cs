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

        [Required]
        public string AccountName { get; set; } = string.Empty;

        public decimal Balance { get; set; } = 0m;

        [Required]
        public AccountTypeEnum AccountType { get; set; }

        // Calculate total debits and credits based on the Transactions or GeneralLedgerEntries
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
        public string CreatedBy { get; set; } = string.Empty;
        public string UpdatedBy { get; set; } = string.Empty;
        public bool IsDeleted { get; set; } = false; // Soft delete flag
    }
}
