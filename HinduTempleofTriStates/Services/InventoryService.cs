using HinduTempleofTriStates.Data;
using HinduTempleofTriStates.Models;
using HinduTempleofTriStates.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HinduTempleofTriStates.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly ILogger<InventoryService> _logger;
        private readonly IInventoryRepository _inventoryRepository;
        private readonly ApplicationDbContext _context;  // Declare DbContext

        public InventoryService(ILogger<InventoryService> logger, IInventoryRepository inventoryRepository, ApplicationDbContext context)
        {
            _logger = logger;
            _inventoryRepository = inventoryRepository;
            _context = context;  // Initialize DbContext
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _inventoryRepository.GetAllProductsAsync();
        }

        public async Task<Product> GetProductById(Guid id)
        {
            var product = await _inventoryRepository.GetProductById(id);
            if (product == null)
            {
                throw new KeyNotFoundException($"Product with ID {id} was not found.");
            }
            return product;
        }

        public async Task AddProductAsync(Product product)
        {
            await _inventoryRepository.AddProductAsync(product);
        }

        public async Task StockInAsync(StockTransaction transaction)
        {
            _logger.LogInformation("Starting StockIn for ProductId: {ProductId}", transaction.ProductId);

            var product = await _inventoryRepository.GetProductById(transaction.ProductId);
            if (product == null)
            {
                _logger.LogWarning("Product with ID {ProductId} not found", transaction.ProductId);
                throw new KeyNotFoundException($"Product with ID {transaction.ProductId} was not found.");
            }

            product.Quantity += transaction.Quantity;
            await _context.SaveChangesAsync();  // Call SaveChangesAsync on DbContext

            _logger.LogInformation("StockIn completed for ProductId: {ProductId}", transaction.ProductId);
        }

        public async Task StockOutAsync(StockTransaction transaction)
        {
            await _inventoryRepository.StockOutAsync(transaction);
        }
        public async Task<IEnumerable<StockReportViewModel>> GenerateStockReportAsync()
        {
            var products = await _inventoryRepository.GetAllProductsAsync();
            var transactions = await _inventoryRepository.GetAllStockTransactionsAsync();

            var report = products.Select(product => new StockReportViewModel
            {
                ProductName = product.Name,
                Category = product.Category,
                InitialQuantity = product.Quantity,
                StockInQuantity = transactions
                    .Where(t => t.ProductId == product.Id && t.Type == "Stock In")
                    .Sum(t => t.Quantity),
                StockOutQuantity = transactions
                    .Where(t => t.ProductId == product.Id && t.Type == "Stock Out")
                    .Sum(t => t.Quantity),
                CurrentStock = product.Quantity
                    + transactions.Where(t => t.ProductId == product.Id && t.Type == "Stock In").Sum(t => t.Quantity)
                    - transactions.Where(t => t.ProductId == product.Id && t.Type == "Stock Out").Sum(t => t.Quantity)
            });

            return report;
        }


    }
}
