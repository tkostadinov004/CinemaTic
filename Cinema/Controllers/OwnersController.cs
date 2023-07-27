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
    [Authorize(Roles = "Owner")]
    public class OwnersController : Controller
    {
        private readonly IOwnersService _ownersService;
        private readonly IMoviesService _moviesService;

        public OwnersController(IOwnersService ownersService, IMoviesService moviesService)
        {
            _ownersService = ownersService;
            _moviesService = moviesService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<IActionResult> UserCinemas()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddCinema()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCinema(AddCinemaViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await _ownersService.AddAsync(viewModel, User.Identity.Name);
            }
            return RedirectToAction("UserCinemas", "Owners");
        }
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMovieToCinemas(MovieDetailsViewModel viewModel)
        {
            if (!await _ownersService.ExistsByIdAsync(viewModel.MovieId))
            {
                return NotFound();
            }
            await _ownersService.AddMovieToCinemas(viewModel);
            return RedirectToAction("AllMovies", "Movies");
        }
        [HttpGet]
        public async Task<IActionResult> EditCinema([ModelBinder(typeof(IdModelBinder))] int id)
        {
            if (!await _ownersService.ExistsByIdAsync(id))
            {
                return NotFound();
            }
            return PartialView("_EditCinemaPartial", await _ownersService.GetEditViewModelByIdAsync(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCinema([FromForm] EditCinemaViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await _ownersService.EditCinema(viewModel);
            }
            return RedirectToAction("UserCinemas", "Owners");
        }
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

        public async Task<IActionResult> SearchAndFilterCinemas(string searchText, string filterValue, string sortBy)
        {
            var cinemas = await _ownersService.SearchAndFilterCinemasAsync(searchText, User.Identity.Name, filterValue, sortBy);
            return PartialView("_CinemasPartial", cinemas);
        }
        public async Task<IActionResult> SearchMoviesByCinema(string searchText, [ModelBinder(typeof(IdModelBinder))] int id)
        {
            if (!await _ownersService.ExistsByIdAsync(id))
            {
                return NotFound();
            }
            var movies = await _ownersService.SearchMoviesByCinema(searchText, id);
            return PartialView("_CinemaMoviesPartial", movies);
        }
        public async Task<IActionResult> GenerateCustomPagePreview([ModelBinder(typeof(IdModelBinder))] int cinemaId)
        {
            if (!await _ownersService.ExistsByIdAsync(cinemaId))
            {
                return NotFound();
            }
            return View("_CustomPagePreview", await _ownersService.PreparePreviewViewModelAsync(User.Identity.Name, cinemaId));
        }
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
