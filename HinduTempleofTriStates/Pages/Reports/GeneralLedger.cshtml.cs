// File: Pages/Reports/GeneralLedger.cshtml.cs
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HinduTempleofTriStates.Pages.Reports
{
    public class GeneralLedgerModel : PageModel
    {
        public required IEnumerable<GeneralLedgerEntry> GeneralLedgerEntries { get; set; }
        public decimal TotalDebit { get; set; }
        public decimal TotalCredit { get; set; }
        public decimal TotalBalance { get; set; }

        public void OnGet()
        {
            // Fetch the data here. Example:
            // GeneralLedgerEntries = await _ledgerService.GetGeneralLedgerEntriesAsync();
            // Calculate totals...
        }
    }
}
