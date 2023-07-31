using Cinema.Core.Contracts;
using Cinema.ViewModels.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Controllers
{
    [Authorize(Roles = "Owner, Customer")]
    public class UserController : Controller
    {
        private readonly IUsersService _userService;

        public UserController(IUsersService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            return View("ChangePassword", await _userService.GetChangePasswordViewModelAsync(User.Identity.Name));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("ChangePassword", await _userService.GetChangePasswordViewModelAsync(User.Identity.Name));
            }
            await _userService.ChangePasswordAsync(viewModel);

            if (User.IsInRole("Owner"))
            {
                return RedirectToAction("Index", "Owners");
            }
            else if(User.IsInRole("Customer"))
            {
                return RedirectToAction("Index", "Customer");
            }
            else
            {
                return RedirectToAction("Index", "Admin");
            }
        }
        [HttpGet]
        public async Task<IActionResult> ChangeProfilePicture()
        {
            return View("ChangeProfilePicture", await _userService.GetChangeProfilePictureViewModelAsync(User.Identity.Name));
        }
        [HttpPost]
        public async Task<IActionResult> ChangeProfilePicture(ChangeProfilePictureViewModel viewModel)
        {
            await _userService.ChangeProfilePictureViewModelAsync(viewModel);

            if (User.IsInRole("Owner"))
            {
                return RedirectToAction("Index", "Owners");
            }
            else if (User.IsInRole("Customer"))
            {
                return RedirectToAction("Index", "Customer");
            }
            else
            {
                return RedirectToAction("Index", "Admin");
            }
        }
    }
}
