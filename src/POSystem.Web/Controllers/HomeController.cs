using Microsoft.AspNetCore.Mvc;
using POSystem.Web.Models;
using System.Diagnostics;

namespace POSystem.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //TODO:: Will fetch real data from database
            var analytics = new AnalyticsViewModel
            {
                TotalOrderPlaced = 100,
                TotalUnitSold = 100,
                TotalTransaction = 10000
            };
            return View(analytics);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
