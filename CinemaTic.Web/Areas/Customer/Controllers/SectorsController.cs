using CinemaTic.Core.Contracts;
using CinemaTic.Extensions.ModelBinders;
using CinemaTic.Web.Areas.Customer.Controllers.BaseControllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CinemaTic.Web.Areas.Customer.Controllers
{
    public class SectorsController : CustomerController
    {
        private readonly ISectorsService _sectorsService;
        private readonly ICinemasService _cinemasService;
        private readonly IMoviesService _moviesService;

        public SectorsController(ISectorsService sectorsService, ICinemasService ownersService, IMoviesService moviesService)
        {
            _sectorsService = sectorsService;
            _cinemasService = ownersService;
            _moviesService = moviesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCinemaSectors([ModelBinder(typeof(IntegerModelBinder))] int cinemaId, [ModelBinder(typeof(IntegerModelBinder))] int movieId, [ModelBinder(typeof(DateModelBinder))] DateTime forDate)
        {
            if (!await _cinemasService.ExistsByIdAsync(cinemaId))
            {
                return NotFound();
            }
            if (!await _moviesService.ExistsByIdAsync(movieId))
            {
                return NotFound();
            }
            return PartialView("_CinemaSectorsGridPartial", await _sectorsService.GetCinemaSectorsGridAsync(cinemaId, movieId, forDate));
        }
        [HttpGet]
        public async Task<IActionResult> GetSectorLayout([ModelBinder(typeof(IntegerModelBinder))] int id, [ModelBinder(typeof(IntegerModelBinder))] int movieId, DateTime forDateTime)
        {
            if (!await _sectorsService.ExistsByIdAsync(id))
            {
                return NotFound();
            }
            if (!await _moviesService.ExistsByIdAsync(movieId))
            {
                return NotFound();
            }
            return PartialView("_SectorLayoutPartial", await _sectorsService.GetSectorByIdAsync(id, movieId, forDateTime));
        }
    }
}
