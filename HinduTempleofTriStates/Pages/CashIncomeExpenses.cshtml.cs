using HinduTempleofTriStates.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HinduTempleofTriStates.Pages
{
    public class CashIncomeExpensesModel : PageModel
    {
        public List<CashTransaction> CashTransactions { get; set; } = new List<CashTransaction>();

        public void OnGet()
        {
            // Example data initialization. Replace this with actual data retrieval logic.
            CashTransactions.Add(item: new CashTransaction
            {
                Date = DateTime.Now,
                Description = "Sample Income",
                Income = 1000.00m,
                Expense = 0.00m
            });
            CashTransactions.Add(item: new CashTransaction
            {
                Date = DateTime.Now.AddDays(-1),
                Description = "Sample Expense",
                Income = 0.00m,
                Expense = 500.00m
            });
        }
    }
}
