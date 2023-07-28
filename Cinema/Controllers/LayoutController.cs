using Cinema.Core.Contracts;
using Cinema.Core.Utilities;
using Cinema.Data;
using Cinema.Data.Models;
using Cinema.ViewModels.Users;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace Cinema.Controllers
{
    public class LayoutController : Controller
    {
        private readonly IImageService _imageService;
        private readonly IUsersService _usersService;

        public LayoutController(IImageService imageService, IUsersService usersService)
        {
            _imageService = imageService;
            _usersService = usersService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSidebar()
        {
            var user = await _usersService.GetByEmailAsync(User.Identity.Name);
            await _imageService.ReplaceWithDefaultIfNotPresentAsync(User.Identity.Name, "Users", user.ProfilePictureUrl);

            return PartialView("_ProfileInfoPartial", await _usersService.GetSidebarViewModelAsync(User.Identity.Name));
        }
        [HttpGet]
        public async Task<IActionResult> GetProfilePictureUrl()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _usersService.GetByEmailAsync(User.Identity.Name);
                await _imageService.ReplaceWithDefaultIfNotPresentAsync(User.Identity.Name, "Users", user.ProfilePictureUrl);

                return PhysicalFile(Path.Combine(Constants.ImagesFolder, "Users", user.ProfilePictureUrl), "image/png");
            }
            return Ok();
        }
    }
}
