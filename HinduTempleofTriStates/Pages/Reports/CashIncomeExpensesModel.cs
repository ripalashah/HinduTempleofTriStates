using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HinduTempleofTriStates.Pages.Reports
{
    public class CashIncomeExpensesModel : PageModel
    {
        public List<CashTransaction> CashTransactions { get; set; } = new List<CashTransaction>();

        public void OnGet()
        {
            // Example data initialization. Replace this with actual data retrieval logic.
            CashTransactions.Add(new CashTransaction
            {
                Date = DateTime.Now,
                Description = "Sample Transaction",
                Income = 100.00m,
                Expense = 50.00m,
                TransactionType = "Sample Type" // Ensure all required properties are set
            });
        }
    }

    public class CashTransaction
    {
        public DateTime Date { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Income { get; set; }
        public decimal Expense { get; set; }
        public string TransactionType { get; set; } = string.Empty; // Ensure this is included if required
    }
}
