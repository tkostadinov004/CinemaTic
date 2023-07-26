using Cinema.Core.Contracts;
using Cinema.ViewModels.Actors;
using Cinema.ViewModels.Cinemas;
using Cinema.ViewModels.Movies;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Cinema.Controllers
{
    public class OwnersController : Controller
    {
        private IOwnersService _ownersService;
        private ISectorsService _sectorsService;
        private ITicketsService _ticketsService;

        public OwnersController(IOwnersService ownersService, ISectorsService sectorsService, ITicketsService ticketsService)
        {
            _ownersService = ownersService;
            _sectorsService = sectorsService;
            _ticketsService = ticketsService;
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
        public async Task<IActionResult> AddCinema(AddCinemaViewModel viewModel, string userEmail)
        {
            if (ModelState.IsValid)
            {
                await _ownersService.AddAsync(viewModel, userEmail);
            }
            return RedirectToAction("UserCinemas", "Owners");
        }
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            return View(await _ownersService.GetByIdAsync(id));
        }
        [HttpPost]
        public async Task<IActionResult> AddMovieToCinemas(MovieDetailsViewModel viewModel)
        {
            await _ownersService.AddMovieToCinemas(viewModel);
            return RedirectToAction("AllMovies", "Movies");
        }
        [HttpGet]
        public async Task<IActionResult> EditCinema(string id)
        {
            return PartialView("_EditCinemaPartial", await _ownersService.GetEditViewModelByIdAsync(int.Parse(id)));
        }
        [HttpPost]
        public async Task<IActionResult> EditCinema([FromForm] EditCinemaViewModel viewModel, int cinemaId)
        {
            viewModel.Id = cinemaId;
            await _ownersService.EditCinema(viewModel);
            return RedirectToAction("UserCinemas", "Owners");
        }
        [HttpGet]
        public async Task<IActionResult> DeleteCinema(int id)
        {
            return PartialView("_DeleteCinemaPartial", await _ownersService.PrepareDeleteViewModelAsync(id));
        }
        [HttpPost]
        public async Task<IActionResult> DeleteCinema([FromForm] DeleteActorViewModel viewModel, int cinemaId)
        {
            await _ownersService.DeleteByIdAsync(cinemaId);
            return RedirectToAction("UserCinemas", "Owners");
        }

        public async Task<IActionResult> SearchAndFilterCinemas(string searchText, string userEmail, string filterValue, string sortBy)
        {
            var cinemas = await _ownersService.SearchAndFilterCinemasAsync(searchText, userEmail, filterValue, sortBy);
            return PartialView("_CinemasPartial", cinemas);
        }
        public async Task<IActionResult> SearchMoviesByCinema(string searchText, string id)
        {
            var movies = await _ownersService.SearchMoviesByCinema(searchText, id);
            return PartialView("_CinemaMoviesPartial", movies);
        }
        public async Task<IActionResult> GenerateCustomPagePreview(string userEmail, string cinemaId)
        {
            return View("_CustomPagePreview", await _ownersService.PreparePreviewViewModelAsync(userEmail, cinemaId));
        }
        public async Task<IActionResult> GetCinemasContainingMovie(string movieId, string userEmail)
        {
            return PartialView("_CinemasMoviePartial", await _ownersService.GetCinemasContainingMovieAsync(movieId, userEmail));
        }
    }
}
