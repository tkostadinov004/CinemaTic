using CinemaTic.Core.Contracts;
using CinemaTic.Web.Areas.Admin.Controllers.BaseControllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CinemaTic.Web.Areas.Admin.Controllers
{
    public class ChartsController : AdminController
    {
        private readonly IChartsService _chartsService;

        public ChartsController(IChartsService chartsService)
        {
            _chartsService = chartsService;
        }
        [HttpGet]
        public async Task<IActionResult> GetRegisteredUsersByMonth()
        {
            return Json(await _chartsService.GetRegisteredUsersByMonthAsync());
        }
        [HttpGet]
        public async Task<IActionResult> GetUsersGrowth()
        {
            return Json(await _chartsService.GetUsersGrowthAsync());
        }
    }
}
