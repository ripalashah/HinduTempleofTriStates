using Microsoft.AspNetCore.Mvc.RazorPages;
using HinduTempleofTriStates.Models;
using HinduTempleofTriStates.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HinduTempleofTriStates.Views.Accounts
{
    public class IndexModel : PageModel
    {
        private readonly AccountService _accountService;

        public IndexModel(AccountService accountService)
        {
            _accountService = accountService;
        }

        public List<Account> Accounts { get; set; } = new List<Account>();

        public async Task OnGetAsync()
        {
            Accounts = await _accountService.GetAllAccountsAsync();
        }
    }
}
