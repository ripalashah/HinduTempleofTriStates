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
        public LedgerAccount? LedgerAccount { get; set; }

        // Fetch the LedgerAccount to display its details before deletion
        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            LedgerAccount = await _ledgerService.GetLedgerAccountByIdAsync(id);

            if (LedgerAccount == null)
            {
                return NotFound();
            }

            return Page();
        }

        // Handle the form submission to delete the LedgerAccount
        public IActionResult OnPost(Guid id)
        {
            _ledgerService.DeleteAccount(id);
            return RedirectToPage("Index");
        }
    }
}
