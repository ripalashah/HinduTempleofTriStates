// File: Pages/Reports/GeneralLedgerModel.cs
using System;
using System.Collections.Generic;

namespace HinduTempleofTriStates.Pages.Reports
{
    public class GeneralLedgerModel
    {
        public IEnumerable<GeneralLedgerEntry> GeneralLedgerEntries { get; set; }
        public decimal TotalDebit { get; set; }
        public decimal TotalCredit { get; set; }
        public decimal TotalBalance { get; set; }
    }
   
}
