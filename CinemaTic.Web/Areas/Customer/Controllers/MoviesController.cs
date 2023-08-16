using CinemaTic.Core.Contracts;
using CinemaTic.Extensions.ModelBinders;
using CinemaTic.Web.Areas.Customer.Controllers.BaseControllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CinemaTic.Web.Areas.Customer.Controllers
{
    public class MoviesController : CustomerController
    {
        private readonly IMoviesService _moviesService;
        private readonly ICinemasService _cinemasService;
        private readonly ICustomersService _customersService;

        public MoviesController(IMoviesService moviesService, ICinemasService cinemasService, ICustomersService customersService)
        {
            _moviesService = moviesService;
            _cinemasService = cinemasService;
            _customersService = customersService;
        }

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
            return View(await _moviesService.GetDetailsViewModelAsync(id, User.Identity.Name));
        }
        [HttpGet]
        public async Task<IActionResult> GetMoviesByDate([ModelBinder(typeof(IntegerModelBinder))] int cinemaId, [ModelBinder(typeof(DateModelBinder))] DateTime date)
        {
            if (!await _cinemasService.ExistsByIdAsync(cinemaId))
            {
                return NotFound();
            }
            return PartialView("_MoviesByDatePartial", await _customersService.GetMoviesByDateAsync(cinemaId, date));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetRating(int? movieId, int? rating)
        {
            if (movieId == null)
            {
                return NotFound();
            }
            if (!await _moviesService.ExistsByIdAsync(movieId))
            {
                return NotFound();
            }
            if (rating == 0 || rating == null)
            {
                return RedirectToAction(nameof(Details), new { id = movieId });
            }

            await _customersService.SetRatingToMovieAsync(movieId, rating.Value, User.Identity.Name);

            return RedirectToAction(nameof(Details), new { id = movieId });
        }
    }
}
