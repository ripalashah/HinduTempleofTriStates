using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HinduTempleofTriStates.Models;
using HinduTempleofTriStates.Services;
using System.Threading.Tasks;

namespace HinduTempleofTriStates.Views.Accounts
{
    public class EditModel : PageModel
    {
        private readonly AccountService _accountService;

        public EditModel(AccountService accountService)
        {
            _accountService = accountService;
        }

        [BindProperty]
        public required Account Account { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            Account = await _accountService.GetAccountByIdAsync(id);

            if (Account == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _accountService.UpdateAccountAsync(Account);
            return RedirectToPage("Index");
        }
    }
}
