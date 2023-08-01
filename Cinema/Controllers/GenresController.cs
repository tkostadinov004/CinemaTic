using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Cinema.Core.Contracts;
using Cinema.ViewModels.Genres;
using Cinema.ViewModels.Actors;
using Cinema.Extensions.ModelBinders;

namespace Cinema.Controllers
{
    [Authorize(Roles = "Owner")]
    public class GenresController : Controller
    {
        private readonly IGenresService _genresService;

        public GenresController(IGenresService genresService)
        {
            _genresService = genresService;
        }

        // GET: Genres
        public async Task<IActionResult> AllGenres()
        {
            return View();
        }

        // GET: Genres/Details/5
        public async Task<IActionResult> Details([ModelBinder(typeof(IdModelBinder))] int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (!await _genresService.ExistsByIdAsync(id))
            {
                return NotFound();
            }
            var genre = await _genresService.GetDetailsViewModelByIdAsync(id);
            if (genre == null)
            {
                return NotFound();
            }
            return View("Details", genre);
        }

        // GET: Genres/Create
        public IActionResult CreateGenre()
        {
            return PartialView("_AddGenrePartial");
        }

        // POST: Genres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] CreateGenreViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await _genresService.CreateAsync(viewModel);
                return RedirectToAction(nameof(AllGenres));
            }
            return PartialView("_AddGenrePartial", viewModel);
        }
        public async Task<IActionResult> SearchAndSortMoviesByGenre(string searchText, string sortBy, [ModelBinder(typeof(IdModelBinder))] int id, [ModelBinder(typeof(IdModelBinder))] int? pageNumber)
        {
            if (!await _genresService.ExistsByIdAsync(id))
            {
                return NotFound();
            }
            var movies = await _genresService.SearchAndSortMoviesByGenre(id, searchText, sortBy, pageNumber ?? 1);
            return PartialView("_GenreMoviesPartial", movies);
        }
        public async Task<IActionResult> SortGenres(string sortBy)
        {
            var genres = await _genresService.SortGenresAsync(sortBy);
            return PartialView("_GenresPartial", genres);
        }
        [HttpGet]
        public async Task<IActionResult> EditGenre([ModelBinder(typeof(IdModelBinder))] int id)
        {
            if (!await _genresService.ExistsByIdAsync(id))
            {
                return NotFound();
            }
            return PartialView("_EditGenrePartial", await _genresService.GetEditViewModelByIdAsync(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditGenre([FromForm] EditGenreViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await _genresService.EditByIdAsync(viewModel);
            }
            return Json(Url.Action("AllGenres", "Genres"));
        }
        [HttpGet]
        public async Task<IActionResult> DeleteGenre(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (!await _genresService.ExistsByIdAsync(id))
            {
                return NotFound();
            }
            return PartialView("_DeleteGenrePartial", await _genresService.GetDeleteViewModelByIdAsync(id.Value));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteGenre([FromForm] DeleteGenreViewModel viewModel, int? genreId)
        {
            if (genreId == null)
            {
                return NotFound();
            }
            if (!await _genresService.ExistsByIdAsync(genreId))
            {
                return NotFound();
            }
            await _genresService.DeleteByIdAsync(genreId);
            return RedirectToAction("AllGenres", "Genres");
        }
    }
}
