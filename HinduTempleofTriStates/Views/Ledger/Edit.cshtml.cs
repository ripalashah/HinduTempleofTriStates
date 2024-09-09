using HinduTempleofTriStates.Models;
using HinduTempleofTriStates.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace HinduTempleofTriStates.Views.Ledger
{
    public class EditModel : PageModel
    {
        private readonly LedgerService _ledgerService;

        public EditModel(LedgerService ledgerService)
        {
            _ledgerService = ledgerService;

        }

        [BindProperty]
        public LedgerAccount? LedgerAccount { get; set; }

        public LedgerService Get_ledgerService()
        {
            return _ledgerService;
        }

        // Fetch the existing LedgerAccount data when the page is loaded
        public async Task<IActionResult> OnGetAsync(Guid id, LedgerService _ledgerService)
        {
            LedgerAccount = await _ledgerService.GetLedgerAccountByIdAsync(id);

            if (LedgerAccount == null)
            {
                return NotFound();
            }

            return Page();
        }

        public LedgerAccount? GetLedgerAccount()
        {
            return LedgerAccount;
        }

        // Save the changes when the form is submitted
        public async Task<IActionResult> OnPostAsync(LedgerAccount? ledgerAccount)
        {
            if (!ModelState.IsValid) // Server-side validation
            {
                return Page(); // Redisplay form with validation messages
            }

#pragma warning disable CS8604 // Possible null reference argument.
            await _ledgerService.UpdateLedgerAccountAsync(ledgerAccount); // Update the account in the database
#pragma warning restore CS8604 // Possible null reference argument.
            return RedirectToPage("Index");
        }
    }
}
