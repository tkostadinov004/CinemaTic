using Cinema.Core.Contracts;
using Microsoft.AspNetCore.Mvc;
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
    }
}
