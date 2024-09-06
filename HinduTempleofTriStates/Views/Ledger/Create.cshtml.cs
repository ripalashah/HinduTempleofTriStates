using HinduTempleofTriStates.Models;
using HinduTempleofTriStates.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace HinduTempleofTriStates.Views.Ledger
{
    public class CreateModel : PageModel
    {
        private readonly LedgerService _ledgerService;

        public CreateModel(LedgerService ledgerService)
        {
            _ledgerService = ledgerService;
        }

        [BindProperty]
        public LedgerAccount LedgerAccount { get; set; }

        // Handles the GET request to display the empty form
        public IActionResult OnGet()
        {
            return Page();
        }

        // Handles the POST request to create a new LedgerAccount
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) // Server-side validation check
            {
                return Page(); // If invalid, re-display the form with validation messages
            }

            await _ledgerService.AddAccountAsync(LedgerAccount); // Adds the new ledger account using the service
            return RedirectToPage("Index"); // Redirects to the Index page after successful creation
        }
    }
}
