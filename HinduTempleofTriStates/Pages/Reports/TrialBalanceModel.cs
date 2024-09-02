using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HinduTempleofTriStates.Pages.Reports
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

    public class TrialBalanceAccount
    {
        public string AccountName { get; set; } = string.Empty;
        public decimal DebitTotal { get; set; }
        public decimal CreditTotal { get; set; }
    }
}
