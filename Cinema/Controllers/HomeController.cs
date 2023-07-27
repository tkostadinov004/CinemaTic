using Cinema.Core.Contracts;
using Cinema.Core.Services;
using Cinema.Core.Utilities;
using Cinema.Data;
using Cinema.Data.Enums;
using Cinema.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CinemaDbContext _context;
        private readonly ILogService _logService;

        public HomeController(ILogger<HomeController> logger, CinemaDbContext context, ILogService logService)
        {
            _logger = logger;
            _context = context;
            _logService = logService;
        }

        public async Task<IActionResult> Index()
        {
            if (TempData.ContainsKey("UserLoggedIn"))
            {
                TempData.Remove("UserLoggedIn");
                await _logService.LogActionAsync(UserActionType.AccountActions, LogMessages.UserLoginMessage);
            }

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
