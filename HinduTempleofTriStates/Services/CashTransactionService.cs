using HinduTempleofTriStates.Data;
using HinduTempleofTriStates.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HinduTempleofTriStates.Services
{
    public class CashTransactionService : ICashTransactionService
    {
        private readonly ApplicationDbContext _context;

        public CashTransactionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CashTransaction>> GetAllCashTransactionsAsync()
        {
            return await _context.CashTransactions.ToListAsync();
        }

        internal Task AddCashTransactionAsync(CashTransaction cashTransaction)
        {
            throw new NotImplementedException();
        }

        // Add more service methods as needed
    }
    namespace HinduTempleofTriStates.Services
    {
        public class CashTransactionService
        {
            private readonly ApplicationDbContext _context;

            public CashTransactionService(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task AddCashTransactionAsync(CashTransaction cashTransaction)
            {
                _context.CashTransactions.Add(cashTransaction);
                await _context.SaveChangesAsync();
            }
        }
    }


    public interface ICashTransactionService
    {
        Task<List<CashTransaction>> GetAllCashTransactionsAsync();
        // Add method signatures as needed
    }
}