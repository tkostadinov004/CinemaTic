using Cinema.Core.Contracts;
using Cinema.Data.Models;
using Cinema.Extensions.ModelBinders;
using Cinema.ViewModels.Actors;
using Cinema.ViewModels.Cinemas;
using Cinema.ViewModels.Movies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Cinema.Controllers
{
    public class OwnersController : Controller
    {
        private readonly IOwnersService _ownersService;
        private readonly IMoviesService _moviesService;

        public OwnersController(IOwnersService ownersService, IMoviesService moviesService)
        {
            _ownersService = ownersService;
            _moviesService = moviesService;
        }
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> Index()
        {
            return View();
        }
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> UserCinemas()
        {
            return View();
        }
        [Authorize(Roles = "Owner")]
        [HttpGet]
        public IActionResult AddCinema()
        {
            return View();
        }
        [Authorize(Roles = "Owner")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCinema(CreateCinemaViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await _ownersService.CreateCinemaAsync(viewModel, User.Identity.Name);
            }
            return RedirectToAction("UserCinemas", "Owners");
        }
        [Authorize(Roles = "Owner")]
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (!await _ownersService.ExistsByIdAsync(id))
            {
                return NotFound();
            }
            return View(await _ownersService.GetByIdAsync(id));
        }
        [Authorize(Roles = "Owner")]
        [HttpGet]
        public async Task<IActionResult> EditCinema([ModelBinder(typeof(IdModelBinder))] int id)
        {
            if (!await _ownersService.ExistsByIdAsync(id))
            {
                return NotFound();
            }
            return PartialView("_EditCinemaPartial", await _ownersService.GetEditViewModelByIdAsync(id));
        }
        [Authorize(Roles = "Owner")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCinema([FromForm] EditCinemaViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await _ownersService.EditCinemaAsync(viewModel);
            }
            return RedirectToAction("UserCinemas", "Owners");
        }
        [Authorize(Roles = "Owner")]
        [HttpGet]
        public async Task<IActionResult> DeleteCinema(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (!await _ownersService.ExistsByIdAsync(id))
            {
                return NotFound();
            }
            return PartialView("_DeleteCinemaPartial", await _ownersService.PrepareDeleteViewModelAsync(id.Value));
        }
        [Authorize(Roles = "Owner")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCinema([FromForm] DeleteActorViewModel viewModel, int? cinemaId)
        {
            if (!await _ownersService.ExistsByIdAsync(cinemaId))
            {
                return NotFound();
            }
            await _ownersService.DeleteByIdAsync(cinemaId);
            return RedirectToAction("UserCinemas", "Owners");
        }
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> SearchAndFilterCinemas(string searchText, string filterValue, string sortBy)
        {
            var cinemas = await _ownersService.SearchAndFilterCinemasAsync(searchText, filterValue, sortBy, User.Identity.Name);
            return PartialView("_CinemasPartial", cinemas);
        }
        [Authorize(Roles = "Administrator, Owner")]
        public async Task<IActionResult> SearchMoviesByCinema(string searchText, string sortBy, [ModelBinder(typeof(IdModelBinder))] int id)
        {
            if (!await _ownersService.ExistsByIdAsync(id))
            {
                return NotFound();
            }
            var movies = await _ownersService.SearchMoviesByCinemaAsync(searchText, sortBy, id);
            return PartialView("_CinemaMoviesPartial", movies);
        }
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> GenerateCustomPagePreview([ModelBinder(typeof(IdModelBinder))] int cinemaId)
        {
            if (!await _ownersService.ExistsByIdAsync(cinemaId))
            {
                return NotFound();
            }
            return View("_CustomPagePreview", await _ownersService.GetPreviewViewModelAsync(User.Identity.Name, cinemaId));
        }
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> GetCinemasContainingMovie([ModelBinder(typeof(IdModelBinder))] int movieId)
        {
            if (!await _moviesService.ExistsByIdAsync(movieId))
            {
                return NotFound();
            }
            return PartialView("_CinemasMoviePartial", await _ownersService.GetCinemasContainingMovieAsync(movieId, User.Identity.Name));
        }
    }
}
