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
            return View(await _adminService.GetAllAsync());
        }
        public async Task<IActionResult> AllCinemas()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Cinema(int? id)
        {
            return View(await _adminService.GetCinemaDetailsAsync(id));
        }
        public async Task<IActionResult> User(string userId)
        {
            return View(await _adminService.GetUserDetailsAsync(userId));
        }
        [HttpGet]
        public async Task<IActionResult> ChangeApprovalStatus(int id)
        {
            return PartialView("_ChangeApprovalStatusPartial", await _adminService.GetCASViewModelAsync(id));
        }
        [HttpPost]
        public async Task<IActionResult> ChangeApprovalStatus(string id, string approvalCode)
        {
            await _adminService.ChangeApprovalStatusAsync(int.Parse(id), int.Parse(approvalCode));
            return Json(new { redirectToUrl = Url.Action("AllCinemas", "Admin") });
        }
        public async Task<IActionResult> SearchAndFilterCinemas(string searchText,  string filterValue, string sortBy)
        {
            var cinemas = await _adminService.SearchAndFilterCinemasAsync(searchText, filterValue, sortBy);
            return PartialView("_CinemasPartial", cinemas);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteFromOwnerRole(string ownerId)
        {
            var owner = await _adminService.FindById(ownerId);
            if (owner == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(owner);
        }
        [HttpPost, ActionName("DeleteFromOwnerRole")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _adminService.DemoteUser(id);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> DeleteVisitorAccount(string userId)
        {
            var visitor = await _adminService.FindById(userId);
            if (visitor == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(visitor);
        }
        [HttpPost, ActionName("DeleteVisitorAccount")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteVisitorAccountConfirmed(string id)
        {
            await _adminService.DeleteAccount(id);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> SetToOwner(string userId)
        {
            return View(await _adminService.FindById(userId));
        }
        [HttpPost, ActionName("SetToOwner")]
        public async Task<IActionResult> SetToOwnerConfirmed(string userId)
        {
            await _adminService.PromoteUser(userId);
            return RedirectToAction(nameof(Index));
        }
    }
}
