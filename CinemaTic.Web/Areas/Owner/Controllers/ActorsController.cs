using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CinemaTic.Core.Contracts;
using CinemaTic.ViewModels.Actors;
using CinemaTic.Extensions.ModelBinders;
using CinemaTic.Web.Areas.Owner.Controllers.BaseControllers;

namespace CinemaTic.Web.Areas.Owner.Controllers
{
    public class ActorsController : OwnerController
    {
        private readonly IActorsService _actorsService;
        public ActorsController(IActorsService actorsService)
        {
            _actorsService = actorsService;
        }

        public async Task<IActionResult> Index()
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
            var actor = await _actorsService.GetDetailsViewModelByIdAsync(id);
            if (actor == null)
            {
                return NotFound();
            }

            return View(actor);
        }

        // GET: Actors/Create
        public async Task<IActionResult> Add()
        {
            return View(await _actorsService.GetCreateViewModelAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(CreateActorViewModel actorVM)
        {
            if (ModelState.IsValid)
            {
                await _actorsService.CreateActorAsync(actorVM);
                return RedirectToAction(nameof(Index));
            }
            actorVM.Countries = (await _actorsService.GetCreateViewModelAsync()).Countries;
            return View(actorVM);
        }
        public async Task<IActionResult> QueryActors(string searchText, string filterValue, string sortBy, [ModelBinder(typeof(IdModelBinder))] int? pageNumber)
        {
            var actors = await _actorsService.QueryActorsAsync(searchText, filterValue, sortBy, pageNumber ?? 1);
            return PartialView("_ActorsPartial", actors);
        }
        public async Task<IActionResult> QueryMoviesByActor([ModelBinder(typeof(IdModelBinder))] int id, string searchText, string sortBy)
        {
            if (!await _actorsService.ExistsByIdAsync(id))
            {
                return NotFound();
            }
            var movies = await _actorsService.QueryMoviesByActorAsync(id, searchText, sortBy);
            return PartialView("_ActorMoviesPartial", movies);
        }
        [HttpGet]
        public async Task<IActionResult> Edit([ModelBinder(typeof(IdModelBinder))] int id)
        {
            if (!await _actorsService.ExistsByIdAsync(id))
            {
                return NotFound();
            }
            return PartialView("_EditActorPartial", await _actorsService.GetEditViewModelByIdAsync(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm] EditActorViewModel viewModel)
        {
            if (viewModel.Id <= 0)
            {
                return NotFound();
            }
            if (!await _actorsService.ExistsByIdAsync(viewModel.Id))
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                await _actorsService.EditActorAsync(viewModel);
                return RedirectToAction("Details", "Actors", new { id = viewModel.Id });
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Delete([ModelBinder(typeof(IdModelBinder))] int id)
        {
            if (!await _actorsService.ExistsByIdAsync(id))
            {
                return NotFound();
            }
            return PartialView("_DeleteActorPartial", await _actorsService.GetDeleteViewModelByIdAsync(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromForm] DeleteActorViewModel viewModel, [ModelBinder(typeof(IdModelBinder))] int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }
            if (!await _actorsService.ExistsByIdAsync(id))
            {
                return NotFound();
            }
            await _actorsService.DeleteByIdAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
