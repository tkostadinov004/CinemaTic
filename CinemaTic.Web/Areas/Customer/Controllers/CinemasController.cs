using CinemaTic.Core.Contracts;
using CinemaTic.Extensions;
using CinemaTic.Extensions.ModelBinders;
using CinemaTic.ViewModels.Customers;
using CinemaTic.Web.Areas.Customer.Controllers.BaseControllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CinemaTic.Web.Areas.Customer.Controllers
{
    public class CinemasController : CustomerController
    {
        private readonly ICustomersService _customersService;
        private readonly ICinemasService _cinemasService;

        public CinemasController(ICustomersService customersService, ICinemasService cinemasService)
        {
            _customersService = customersService;
            _cinemasService = cinemasService;
        }

        public async Task<IActionResult> QueryCinemas(bool? all, string searchText, int? pageNumber)
        {
            var cinemas = await _customersService.QueryCinemasAsync(all,searchText, pageNumber, User.Identity.Name);
            return PartialView("_CinemasPartial", cinemas);
        }
        public async Task<IActionResult> Cinema([ModelBinder(typeof(IntegerModelBinder))] int id)
        {
            if (!await _cinemasService.ExistsByIdAsync(id))
            {
                return NotFound();
            }
            return View(await _customersService.PrepareCinemaViewModelAsync(User.Identity.Name, id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCinemaToFavorites(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (!await _cinemasService.ExistsByIdAsync(id))
            {
                return NotFound();
            }
            await _customersService.AddCinemaToFavoritesAsync(id, User.Identity.Name);
            return Ok();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveCinemaFromFavorites(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (!await _cinemasService.ExistsByIdAsync(id))
            {
                return NotFound();
            }
            await _customersService.RemoveCinemaFromFavoritesAsync(id, User.Identity.Name);
            return Ok();
        }
    }
}
