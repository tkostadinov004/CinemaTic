using CinemaTic.Core.Contracts;
using CinemaTic.Web.Areas.Customer.Controllers.BaseControllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CinemaTic.Web.Areas.Customer.Controllers
{
    public class ChartsController : CustomerController
    {
        private readonly IChartsService _chartsService;

        public ChartsController(IChartsService chartsService)
        {
            _chartsService = chartsService;
        }
        [HttpGet]
        public async Task<IActionResult> GetTicketsBought()
        {
            return Json(await _chartsService.GetTicketsBoughtByCustomerAsync(User.Identity.Name));
        }
    }
}
