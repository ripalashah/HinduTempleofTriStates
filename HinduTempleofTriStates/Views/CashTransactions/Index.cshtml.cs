using Microsoft.AspNetCore.Mvc.RazorPages;
using HinduTempleofTriStates.Models;
using HinduTempleofTriStates.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HinduTempleofTriStates.Views.CashTransactions
{
    public class IndexModel : PageModel
    {
        private readonly CashTransactionService _cashTransactionService;

        public IndexModel(CashTransactionService cashTransactionService)
        {
            _cashTransactionService = cashTransactionService;
        }

        public List<CashTransaction> CashTransactions { get; set; } = new List<CashTransaction>();

        public async Task OnGetAsync()
        {
            CashTransactions = await _cashTransactionService.GetAllCashTransactionsAsync();
        }
    }
}
