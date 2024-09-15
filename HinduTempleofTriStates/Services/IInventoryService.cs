using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HinduTempleofTriStates.Models;
using HinduTempleofTriStates.Repositories;

namespace HinduTempleofTriStates.Services
{
    public interface IInventoryService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductById(Guid id);
        Task AddProductAsync(Product product);
        Task StockInAsync(StockTransaction transaction);
        Task StockOutAsync(StockTransaction transaction);
        Task<IEnumerable<StockReportViewModel>> GenerateStockReportAsync();

    }
    
}
