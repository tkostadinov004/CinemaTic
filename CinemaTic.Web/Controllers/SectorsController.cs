using CinemaTic.Core.Contracts;
using CinemaTic.Core.Services;
using CinemaTic.Extensions.ModelBinders;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CinemaTic.Web.Controllers
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
        public async Task<IActionResult> GetCinemaSectors([ModelBinder(typeof(IdModelBinder))] int id, [ModelBinder(typeof(IdModelBinder))] int movieId, [ModelBinder(typeof(DateModelBinder))] DateTime forDate)
        {
            if (!await _ownersService.ExistsByIdAsync(id))
            {
                return NotFound();
            }
            if (!await _moviesService.ExistsByIdAsync(movieId))
            {
                return NotFound();
            }
            return PartialView("_CinemaSectorsGridPartial", await _sectorsService.GetCinemaSectorsGridAsync(id, movieId, forDate));
        }
        [HttpGet]
        public async Task<IActionResult> GetSectorLayout([ModelBinder(typeof(IdModelBinder))] int id, [ModelBinder(typeof(IdModelBinder))] int movieId, DateTime forDateTime)
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
