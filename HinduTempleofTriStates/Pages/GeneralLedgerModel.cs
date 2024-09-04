// File: Pages/Reports/GeneralLedgerModel.cs
using System;
using System.Collections.Generic;

namespace HinduTempleofTriStates.Pages
{
    public class GeneralLedgerModel
    {
        public required IEnumerable<GeneralLedgerEntry> GeneralLedgerEntries { get; set; }
        public decimal TotalDebit { get; set; }
        public decimal TotalCredit { get; set; }
        public decimal TotalBalance { get; set; }
    }

    public class GeneralLedgerEntry
    {
        public DateTime Date { get; set; }
        public required string Description { get; set; }
        public required string AccountName { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public decimal Balance { get; set; }
    }
}
