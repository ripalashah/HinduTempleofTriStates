using HinduTempleofTriStates.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HinduTempleofTriStates.Pages
{
    public class ProfitLossModel : PageModel
    {
        private ProfitLossModel? profitLossModel;

        public ProfitLossModel? GetProfitLossModel()
        {
            return profitLossModel;
        }

        public void SetProfitLossModel(ProfitLossModel value)
        {
            profitLossModel = value;
        }

        public List<ProfitLossItem>? ProfitLossItems { get; private set; }

        public ProfitLossItem GetProfitLossItem()
        {
            return new() { Description = "Sample", Amount = 100.00m };
        }

        public ProfitLossItem GetProfitLossItem(ProfitLossItem profitLossItem) => profitLossItem;
               
        public void OnGet(ProfitLossItem profitLossItem)
        {
            // Initialize with sample data or fetch data from the database
            SetProfitLossModel(new ProfitLossModel
            {
                ProfitLossItems =
                [
                    profitLossItem
                ]
            });
        }
    }
}
