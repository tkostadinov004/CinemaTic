using Cinema.Core.Contracts;
using Cinema.Core.Services;
using Cinema.Data.Enums;
using Cinema.Extensions.ModelBinders;
using Cinema.ViewModels.Admin;
using Cinema.ViewModels.Cinemas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Cinema.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<IActionResult> AllCinemas()
        {
            return View();
        }
        public async Task<IActionResult> Users()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Cinema(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cinema = await _adminService.GetCinemaDetailsAsync(id);
            if (cinema == null)
            {
                return NotFound();
            }

            return View(cinema);
        }
        public async Task<IActionResult> User(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _adminService.GetUserDetailsAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
        [HttpGet]
        public async Task<IActionResult> ChangeApprovalStatus([ModelBinder(typeof(IdModelBinder))] int id)
        {
            if (!await _adminService.CinemaExistsAsync(id)) 
            {
                return NotFound();
            }
            var cinemaViewModel = await _adminService.GetCASViewModelAsync(id);
            if (cinemaViewModel == null)
            {
                return NotFound();
            }
            return PartialView("_ChangeApprovalStatusPartial", cinemaViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeApprovalStatus([ModelBinder(typeof(IdModelBinder))] int id, [ModelBinder(typeof(ApprovalCodeBinder))] ApprovalStatus approvalCode)
        {
            if (!await _adminService.CinemaExistsAsync(id))
            {
                return NotFound();
            }
            await _adminService.ChangeApprovalStatusAsync(id, approvalCode);
            return RedirectToAction("AllCinemas", "Admin");
        }
        public async Task<IActionResult> SearchAndFilterCinemas(string searchText, string filterValue, string sortBy)
        {
            var cinemas = await _adminService.SearchAndFilterCinemasAsync(searchText, filterValue, sortBy);
            return PartialView("_CinemasPartial", cinemas);
        }
        public async Task<IActionResult> SearchAndFilterUsers(string searchText, string filterValue)
        {
            var users = await _adminService.SearchAndFilterUsersAsync(searchText, filterValue);
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
            await _adminService.PromoteUser(id);
            return RedirectToAction("Users", "Admin");
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
            await _adminService.DemoteUser(id);
            return RedirectToAction("Users", "Admin");
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
            await _adminService.DeleteAccount(id);
            return RedirectToAction("Users", "Admin");
        }
    }
}
