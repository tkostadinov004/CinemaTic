using CinemaTic.Core.Contracts;
using CinemaTic.Core.Services;
using CinemaTic.Data.Enums;
using CinemaTic.Extensions.ModelBinders;
using CinemaTic.ViewModels.Admin;
using CinemaTic.ViewModels.Cinemas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CinemaTic.Web.Controllers
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

            var cinema = await _adminService.GetCinemaDetailsViewModelByIdAsync(id);
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

            var user = await _adminService.GetUserDetailsViewModelByIdAsync(id);
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
            var cinemaViewModel = await _adminService.GetChangeApprovalStatusViewModelByIdAsync(id);
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
            await _adminService.ChangeApprovalByIdStatusAsync(id, approvalCode);
            return RedirectToAction("AllCinemas", "Admin");
        }
        public async Task<IActionResult> SearchAndFilterCinemas(string searchText, string filterValue, string sortBy, int? pageNumber)
        {
            var cinemas = await _adminService.SearchAndFilterCinemasAsync(searchText, filterValue, sortBy, pageNumber);
            return PartialView("_CinemasPartial", cinemas);
        }
        public async Task<IActionResult> SearchAndFilterUsers(string searchText, string filterValue, string sortBy, int? pageNumber)
        {
            var users = await _adminService.SearchAndFilterUsersAsync(searchText, filterValue, sortBy, pageNumber);
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
            await _adminService.DemoteUserAsync(id);
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
            await _adminService.DeleteAccountAsync(id);
            return RedirectToAction("Users", "Admin");
        }
    }
}
