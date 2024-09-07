using HinduTempleofTriStates.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace HinduTempleofTriStates.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        // Injecting the logger service into the HomeController constructor
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Action for the default home page (Index)
        
        public IActionResult Index()
        {
            // Log an informational message when the Index page is accessed
            _logger.LogInformation("Accessed the Home/Index page.");
            return View();
        }

        // Action for the Privacy page
        
        public IActionResult Privacy()
        {
            // Log an informational message when the Privacy page is accessed
            _logger.LogInformation("Accessed the Home/Privacy page.");
            return View();
        }

        // Action for handling errors
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // Log the error occurrence
            _logger.LogError("An error occurred.");

            // Return the Error view with the ErrorViewModel, containing the current RequestId
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
