using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Cinema.Core.Contracts;
using Cinema.ViewModels.Genres;
using Cinema.ViewModels.Actors;

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
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genre = await _genresService.PrepareDetailsViewModelAsync(int.Parse(id));
            if (genre == null)
            {
                return NotFound();
            }
            return PartialView("_GenreDetailsPartial", genre);
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
            return View(viewModel);
        }
        public async Task<IActionResult> SearchAndSortMoviesByGenre(string searchText, string id, string sortBy)
        {
            var movies = await _genresService.SearchAndSortMoviesByGenre(searchText, id, sortBy);
            return PartialView("_GenreMoviesPartial", movies);
        }
        public async Task<IActionResult> SortGenres(string sortBy)
        {
            var genres = await _genresService.SortGenresAsync(sortBy);
            return PartialView("_GenresPartial", genres);
        }
        [HttpGet]
        public async Task<IActionResult> EditGenre(string id)
        {
            return PartialView("_EditGenrePartial", await _genresService.GetEditViewModelByIdAsync(int.Parse(id)));
        }
        [HttpPost]
        public async Task<IActionResult> EditGenre([FromForm] EditGenreViewModel viewModel, int genreId, string name)
        {
            viewModel.Id = genreId;
            viewModel.Name = name;
            await _genresService.EditByIdAsync(viewModel, genreId);
            return Json(Url.Action("AllGenres", "Genres"));
        }
        [HttpGet]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            return PartialView("_DeleteGenrePartial", await _genresService.PrepareDeleteViewModelAsync(id));
        }
        [HttpPost]
        public async Task<IActionResult> DeleteGenre([FromForm] DeleteGenreViewModel viewModel, int genreId)
        {
            await _genresService.DeleteByIdAsync(genreId);
            return Json(Url.Action("AllGenres", "Genres"));
        }
    }
}
