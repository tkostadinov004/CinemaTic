using Cinema.Core.Contracts;
using Cinema.Core.Services;
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
        public async Task<IActionResult> ChangeApprovalStatus(int id)
        {
            if (id == null)
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
        public async Task<IActionResult> ChangeApprovalStatus(string id, string approvalCode)
        {
            await _adminService.ChangeApprovalStatusAsync(int.Parse(id), int.Parse(approvalCode));
            return Json(new { redirectToUrl = Url.Action("AllCinemas", "Admin") });
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
            var userViewModel = await _adminService.GetAdminUserCRUDPartialAsync(id);
            if (userViewModel == null)
            {
                return NotFound();
            }
            return PartialView("_PromoteToOwnerPartial", userViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> PromoteToOwner([FromForm] AdminUserCRUDViewModel viewModel, string id)
        {
            await _adminService.PromoteUser(id);
            return Json(new { redirectToUrl = Url.Action("Users", "Admin") });
        }
        [HttpGet]
        public async Task<IActionResult> DemoteUser(string id)
        {
            if (id == null)
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
        public async Task<IActionResult> DemoteUser([FromForm] AdminUserCRUDViewModel viewModel, string id)
        {
            await _adminService.DemoteUser(id);
            return Json(new { redirectToUrl = Url.Action("Users", "Admin") });
        }
        [HttpGet]
        public async Task<IActionResult> DeleteUserAccount(string id)
        {
            if (id == null)
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
        public async Task<IActionResult> DeleteUserAccount([FromForm] AdminUserCRUDViewModel viewModel, string id)
        {
            await _adminService.DeleteAccount(id);
            return Json(new { redirectToUrl = Url.Action("Users", "Admin") });
        }
    }
}
