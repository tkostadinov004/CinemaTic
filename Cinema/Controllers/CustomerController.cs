using Cinema.Core.Contracts;
using Cinema.Core.Services;
using Cinema.Core.Utilities;
using Cinema.Data.Models;
using Cinema.Extensions.ModelBinders;
using Cinema.ViewModels.Customers;
using Cinema.ViewModels.Sectors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Cinema.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CustomerController : Controller
    {
        private readonly ICustomersService _customersService;
        private readonly ISectorsService _sectorsService;
        private readonly IMoviesService _moviesService;
        private readonly IOwnersService _ownersService;

        public CustomerController(ICustomersService customersService, ISectorsService sectorsService, IMoviesService moviesService, IOwnersService ownersService)
        {
            _customersService = customersService;
            _sectorsService = sectorsService;
            _moviesService = moviesService;
            _ownersService = ownersService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _customersService.GetCinemasForUserAsync(User.Identity.Name));
        }
        public async Task<IActionResult> MovieDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (!await _moviesService.ExistsByIdAsync(id))
            {
                return NotFound();
            }
            return View("MovieDetails", await _moviesService.GetDetailsViewModelAsync(id, User.Identity.Name));
        }
        public async Task<IActionResult> Cinemas(bool? all, int? pageNumber)
        {
            TempData["All"] = all;
            var cinemas = await _customersService.GetCinemasByUserAsync(all, User.Identity.Name);
            return View("Cinemas", await PaginatedList<CinemasViewModel>.CreateAsync(cinemas, pageNumber ?? 1, 5));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCinemaToFavorites(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (await _ownersService.ExistsByIdAsync(id))
            {
                return NotFound();
            }
            await _customersService.AddCinemaToFavoritesAsync(id.Value, User.Identity.Name);
            return RedirectToAction("Cinemas", new { all = false });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveCinemaFromFavorites(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (await _ownersService.ExistsByIdAsync(id))
            {
                return NotFound();
            }
            await _customersService.RemoveCinemaFromFavoritesAsync(id.Value, User.Identity.Name);
            return RedirectToAction("Cinemas", new { all = false });
        }
        public async Task<IActionResult> Tickets(int? pageNumber)
        {
            var tickets = await _customersService.GetTicketsForCustomerAsync(User.Identity.Name);
            return View("Tickets", await PaginatedList<CustomerTicketViewModel>.CreateAsync(tickets, pageNumber ?? 1, 5));
        }
        public async Task<IActionResult> Cinema([ModelBinder(typeof(IdModelBinder))] int cinemaId)
        {
            if (!await _ownersService.ExistsByIdAsync(cinemaId))
            {
                return NotFound();
            }
            return View("Cinema", await _customersService.PrepareCinemaViewModelAsync(User.Identity.Name, cinemaId));
        }
        public async Task<IActionResult> BuyTicket(int cinemaId, int movieId, [ModelBinder(typeof(DateModelBinder))] DateTime forDate)
        {
            if (!await _ownersService.ExistsByIdAsync(cinemaId))
            {
                return NotFound();
            }
            if (!await _moviesService.ExistsByIdAsync(movieId))
            {
                return NotFound();
            }
            return View("BuyTicket", await _customersService.GetBuyTicketViewModelAsync(cinemaId, movieId, forDate));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BuyTicket(int sectorId, int movieId, SectorDetailsViewModel viewModel, DateTime forDate)
        {
            if (!await _sectorsService.ExistsByIdAsync(sectorId))
            {
                return NotFound();
            }
            if (!await _moviesService.ExistsByIdAsync(movieId))
            {
                return NotFound();
            }
            await _customersService.BuyTicketAsync(sectorId, movieId, viewModel, forDate, User.Identity.Name);
            return RedirectToAction("Cinema", "Customer", new { userEmail = User.Identity.Name, cinemaId = TempData["CinemaId"] });
        }
        [HttpGet]
        public async Task<IActionResult> GetMoviesByDate([ModelBinder(typeof(IdModelBinder))] int cinemaId, [ModelBinder(typeof(DateModelBinder))] DateTime date)
        {
            if (!await _ownersService.ExistsByIdAsync(cinemaId))
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
                return RedirectToAction("MovieDetails", "Customer", new { id = movieId });
            }

            await _customersService.SetRatingToMovieAsync(movieId.Value, rating.Value, User.Identity.Name);

            return RedirectToAction("MovieDetails", "Customer", new { id = movieId });
        }
    }
}
