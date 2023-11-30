using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WEB_520_Dynamic.Model;

namespace WEB_520_Dynamic.Controllers
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
            ViewData["HideHeader"] = true;
            return View();

        }

        public IActionResult Privacy()
        {
            ViewData["HideHeader"] = true;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}