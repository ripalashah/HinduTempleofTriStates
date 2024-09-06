using HinduTempleofTriStates.Data;
using HinduTempleofTriStates.Repositories;
using HinduTempleofTriStates.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HinduTempleofTriStates.Services
{
    public class FinancialReportService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILedgerRepository _ledgerRepository;

        public FinancialReportService(ApplicationDbContext context, ILedgerRepository ledgerRepository)
        {
            _context = context;
            _ledgerRepository = ledgerRepository;
        }

        // Method for General Ledger Report
        public async Task<GeneralLedgerModel> GetGeneralLedgerAsync()
        {
            var generalLedgerEntries = await _context.GeneralLedgerEntries
                .Include(gl => gl.LedgerAccount) // Include the LedgerAccount for each entry
                .Select(gl => new GeneralLedgerEntryModel
                {
                    Date = gl.Date,
                    Description = gl.Description,
                    AccountName = gl.LedgerAccount.AccountName, // Accessing AccountName via LedgerAccount
                    Debit = gl.Debit,
                    Credit = gl.Credit,
                 }).ToListAsync();

            // Calculating total debit, credit, and balance
            var totalDebit = generalLedgerEntries.Sum(e => e.Debit);
            var totalCredit = generalLedgerEntries.Sum(e => e.Credit);
            var totalBalance = totalDebit - totalCredit;

            // Returning the GeneralLedgerModel with the results
            return new GeneralLedgerModel
            {
                GeneralLedgerEntries = generalLedgerEntries,
                TotalDebit = totalDebit,
                TotalCredit = totalCredit,
                TotalBalance = totalBalance
            };
        }
    }
}
