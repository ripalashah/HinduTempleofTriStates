using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HinduTempleofTriStates.Models;
using HinduTempleofTriStates.Services;
using System.Threading.Tasks;

namespace HinduTempleofTriStates.Views.Accounts
{
    public class CreateModel : PageModel
    {
        private readonly IAccountService _accountService;  // Use the interface for DI

        public CreateModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [BindProperty]
        public Account Account { get; set; } = new Account // Initialize an empty account model
            { 
                AccountType = AccountTypeEnum.Asset,  // Set a default AccountType
                AccountName = string.Empty            // Set an empty string as the default AccountName
            };
    public IActionResult OnGet()
        {
            // Load any necessary data for the page
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page(); // Return the page if the model state is not valid
            }

            await _accountService.AddAccountAsync(Account);  // Call the service to add a new account

            return RedirectToPage("Index"); // Redirect to the Index page after successful creation
        }
    }
}
