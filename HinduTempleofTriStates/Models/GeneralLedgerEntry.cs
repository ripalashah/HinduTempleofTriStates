using System;
using System.Collections.Generic;

namespace HinduTempleofTriStates.Models
{
    // Model representing the general ledger report
    public class GeneralLedgerEntry
    {
        public Guid Id { get; set; } // Primary Key

        public Guid LedgerAccountId { get; set; } // Foreign Key to LedgerAccount
        public virtual LedgerAccount? LedgerAccount { get; set; }  // Navigation property

        public Guid? DonationId { get; set; } // Foreign Key to Donation (if applicable)
        public Donation? Donation { get; set; } // Navigation property to Donation

        public DateTime Date { get; set; } // Transaction date
        public string Description { get; set; } = string.Empty; // Description of the transaction
        public decimal Debit { get; set; } // Debit amount
        public decimal Credit { get; set; } // Credit amount

        public decimal Balance => Credit - Debit; // Computed property to calculate balance
    }

    // Class representing each entry in the general ledger report
    public class GeneralLedgerEntryModel
    {
        public DateTime Date { get; set; }
        public string Description { get; set; } = string.Empty;
        public string AccountName { get; set; } = string.Empty;
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public decimal Balance => Credit - Debit; // Calculated balance
    }
    // General ledger model for the report generation
    public class GeneralLedgerModel
    {
        public List<GeneralLedgerEntryModel> LedgerEntries { get; set; } = new List<GeneralLedgerEntryModel>();
        public decimal TotalDebits { get; set; }
        public decimal TotalCredits { get; set; }
        public decimal NetBalance => TotalCredits - TotalDebits;
    }

    public class LedgerEntry
    {
        public string AccountName { get; set; } = string.Empty;
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public decimal Balance => Credit - Debit;
    }
}
