using Cinema.Data.Models;
using Cinema.ViewModels.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Cinema.Controllers
{
    public class LayoutController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public LayoutController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> GetSidebar(string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            var viewModel = new SidebarUserViewModel
            {
                Name = $"{user.FirstName} {user.LastName}",
                ImageUrl = user.ProfilePictureUrl
            };
            return PartialView("_ProfileInfoPartial", viewModel);
        }
    }
}
