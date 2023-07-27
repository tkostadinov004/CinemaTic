using Cinema.Core.Contracts;
using Cinema.Core.Services;
using Cinema.Extensions.ModelBinders;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Cinema.Controllers
{
    public class SectorsController : Controller
    {
        private readonly ISectorsService _sectorsService;
        private readonly IOwnersService _ownersService;
        private readonly IMoviesService _moviesService;

        public SectorsController(ISectorsService sectorsService, IOwnersService ownersService, IMoviesService moviesService)
        {
            _sectorsService = sectorsService;
            _ownersService = ownersService;
            _moviesService = moviesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCinemaSectors([ModelBinder(typeof(IdModelBinder))] int id, [ModelBinder(typeof(IdModelBinder))] int movieId)
        {
            if (!await _ownersService.ExistsByIdAsync(id))
            {
                return NotFound();
            }
            if (!await _moviesService.ExistsByIdAsync(movieId))
            {
                return NotFound();
            }
            return PartialView("_CinemaSectorsGridPartial", await _sectorsService.GetCinemaSectorsGridAsync(id, movieId, (DateTime)TempData["ForDateTime"]));
        }
        [HttpGet]
        public async Task<IActionResult> GetSectorLayout([ModelBinder(typeof(IdModelBinder))] int id, [ModelBinder(typeof(IdModelBinder))] int movieId, DateTime forDateTime)
        {
            if (!await _ownersService.ExistsByIdAsync(id))
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
