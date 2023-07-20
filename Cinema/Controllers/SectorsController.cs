using Cinema.Core.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Cinema.Controllers
{
    public class SectorsController : Controller
    {
        private readonly ISectorsService _sectorsService;

        public SectorsController(ISectorsService sectorsService)
        {
            _sectorsService = sectorsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCinemaSectors(string id, string movieId)
        {
             return PartialView("_CinemaSectorsGridPartial", await _sectorsService.GetCinemaSectorsGridAsync(id, movieId, (DateTime)TempData["ForDateTime"]));
        }
        [HttpGet]
        public async Task<IActionResult> GetSectorLayout(string id, string movieId, DateTime forDateTime)
        {
            return PartialView("_SectorLayoutPartial", await _sectorsService.GetSectorByIdAsync(id, movieId, forDateTime));
        }
    }
}
