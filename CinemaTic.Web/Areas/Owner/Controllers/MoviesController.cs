using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CinemaTic.Core.Contracts;
using CinemaTic.ViewModels.Movies;
using CinemaTic.Extensions.ModelBinders;
using System.Linq;
using CinemaTic.Web.Areas.Owner.Controllers.BaseControllers;

namespace CinemaTic.Web.Areas.Owner.Controllers
{
    public class MoviesController : OwnerController
    {
        private readonly IMoviesService _moviesService;
        private readonly ICinemasService _cinemasService;

        public MoviesController(IMoviesService moviesService, ICinemasService cinemasService)
        {
            _moviesService = moviesService;
            _cinemasService = cinemasService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _moviesService.GetFilterViewModelAsync());
        }
        public async Task<IActionResult> QueryMovies(string searchText, string filterValue, string sortBy, [ModelBinder(typeof(IdModelBinder))] int? pageNumber)
        {
            var movies = await _moviesService.QueryMoviesAsync(searchText, filterValue, sortBy, pageNumber ?? 1);
            return PartialView("_MoviesPartial", movies);
        }
        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (!await _moviesService.ExistsByIdAsync(id))
            {
                return NotFound();
            }
            var viewModel = await _moviesService.GetDetailsViewModelAsync(id, User.Identity.Name);
            if (viewModel == null)
            {
                return NotFound();
            }
            return View(viewModel);
        }

        // GET: Movies/Create
        public async Task<IActionResult> Add()
        {
            return View(await _moviesService.GetCreateViewModelAsync());
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(CreateMovieViewModel movieVM)
        {
            if (!movieVM.ActorsDropdown.Where(i => i.IsChecked).Any())
            {
                ModelState.AddModelError("ActorsDropdown", "Select at least 1 actor");
            }
            if (ModelState.IsValid)
            {
                await _moviesService.CreateMovieAsync(movieVM, User.Identity.Name);
                return RedirectToAction(nameof(Index));
            }
            return View(movieVM);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit([ModelBinder(typeof(IdModelBinder))] int id)
        {
            if (!await _moviesService.ExistsByIdAsync(id))
            {
                return NotFound();
            }
            var viewModel = await _moviesService.GetEditViewModelAsync(id);
            if (viewModel == null)
            {
                return NotFound();
            }

            return PartialView("_EditMoviePartial", viewModel);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm] EditMovieViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _moviesService.EditMovieAsync(viewModel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    bool movieExists = await _moviesService.ExistsByIdAsync(viewModel.Id);
                    if (!movieExists)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details), new { id = viewModel.Id });
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Delete([ModelBinder(typeof(IdModelBinder))] int id)
        {
            if (!await _moviesService.ExistsByIdAsync(id))
            {
                return NotFound();
            }

            var viewModel = await _moviesService.GetDeleteViewModelAsync(id);
            return PartialView("_DeleteMoviePartial", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (!await _moviesService.ExistsByIdAsync(id))
            {
                return NotFound();
            }
            await _moviesService.DeleteByIdAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMovieToCinemas(MovieDetailsViewModel viewModel)
        {
            if (!await _moviesService.ExistsByIdAsync(viewModel.Id))
            {
                return NotFound();
            }
            await _moviesService.AddMovieToCinemasAsync(viewModel);
            return RedirectToAction(nameof(Details), new { id = viewModel.Id });
        }
        [HttpGet]
        public async Task<IActionResult> SetMovieSchedule(int? cinemaId, int? movieId)
        {
            if (!await _cinemasService.ExistsByIdAsync(cinemaId) || !await _moviesService.ExistsByIdAsync(movieId))
            {
                return NotFound();
            }

            var viewModel = await _moviesService.GetSetMovieScheduleViewModelAsync(cinemaId, movieId);
            if (viewModel == null)
            {
                return NotFound();
            }
            return PartialView("_SetMovieSchedulePartial", viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> SetMovieSchedule(SetMovieScheduleViewModel viewModel)
        {
            if (!await _cinemasService.ExistsByIdAsync(viewModel.CinemaId) || !await _moviesService.ExistsByIdAsync(viewModel.MovieId))
            {
                return NotFound();
            }

            await _moviesService.SetMovieScheduleAsync(viewModel);
            return RedirectToAction(nameof(Details), new { id = viewModel.MovieId });
        }
        [HttpGet]
        public async Task<IActionResult> EditCinemaMovieData(int? cinemaId, int? movieId)
        {
            if (!await _cinemasService.ExistsByIdAsync(cinemaId) || !await _moviesService.ExistsByIdAsync(movieId))
            {
                return NotFound();
            }

            var viewModel = await _moviesService.GetEditCinemaMovieDataViewModelAsync(cinemaId, movieId);
            if (viewModel == null)
            {
                return NotFound();
            }
            return PartialView("_EditCinemaMovieDataPartial", viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> EditCinemaMovieData(EditCinemaMovieDataViewModel viewModel)
        {
            if (!await _cinemasService.ExistsByIdAsync(viewModel.CinemaId) || !await _moviesService.ExistsByIdAsync(viewModel.MovieId))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _moviesService.EditCinemaMovieDataAsync(viewModel);
            }
            return RedirectToAction(nameof(Details), new { id = viewModel.MovieId });
        }
        public async Task<IActionResult> GetCinemasContainingMovie([ModelBinder(typeof(IdModelBinder))] int movieId, string sortBy)
        {
            if (!await _moviesService.ExistsByIdAsync(movieId))
            {
                return NotFound();
            }
            var viewModel = await _cinemasService.QueryCinemasContainingMovieAsync(movieId, User.Identity.Name, sortBy);
            if (viewModel == null)
            {
                return NotFound();
            }
            return PartialView("_CinemasPartial", viewModel);
        }
    }
}
