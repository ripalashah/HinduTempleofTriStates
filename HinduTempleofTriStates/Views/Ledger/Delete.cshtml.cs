using HinduTempleofTriStates.Models;
using HinduTempleofTriStates.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace HinduTempleofTriStates.Views.Ledger
{
    public class DeleteModel : PageModel
    {
        private readonly LedgerService _ledgerService;

        public DeleteModel(LedgerService ledgerService)
        {
            _ledgerService = ledgerService;
        }

        [BindProperty]
        public LedgerAccount LedgerAccount { get; set; }

        // Fetch the LedgerAccount to display its details before deletion
        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            LedgerAccount = await _ledgerService.GetAccountByIdAsync(id);

            if (LedgerAccount == null)
            {
                return NotFound();
            }

            return Page();
        }

        // Handle the form submission to delete the LedgerAccount
        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            await _ledgerService.DeleteAccountAsync(id);
            return RedirectToPage("Index");
        }
    }
}
