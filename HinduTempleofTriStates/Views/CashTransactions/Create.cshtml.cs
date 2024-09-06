using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HinduTempleofTriStates.Models;
using HinduTempleofTriStates.Services;
using System.Threading.Tasks;

namespace HinduTempleofTriStates.Views.CashTransactions
{
    public class CreateModel : PageModel
    {
        private readonly CashTransactionService _cashTransactionService;

        public CreateModel(CashTransactionService cashTransactionService)
        {
            _cashTransactionService = cashTransactionService;
        }

        [BindProperty]
        public CashTransaction CashTransaction { get; set; } = new CashTransaction();

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _cashTransactionService.AddCashTransactionAsync(CashTransaction);
            return RedirectToPage("Index");
        }
    }
}
