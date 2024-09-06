using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HinduTempleofTriStates.Models;
using HinduTempleofTriStates.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace HinduTempleofTriStates.Views.Ledger
{
    public class IndexModel : PageModel
    {
        private readonly LedgerService _ledgerService;

        public IndexModel(LedgerService ledgerService)
        {
            _ledgerService = ledgerService;
        }

        public IList<LedgerAccount> LedgerAccounts { get; set; } = new List<LedgerAccount>();

        // Fetch all ledger accounts when the page is requested
        public async Task OnGetAsync()
        {
            LedgerAccounts = (await _ledgerService.GetAllAccountsAsync()).ToList();
        }
    }
}
