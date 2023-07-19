using Cinema.Core.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Cinema.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomersService _customersService;
        private readonly ISectorsService _sectorsService;

        public CustomerController(ICustomersService customersService, ISectorsService sectorsService)
        {
            _customersService = customersService;
            _sectorsService = sectorsService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _customersService.GetCinemasForUserAsync(User.Identity.Name));
        }
        [HttpGet]
        public async Task<IActionResult> GetTicketPurchaseView(string cinemaId)
        {
            return PartialView("_CinemaSectorsGridPartial", await _sectorsService.GetCinemaSectorsGridAsync(cinemaId, null));
        }
    }
}
