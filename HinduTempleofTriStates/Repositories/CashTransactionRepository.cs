using HinduTempleofTriStates.Models;
using HinduTempleofTriStates.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HinduTempleofTriStates.Repositories
{
    public class CashTransactionRepository : ICashTransactionRepository
    {
        private readonly ApplicationDbContext _context;

        public CashTransactionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CashTransaction>> GetAllCashTransactionsAsync()
        {
            return await _context.CashTransactions.ToListAsync();
        }

        public async Task<CashTransaction?> GetCashTransactionByIdAsync(Guid id)
        {
            return await _context.CashTransactions.FindAsync(id);
        }

        public async Task AddCashTransactionAsync(CashTransaction transaction)
        {
            _context.CashTransactions.Add(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCashTransactionAsync(CashTransaction transaction)
        {
            _context.CashTransactions.Update(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCashTransactionAsync(Guid id)
        {
            var transaction = await _context.CashTransactions.FindAsync(id);
            if (transaction != null)
            {
                _context.CashTransactions.Remove(transaction);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> CashTransactionExistsAsync(Guid id)
        {
            return await _context.CashTransactions.AnyAsync(e => e.Id == id);
        }
    }
}
