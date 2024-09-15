using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HinduTempleofTriStates.Models;

namespace HinduTempleofTriStates.Repositories
{
    public interface IInventoryRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product?> GetProductById(Guid id);
        Task AddProductAsync(Product product);
        Task StockInAsync(StockTransaction transaction);
        Task StockOutAsync(StockTransaction transaction);
        Task<IEnumerable<StockTransaction>> GetAllStockTransactionsAsync();
        Task SaveChangesAsync();  // Add this to your repository interface

    }
}
