using HinduTempleofTriStates.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HinduTempleofTriStates.Pages
{
    public class TrialBalanceModel : PageModel
    {
        public List<TrialBalanceAccount> TrialBalanceAccounts { get; set; } = new List<TrialBalanceAccount>();

        public void OnGet()
        {
            // Example data initialization. Replace this with actual data retrieval logic.
            TrialBalanceAccounts.Add(new TrialBalanceAccount
            {
                AccountName = "Sample Account",
                DebitTotal = 1000.00m,
                CreditTotal = 500.00m
            });
        }
    }
}
