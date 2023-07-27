using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cinema.Core.Utilities;
using Microsoft.AspNetCore.Authorization;
using Cinema.Core.Contracts;
using Cinema.ViewModels.Actors;
using Cinema.Core.Services;
using Cinema.Extensions.ModelBinders;

namespace Cinema.Controllers
{
    [Authorize(Roles = "Owner")]
    public class ActorsController : Controller
    {
        private readonly IActorsService _actorsService;
        public ActorsController(IActorsService actorsService)
        {
            _actorsService = actorsService;
        }

        public async Task<IActionResult> AllActors()
        {
            return View();
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (!await _actorsService.ExistsByIdAsync(id))
            {
                return NotFound();
            }
            var actor = await _actorsService.GetByIdAsync(id);
            if (actor == null)
            {
                return NotFound();
            }

            return View(actor);
        }

        // GET: Actors/Create
        public async Task<IActionResult> AddActor()
        {
            return View(await _actorsService.PrepareForAddingAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddActor(CreateActorViewModel actorVM)
        {
            if (ModelState.IsValid)
            {
                await _actorsService.AddActorAsync(actorVM);
            }
            actorVM.Countries = (await _actorsService.PrepareForAddingAsync()).Countries;
            return View(nameof(AddActor), actorVM);
        }
        public async Task<IActionResult> SearchAndFilterActors(string searchText, string filterValue, string sortBy)
        {
            var actors = await _actorsService.SearchAndFilterActorsAsync(searchText, filterValue, sortBy);
            return PartialView("_ActorsPartial", actors);
        }
        public async Task<IActionResult> SearchMoviesByActor(string searchText, [ModelBinder(typeof(IdModelBinder))] int id)
        {
            if (!await _actorsService.ExistsByIdAsync(id))
            {
                return NotFound();
            }
            var movies = await _actorsService.SearchMoviesByActor(searchText, id);
            return PartialView("_ActorMoviesPartial", movies);
        }
        [HttpGet]
        public async Task<IActionResult> EditActor([ModelBinder(typeof(IdModelBinder))] int id)
        {
            if (!await _actorsService.ExistsByIdAsync(id))
            {
                return NotFound();
            }
            return PartialView("_EditActorPartial", await _actorsService.GetEditViewModelByIdAsync(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditActor([FromForm] EditActorViewModel viewModel)
        {
            if (viewModel.Id <= 0)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                await _actorsService.EditActorAsync(viewModel);
            }
            return RedirectToAction("AllActors", "Actors");
        }
        [HttpGet]
        public async Task<IActionResult> DeleteActor([ModelBinder(typeof(IdModelBinder))] int id)
        {
            if (!await _actorsService.ExistsByIdAsync(id))
            {
                return NotFound();
            }
            return PartialView("_DeleteActorPartial", await _actorsService.PrepareDeleteViewModelAsync(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteActor([FromForm] DeleteActorViewModel viewModel, [ModelBinder(typeof(IdModelBinder))] int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }
            if (!await _actorsService.ExistsByIdAsync(id))
            {
                return NotFound();
            }
            var actor = await _actorsService.GetByIdAsync(id);
            if (actor == null)
            {
                return NotFound();
            }
            await _actorsService.DeleteByIdAsync(actor.Id);
            return RedirectToAction("AllActors", "Actors");
        }
    }
}
