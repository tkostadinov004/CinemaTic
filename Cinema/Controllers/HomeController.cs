using Cinema.Data;
using Cinema.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;

namespace Cinema.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CinemaDbContext _context;

        public HomeController(ILogger<HomeController> logger, CinemaDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            if (User.IsInRole("Owner"))
            {
                return RedirectToAction("Index", "Owners");
            }
            if (User.IsInRole("Administrator"))
            {
                return RedirectToAction("Index", "Admin");
            }
            if (User.IsInRole("Customer"))
            {
                return RedirectToAction("Index", "Customer");
            }
            return View();
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
