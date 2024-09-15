using HinduTempleofTriStates.Data;
using HinduTempleofTriStates.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HinduTempleofTriStates.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly ApplicationDbContext _context;

        public InventoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products.Include(p => p.Supplier).ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Product?> GetProductById(Guid id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<IEnumerable<StockTransaction>> GetAllStockTransactionsAsync()
        {
            return await _context.StockTransactions.Include(t => t.Product).ToListAsync();
        }

        public async Task AddProductAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task StockInAsync(StockTransaction transaction)
        {
            _context.StockTransactions.Add(transaction);
            var product = await _context.Products.FindAsync(transaction.ProductId);

            if (product == null)
            {
                throw new KeyNotFoundException($"Product with ID {transaction.ProductId} was not found.");
            }

            product.Quantity += transaction.Quantity;
            await _context.SaveChangesAsync();
        }

        public async Task StockOutAsync(StockTransaction transaction)
        {
            _context.StockTransactions.Add(transaction);
            var product = await _context.Products.FindAsync(transaction.ProductId);

            if (product == null)
            {
                throw new KeyNotFoundException($"Product with ID {transaction.ProductId} was not found.");
            }

            product.Quantity -= transaction.Quantity;
            await _context.SaveChangesAsync();
        }

    }
}
