using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HinduTempleofTriStates.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using HinduTempleofTriStates.Data;

namespace HinduTempleofTriStates.Pages.Reports.Reports
{
    public class CashIncomeExpensesModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CashIncomeExpensesModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<CashTransaction> CashTransactions { get; set; } = new List<CashTransaction>();

        public async Task OnGetAsync()
        {
            CashTransactions = await _context.CashTransactions
                .Include(ct => ct.LedgerAccount) // Include related data if needed
                .ToListAsync();
        }
    }
}
