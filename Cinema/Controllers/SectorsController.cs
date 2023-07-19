using Cinema.Core.Contracts;
using Microsoft.AspNetCore.Mvc;
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
             return PartialView("_CinemaSectorsGridPartial", await _sectorsService.GetCinemaSectorsGridAsync(id, movieId));
        }
        [HttpGet]
        public async Task<IActionResult> GetSectorLayout(string id, string movieId)
        {
            return PartialView("_SectorLayoutPartial", await _sectorsService.GetSectorByIdAsync(id, movieId));
        }
    }
}
