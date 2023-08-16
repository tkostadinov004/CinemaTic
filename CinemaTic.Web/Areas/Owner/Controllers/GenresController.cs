using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CinemaTic.Core.Contracts;
using CinemaTic.ViewModels.Genres;
using CinemaTic.Extensions.ModelBinders;
using CinemaTic.Web.Areas.Owner.Controllers.BaseControllers;

namespace CinemaTic.Web.Areas.Owner.Controllers
{
    public class GenresController : OwnerController
    {
        private readonly IGenresService _genresService;

        public GenresController(IGenresService genresService)
        {
            _genresService = genresService;
        }

        // GET: Genres
        public async Task<IActionResult> Index()
        {
            return View();
        }

        // GET: Genres/Details/5
        public async Task<IActionResult> Details([ModelBinder(typeof(IntegerModelBinder))] int? id)
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
            return View(genre);
        }

        // GET: Genres/Create
        public IActionResult Add()
        {
            return PartialView("_AddGenrePartial");
        }

        // POST: Genres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([FromForm] CreateGenreViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await _genresService.CreateAsync(viewModel);
            }
            return View(viewModel);
        }
        public async Task<IActionResult> QueryMoviesByGenre(string searchText, string sortBy, [ModelBinder(typeof(IntegerModelBinder))] int id, [ModelBinder(typeof(IntegerModelBinder))] int? pageNumber)
        {
            if (!await _genresService.ExistsByIdAsync(id))
            {
                return NotFound();
            }
            var movies = await _genresService.QueryMoviesByGenreAsync(id, searchText, sortBy, pageNumber ?? 1);
            return PartialView("_GenreMoviesPartial", movies);
        }
        public async Task<IActionResult> QueryGenres(string sortBy, int? pageNumber)
        {
            var genres = await _genresService.QueryGenresAsync(sortBy, pageNumber);
            return PartialView("_GenresPartial", genres);
        }
        [HttpGet]
        public async Task<IActionResult> Edit([ModelBinder(typeof(IntegerModelBinder))] int id)
        {
            if (!await _genresService.ExistsByIdAsync(id))
            {
                return NotFound();
            }
            return PartialView("_EditGenrePartial", await _genresService.GetEditViewModelByIdAsync(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm] EditGenreViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await _genresService.EditAsync(viewModel);
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
            if (!await _genresService.ExistsByIdAsync(id))
            {
                return NotFound();
            }
            return PartialView("_DeleteGenrePartial", await _genresService.GetDeleteViewModelByIdAsync(id.Value));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromForm] DeleteGenreViewModel viewModel, int? genreId)
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

            return RedirectToAction(nameof(Index));
        }
    }
}
