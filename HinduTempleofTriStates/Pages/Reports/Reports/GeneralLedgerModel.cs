using HinduTempleofTriStates.Models;

namespace HinduTempleofTriStates.Pages.Reports.Reports
{
    public class GeneralLedgerModel
    {
        public required List<GeneralLedgerEntry> GeneralLedgerEntries { get; set; }
        public decimal TotalDebit { get; set; }
        public decimal TotalCredit { get; set; }
        public decimal TotalBalance { get; set; }
    }
}
