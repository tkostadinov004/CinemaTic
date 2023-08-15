using CinemaTic.Core.Contracts;
using CinemaTic.ViewModels.Admin;
using CinemaTic.Web.Areas.Admin.Controllers.BaseControllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CinemaTic.Web.Areas.Admin.Controllers
{
    public class UsersController : AdminController
    {
        private readonly IAdminService _adminService;

        public UsersController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<IActionResult> User(string id, int? actionPageNumber)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _adminService.GetUserDetailsViewModelByIdAsync(id, actionPageNumber);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
        public async Task<IActionResult> QueryUsers(string searchText, string filterValue, string sortBy, int? pageNumber)
        {
            var users = await _adminService.QueryUsersAsync(searchText, filterValue, sortBy, pageNumber);
            return PartialView("_UsersPartial", users);
        }
        public async Task<IActionResult> PromoteToOwner(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (!await _adminService.UserExistsAsync(id))
            {
                return NotFound();
            }
            var userViewModel = await _adminService.GetAdminUserCRUDPartialAsync(id);
            if (userViewModel == null)
            {
                return NotFound();
            }
            return PartialView("_PromoteToOwnerPartial", userViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PromoteToOwner([FromForm] AdminUserCRUDViewModel viewModel, string id)
        {
            if (!await _adminService.UserExistsAsync(id))
            {
                return NotFound();
            }
            await _adminService.PromoteUserAsync(id);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> DemoteUser(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (!await _adminService.UserExistsAsync(id))
            {
                return NotFound();
            }
            var userViewModel = await _adminService.GetAdminUserCRUDPartialAsync(id);
            if (userViewModel == null)
            {
                return NotFound();
            }
            return PartialView("_DemoteUserPartial", userViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DemoteUser([FromForm] AdminUserCRUDViewModel viewModel, string id)
        {
            if (!await _adminService.UserExistsAsync(id))
            {
                return NotFound();
            }
            await _adminService.DemoteUserAsync(id);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> DeleteUserAccount(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (!await _adminService.UserExistsAsync(id))
            {
                return NotFound();
            }
            var userViewModel = await _adminService.GetAdminUserCRUDPartialAsync(id);
            if (userViewModel == null)
            {
                return NotFound();
            }
            return PartialView("_DeleteUserAccountPartial", userViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUserAccount([FromForm] AdminUserCRUDViewModel viewModel, string id)
        {
            if (!await _adminService.UserExistsAsync(id))
            {
                return NotFound();
            }
            await _adminService.DeleteAccountAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
