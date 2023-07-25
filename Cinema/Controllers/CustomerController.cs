using Cinema.Core.Contracts;
using Cinema.Core.Utilities;
using Cinema.Data.Models;
using Cinema.ViewModels.Customers;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomersService _customersService;
        private readonly ISectorsService _sectorsService;
        private readonly IMoviesService _moviesService;

        public CustomerController(ICustomersService customersService, ISectorsService sectorsService, IMoviesService moviesService)
        {
            _customersService = customersService;
            _sectorsService = sectorsService;
            _moviesService = moviesService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _customersService.GetCinemasForUserAsync(User.Identity.Name));
        }
        [HttpGet]
        public async Task<IActionResult> GetTicketPurchaseView(string cinemaId)
        {
            return PartialView("_CinemaSectorsGridPartial", await _sectorsService.GetCinemaSectorsGridAsync(cinemaId, null, new System.DateTime()));
        }
        public async Task<IActionResult> MovieDetails(int id)
        {
            return View("MovieDetails", await _moviesService.GetDetailsViewModel(await _moviesService.GetByIdAsync(id), null, User.Identity.Name));
        }
        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            return View("ChangePassword", await _customersService.GetChangePasswordViewModelAsync(User.Identity.Name));
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("ChangePassword", await _customersService.GetChangePasswordViewModelAsync(User.Identity.Name));
            }
            await _customersService.ChangePasswordAsync(viewModel);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Cinemas(bool? all, int? pageNumber)
        {
            TempData["All"] = all;
            var cinemas = await _customersService.GetCinemasAsync(all, User.Identity.Name);
            return View("Cinemas", await PaginatedList<CinemasViewModel>.CreateAsync(cinemas, pageNumber ?? 1, 5));
        }
        public async Task<IActionResult> AddCinemaToFavorites(int id)
        {
            await _customersService.AddCinemaToFavoritesAsync(id, User.Identity.Name);
            return RedirectToAction("Cinemas", new { all = false });
        }
        public async Task<IActionResult> RemoveCinemaFromFavorites(int id)
        {
            await _customersService.RemoveCinemaFromFavoritesAsync(id, User.Identity.Name);
            return RedirectToAction("Cinemas", new { all = false });
        }
        public async Task<IActionResult> Tickets(int? pageNumber)
        {
            var tickets = await _customersService.GetTicketsForCustomerAsync(User.Identity.Name);
            return View("Tickets", await PaginatedList<CustomerTicketViewModel>.CreateAsync(tickets, pageNumber ?? 1, 5));
        }

    }
}
