﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Cinema.Core.Contracts;
using Cinema.ViewModels.Movies;
using Cinema.ViewModels.Cinemas;
using Cinema.Core.Services;

namespace Cinema.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMoviesService _moviesService;

        public MoviesController(IMoviesService moviesService)
        {
            _moviesService = moviesService;
        }

        // GET: Movies
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> Index()
        {
            return View(await _moviesService.GetAllAsync());
        }
        [HttpGet]
        public async Task<IActionResult> AllMovies()
        {
            return View(await _moviesService.PrepareFilterViewModelAsync());
        }
        public async Task<IActionResult> SearchAndFilterMovies(string searchText, string filterValue)
        {
            var movies = await _moviesService.SearchAndFilterMoviesAsync(searchText, filterValue);
            return PartialView("_MoviesPartial", movies);
        }
        // GET: Movies/Details/5
        //[Authorize(Roles = "Owner")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
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
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> Create()
        {
            return View(await _moviesService.PrepareForAddingAsync());
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> Create(CreateMovieViewModel movieVM, string userEmail)
        {
            if (ModelState.IsValid)
            {
                await _moviesService.CreateMovieAsync(movieVM, userEmail);
            }
            return RedirectToAction("AllMovies", "Owners");
        }

        // GET: Movies/Edit/5
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _moviesService.GetByIdAsync(int.Parse(id));
            if (movie == null)
            {
                return NotFound();
            }
            var viewModel = await _moviesService.PrepareForEditing(int.Parse(id));

            return PartialView("_EditMoviePartial", viewModel);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> Edit([FromForm] EditMovieViewModel viewModel, int id, int genreId, IEnumerable<string> acts)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    viewModel.Id = id;
                    await _moviesService.EditByIdAsync(viewModel, id, genreId, acts);
                }
                catch (DbUpdateConcurrencyException)
                {
                    bool movieExists = await _moviesService.ExistsByIdAsync(id); //or vm.Id
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
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _moviesService.GetByIdAsync(int.Parse(id));
            if (movie == null)
            {
                return NotFound();
            }
            var viewModel = await _moviesService.PrepareForDeleting(int.Parse(id));
            return PartialView("_DeleteMoviePartial", viewModel);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _moviesService.DeleteByIdAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> SetRating(int? id)
        {
            return Ok(); //todo: remove
        }
        [HttpPost]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> SetRating(int movieId, decimal userRating)
        {
            if (userRating == 0)
            {
                return RedirectToAction("Details", new { id = movieId });
            }

            await _moviesService.SetRatingAsync(movieId, userRating, User.Identity.Name);

            return RedirectToAction("Details", new { id = movieId });
        }
        [AllowAnonymous]
        public async Task<IActionResult> WatchTrailer(int id)
        {
            return View(await _moviesService.GetByIdAsync(id));
        }
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> Statistics()
        {
            var viewModel = await _moviesService.GetStatistics();

            return View(viewModel);
        }
    }
}
