using Microsoft.AspNetCore.Mvc;
using HinduTempleofTriStates.Models;
using HinduTempleofTriStates.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HinduTempleofTriStates.Controllers
{
    [Route("Inventory")]
    public class InventoryController : Controller
    {
        private readonly IInventoryService _inventoryService;
        private readonly ISupplierService _supplierService;

        public InventoryController(IInventoryService inventoryService, ISupplierService supplierService)
        {
            _inventoryService = inventoryService;
            _supplierService = supplierService;
        }

        // GET: Inventory/Index
        [HttpGet("")]
        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var products = await _inventoryService.GetAllProductsAsync();
            return View(products);
        }

        // GET: Inventory/Create
        [HttpGet("Create")]
        public IActionResult Create()
        {
            ViewBag.Suppliers = new SelectList(_supplierService.GetAllSuppliersAsync().Result, "Id", "Name");
            return View();
        }

        // POST: Inventory/Create
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                await _inventoryService.AddProductAsync(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Inventory/StockIn
        [HttpGet("StockIn/{id}")]
        public async Task<IActionResult> StockIn(Guid id)
        {
            var product = await _inventoryService.GetProductById(id);  // Await the Task to get the Product

            return View(new StockTransaction
            {
                ProductId = id,
                Product = product,  // Setting the Product object
                Type = "Stock In",  // Setting the required Type property
                Description = $"Stock In for Product {product.Name}"  // Setting the required Description property
            });
        }

        // POST: Inventory/StockIn
        [HttpPost("StockIn")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StockIn(StockTransaction transaction)
        {
            if (ModelState.IsValid)
            {
                transaction.Type = "Stock In";
                transaction.Description = $"Stock In of {transaction.Quantity} units";
                transaction.Product = await _inventoryService.GetProductById(transaction.ProductId);
                await _inventoryService.StockInAsync(transaction);
                return RedirectToAction(nameof(Index));
            }

            return View(transaction);
        }


        // GET: Inventory/StockOut/{id}
        [HttpGet("StockOut/{id}")]
        public async Task<IActionResult> StockOut(Guid id)
        {
            var product = await _inventoryService.GetProductById(id);  // Await the Task to get the Product

            return View(new StockTransaction
            {
                ProductId = id,
                Product = product,  // Set the Product object
                Type = "Stock Out",  // Set the required Type property
                Description = $"Stock Out for Product {product.Name}"  // Set the required Description property
            });
        }


        // POST: Inventory/StockOut
        [HttpPost("StockOut")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StockOut(StockTransaction transaction)
        {
            if (ModelState.IsValid)
            {
                transaction.Type = "Stock Out";
                transaction.Description = $"Stock Out of {transaction.Quantity} units";
                transaction.Product = await _inventoryService.GetProductById(transaction.ProductId); // Make sure this method exists
                await _inventoryService.StockOutAsync(transaction);
                return RedirectToAction(nameof(Index));
            }
            
            return View(transaction);
        }
        // GET: Inventory/Reports
        [HttpGet("Views/Reports")]
        public async Task<IActionResult> InventoryReport()
        {
            var reportData = await _inventoryService.GenerateStockReportAsync();
            return View("/Views/Reports/InventoryReport.cshtml", reportData);  // Return the view by name
        }

    }
}
