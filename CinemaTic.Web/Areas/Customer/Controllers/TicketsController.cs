using CinemaTic.Core.Contracts;
using CinemaTic.Extensions.ModelBinders;
using CinemaTic.ViewModels.Sectors;
using CinemaTic.Web.Areas.Customer.Controllers.BaseControllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CinemaTic.Web.Areas.Customer.Controllers
{
    public class TicketsController : CustomerController
    {
        private readonly ICustomersService _customersService;
        private readonly ICinemasService _cinemasService;
        private readonly IMoviesService _moviesService;
        private readonly ISectorsService _sectorsService;

        public TicketsController(ICustomersService customersService, ICinemasService cinemasService, IMoviesService moviesService, ISectorsService sectorsService)
        {
            _customersService = customersService;
            _cinemasService = cinemasService;
            _moviesService = moviesService;
            _sectorsService = sectorsService;
        }

        public async Task<IActionResult> QueryTickets(string searchText, int? pageNumber)
        {
            var tickets = await _customersService.QueryTicketsAsync(User.Identity.Name, searchText, pageNumber);
            return PartialView("_TicketsPartial", tickets);
        }

        public async Task<IActionResult> BuyTicket(int cinemaId, int movieId, [ModelBinder(typeof(DateModelBinder))] DateTime forDate)
        {
            if (!await _cinemasService.ExistsByIdAsync(cinemaId))
            {
                return NotFound();
            }
            if (!await _moviesService.ExistsByIdAsync(movieId))
            {
                return NotFound();
            }
            if (!await _customersService.CustomerHasCinemaAsync(cinemaId, User.Identity.Name))
            {
                TempData["ErrorMessage"] = "You must add this cinema to favorites in order to buy tickets!";
                return Forbid();
            }
            return View(await _customersService.GetBuyTicketViewModelAsync(cinemaId, movieId, forDate));
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
            await _customersService.BuyTicketAsync(sectorId, movieId, User.Identity.Name, viewModel, forDate);
            return Redirect(Url.RouteUrl(new { controller = "Dashboard", action = "Index"}) + "#tickets");
        }
    }
}
