using CinemaTic.Core.Contracts;
using CinemaTic.Web.Areas.Owner.Controllers.BaseControllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CinemaTic.Web.Areas.Owner.Controllers
{
    public class ChartsController : OwnerController
    {
        private readonly IChartsService _chartsService;

        public ChartsController(IChartsService chartsService)
        {
            _chartsService = chartsService;
        }
        [HttpGet]
        public async Task<IActionResult> GetMarketShare()
        {
            return Json(await _chartsService.GetMarketShareByUserAsync(User.Identity.Name));
        }
        [HttpGet]
        public async Task<IActionResult> GetTotalIncomes()
        {
            return Json(await _chartsService.GetTotalIncomesAsync(User.Identity.Name));
        }
        [HttpGet]
        public async Task<IActionResult> GetCustomersPerCinema()
        {
            return Json(await _chartsService.GetCustomersPerCinemaAsync(User.Identity.Name));
        }
        [HttpGet]
        public async Task<IActionResult> GetBestSellingMoviesPerCinema()
        {
            return Json(await _chartsService.GetBestSellingMoviesPerCinemaAsync(User.Identity.Name));
        }
    }
}
