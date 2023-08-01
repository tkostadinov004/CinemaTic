using CinemaTic.Core.Contracts;
using CinemaTic.Core.Utilities;
using CinemaTic.Data;
using CinemaTic.Data.Models;
using CinemaTic.ViewModels.Users;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace CinemaTic.Web.Controllers
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
            var user = await _usersService.GetUserByEmailAsync(User.Identity.Name);
            await _imageService.ReplaceWithDefaultIfNotPresentAsync(User.Identity.Name, "Users", user.ProfilePictureUrl);

            return PartialView("_ProfileInfoPartial", await _usersService.GetSidebarViewModelByEmailAsync(User.Identity.Name));
        }
        [HttpGet]
        public async Task<IActionResult> GetProfilePictureUrl()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _usersService.GetUserByEmailAsync(User.Identity.Name);
                await _imageService.ReplaceWithDefaultIfNotPresentAsync(User.Identity.Name, "Users", user.ProfilePictureUrl);

                return PhysicalFile(Path.Combine(Constants.ImagesFolder, "Users", user.ProfilePictureUrl), "image/png");
            }
            return Ok();
        }
    }
}
