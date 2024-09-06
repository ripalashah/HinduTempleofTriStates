using HinduTempleofTriStates.Models; // Adjust to match your namespace
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HinduTempleofTriStates.Views.Reports
{
    public class ProfitLossModel : PageModel
    {
        public List<ProfitLossItem>? ProfitLossItems { get; private set; }

        public void OnGet()
        {
            // Initialize with sample data or fetch data from the database
            ProfitLossItems = new List<ProfitLossItem>
            {
                new ProfitLossItem { Description = "Sample", Amount = 100.00m }
            };
        }
    }
}
