using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HinduTempleofTriStates.Models
{
    public class GeneralLedgerEntry
    {
        // Primary key for the ledger entry
        [Key]
        public Guid Id { get; set; }

        // The account impacted by this ledger entry (debit or credit)
        public Guid LedgerAccountId { get; set; }

        // Date of the transaction
        public DateTime Date { get; set; } = DateTime.UtcNow;

        // Description of the transaction (e.g., "Donation from John Doe")
        [Required]
        public required string Description { get; set; }

        // Amount debited or credited
        public decimal Debit { get; set; }

        public decimal Credit { get; set; }

        // Navigation property for LedgerAccount
        public required LedgerAccount LedgerAccount { get; set; }

        // Calculated balance (Debit - Credit)
        public decimal Balance => Debit - Credit;
    }

    // Model representing the general ledger report
    public class GeneralLedgerModel
    {
        public List<GeneralLedgerEntryModel> GeneralLedgerEntries { get; set; } = new List<GeneralLedgerEntryModel>();
        public decimal TotalDebit { get; set; }
        public decimal TotalCredit { get; set; }
        public decimal TotalBalance { get; set; }
    }

    // Class representing each entry in the general ledger
    public class GeneralLedgerEntryModel
    {
        public DateTime Date { get; set; }
        public required string Description { get; set; }
        public required string AccountName { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public decimal Balance => Debit - Credit;
    }
}
