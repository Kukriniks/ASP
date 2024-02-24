using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication3.Models;

namespace WebApplication3.Controllers
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
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult MyName()
        {
            return View();
        }

        [HttpPost]
        public IActionResult MyName(string name)
        {
            if (name != null)
            {
                ViewData["Head"] = name;                
                return RedirectToAction("Index2", new { Name = name });
            }
            ViewData["Head"] = "Try once again";
            return View();
        }

        public IActionResult Index2(string name)
        {
            ViewData["Head"] = name;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
