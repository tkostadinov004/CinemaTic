using CinemaTic.Core.Contracts;
using CinemaTic.Data.Enums;
using CinemaTic.Extensions.ModelBinders;
using CinemaTic.Web.Areas.Admin.Controllers.BaseControllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CinemaTic.Web.Areas.Admin.Controllers
{
    public class CinemasController : AdminController
    {
        private readonly IAdminService _adminService;
        private readonly ICinemasService _cinemasService;

        public CinemasController(IAdminService adminService, ICinemasService cinemasService)
        {
            _adminService = adminService;
            _cinemasService = cinemasService;
        }

        public async Task<IActionResult> Index()
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
            await _adminService.ChangeApprovalStatusByIdStatusAsync(id, approvalCode);
            return RedirectToAction(nameof(Index), "Cinemas");
        }
        public async Task<IActionResult> QueryCinemas(string searchText, string filterValue, string sortBy, int? pageNumber)
        {
            var cinemas = await _adminService.QueryCinemasAsync(searchText, filterValue, sortBy, pageNumber);
            return PartialView("_CinemasPartial", cinemas);
        }
        public async Task<IActionResult> QueryMoviesByCinema([ModelBinder(typeof(IdModelBinder))] int id, string sortBy)
        {
            if (!await _cinemasService.ExistsByIdAsync(id))
            {
                return NotFound();
            }
            var movies = await _cinemasService.QueryMoviesByCinemaAsync(id, "", sortBy);
            return PartialView("_CinemaMoviesPartial", movies);
        }
    }
}
