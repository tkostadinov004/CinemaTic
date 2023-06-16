using Cinema.Core.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Cinema.Controllers
{
    [Authorize(Roles = "Owner")]
    public class VisitorsController : Controller
    {
        private readonly IVisitorsService _visitorsService;

        public VisitorsController(IVisitorsService visitorsService)
        {
            _visitorsService = visitorsService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _visitorsService.GetAllAsync());
        }
    }
}
