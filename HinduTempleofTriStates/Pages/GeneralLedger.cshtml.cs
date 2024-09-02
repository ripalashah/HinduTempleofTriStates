using HinduTempleofTriStates.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace HinduTempleofTriStates.Pages
{
    public class GeneralLedgerModel : PageModel
    {
        public List<GeneralLedgerEntry> GeneralLedgerEntries { get; set; } = new List<GeneralLedgerEntry>();

        public void OnGet()
        {
            // Example data initialization. Replace this with actual data retrieval logic.
            GeneralLedgerEntries.Add(new GeneralLedgerEntry
            {
                Date = DateTime.Now,
                Description = "Sample Entry",
                AccountName = "Sample Account",
                Debit = 1500.00m,
                Credit = 300.00m,
                Balance = 1200.00m
            });
        }
    }
}
