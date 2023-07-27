using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Cinema.Core.Contracts;
using Cinema.ViewModels.Movies;
using Cinema.ViewModels.Cinemas;
using Cinema.Core.Services;
using Cinema.Extensions.ModelBinders;

namespace Cinema.Controllers
{
    [Authorize(Roles = "Owner")]
    public class MoviesController : Controller
    {
        private readonly IMoviesService _moviesService;

        public MoviesController(IMoviesService moviesService)
        {
            _moviesService = moviesService;
        }

        [HttpGet]
        public async Task<IActionResult> AllMovies()
        {
            return View(await _moviesService.PrepareFilterViewModelAsync());
        }
        public async Task<IActionResult> SearchAndFilterMovies(string searchText, string filterValue, string sortBy)
        {
            var movies = await _moviesService.SearchAndFilterMoviesAsync(searchText, filterValue, sortBy);
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
            var movie = await _moviesService.GetByIdAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            var ratings = await _moviesService.GetRatingsByMovieIdAsync(id);
            var viewModel = await _moviesService.GetDetailsViewModel(movie, ratings, User.Identity.Name);
            return View(viewModel);
        }

        // GET: Movies/Create
        public async Task<IActionResult> Create()
        {
            return View(await _moviesService.PrepareForAddingAsync());
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateMovieViewModel movieVM)
        {
            if (ModelState.IsValid)
            {
                await _moviesService.CreateMovieAsync(movieVM, User.Identity.Name);
            }
            return RedirectToAction("AllMovies", "Owners");
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit([ModelBinder(typeof(IdModelBinder))] int id)
        {
            if (!await _moviesService.ExistsByIdAsync(id))
            {
                return NotFound();
            }
            var viewModel = await _moviesService.PrepareForEditing(id);
            if(viewModel == null)
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
                    await _moviesService.EditByIdAsync(viewModel);
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
                return RedirectToAction("AllMovies", "Movies");
            }
            return View();
        }

        // GET: Movies/Delete/5
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> Delete([ModelBinder(typeof(IdModelBinder))] int id)
        {
            if (!await _moviesService.ExistsByIdAsync(id))
            {
                return NotFound();
            }

            var viewModel = await _moviesService.PrepareForDeleting(id);
            return PartialView("_DeleteMoviePartial", viewModel);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (!await _moviesService.ExistsByIdAsync(id))
            {
                return NotFound();
            }
            await _moviesService.DeleteByIdAsync(id);
            return RedirectToAction(nameof(AllMovies));
        }
    }
}
