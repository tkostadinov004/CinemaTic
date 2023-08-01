using CinemaTic.Core.Contracts;
using CinemaTic.Core.Utilities;
using CinemaTic.Data.Enums;
using CinemaTic.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CinemaTic.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogService _logService;
        private readonly IUsersService _usersService;
        private readonly IImageService _imageService;

        public HomeController(ILogService logService, IUsersService usersService, IImageService imageService)
        {
            _logService = logService;
            _usersService = usersService;
            _imageService = imageService;
        }

        public async Task<IActionResult> Index()
        {
            if (TempData.ContainsKey("UserLoggedIn"))
            {
                TempData.Remove("UserLoggedIn");
                await _logService.LogActionAsync(UserActionType.AccountActions, LogMessages.UserLoginMessage);
            }
            if (TempData.ContainsKey("UserRegistered"))
            {
                TempData.Remove("UserRegistered");
                await _logService.LogActionAsync(UserActionType.AccountActions, LogMessages.UserRegisterMessage);
            }

            if (User.Identity.IsAuthenticated)
            {
                var user = await _usersService.GetUserByEmailAsync(User.Identity.Name);
                await _imageService.ReplaceWithDefaultIfNotPresentAsync(user.Email, "Users", user.ProfilePictureUrl);
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
