using CinemaTic.Core.Contracts;
using CinemaTic.Extensions.ModelBinders;
using CinemaTic.ViewModels.Actors;
using CinemaTic.ViewModels.Cinemas;
using CinemaTic.Web.Areas.Owner.Controllers.BaseControllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CinemaTic.Web.Areas.Owner.Controllers
{
    public class CinemasController : OwnerController
    {
        private readonly ICinemasService _cinemasService;

        public CinemasController(ICinemasService cinemasService)
        {
            _cinemasService = cinemasService;
        }  
        public async Task<IActionResult> Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(CreateCinemaViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await _cinemasService.CreateCinemaAsync(viewModel, User.Identity.Name);
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (!await _cinemasService.ExistsByIdAsync(id))
            {
                return NotFound();
            }

            if(!await _cinemasService.OwnerHasCinemaAsync(id, User.Identity.Name))
            {
                return Forbid();
            }
            return View(await _cinemasService.GetDetailsViewModelByIdAsync(id));
        }
        [HttpGet]
        public async Task<IActionResult> Edit([ModelBinder(typeof(IdModelBinder))] int id)
        {
            if (!await _cinemasService.ExistsByIdAsync(id))
            {
                return NotFound();
            }
            return PartialView("_EditCinemaPartial", await _cinemasService.GetEditViewModelByIdAsync(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm] EditCinemaViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await _cinemasService.EditCinemaAsync(viewModel);
            }
            return RedirectToAction(nameof(Details), new { id = viewModel.Id });
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (!await _cinemasService.ExistsByIdAsync(id))
            {
                return NotFound();
            }
            return PartialView("_DeleteCinemaPartial", await _cinemasService.GetDeleteViewModelAsync(id.Value));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromForm] DeleteActorViewModel viewModel, int? cinemaId)
        {
            if (!await _cinemasService.ExistsByIdAsync(cinemaId))
            {
                return NotFound();
            }
            await _cinemasService.DeleteByIdAsync(cinemaId);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> QueryCinemas(string searchText, string filterValue, string sortBy)
        {
            var cinemas = await _cinemasService.QueryCinemasAsync(searchText, filterValue, sortBy, User.Identity.Name);
            return PartialView("_CinemasPartial", cinemas);
        }
        public async Task<IActionResult> QueryMoviesByCinema([ModelBinder(typeof(IdModelBinder))] int id, string searchText, string sortBy)
        {
            if (!await _cinemasService.ExistsByIdAsync(id))
            {
                return NotFound();
            }
            var movies = await _cinemasService.QueryMoviesByCinemaAsync(id, searchText, sortBy);
            return PartialView("_CinemaMoviesPartial", movies);
        }
        public async Task<IActionResult> GenerateCustomPagePreview([ModelBinder(typeof(IdModelBinder))] int id)
        {
            if (!await _cinemasService.ExistsByIdAsync(id))
            {
                return NotFound();
            }
            return View("_CustomPagePreview", await _cinemasService.GetPreviewViewModelAsync(User.Identity.Name, id));
        }      
    }
}
