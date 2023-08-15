using CinemaTic.Core.Contracts;
using CinemaTic.Web.Areas.Customer.Controllers.BaseControllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CinemaTic.Web.Areas.Customer.Controllers
{
    public class DashboardController : CustomerController
    {
        private readonly ICustomersService _customersService;

        public DashboardController(ICustomersService customersService)
        {
            _customersService = customersService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _customersService.GetHomePageViewModelAsync(User.Identity.Name));
        }
    }
}
