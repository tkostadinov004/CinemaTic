using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cinema.Core.Utilities;
using Microsoft.AspNetCore.Authorization;
using Cinema.Core.Contracts;
using Cinema.ViewModels.Actors;
using Cinema.Core.Services;

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
        public async Task<IActionResult> SearchMoviesByActor(string searchText, string id)
        {
            var movies = await _actorsService.SearchMoviesByActor(searchText, id);
            return PartialView("_ActorMoviesPartial", movies);
        }
        [HttpGet]
        public async Task<IActionResult> EditActor(string id)
        {
            return PartialView("_EditActorPartial", await _actorsService.GetEditViewModelByIdAsync(int.Parse(id)));
        }
        [HttpPost]
        public async Task<IActionResult> EditActor([FromForm] EditActorViewModel viewModel)
        {
            await _actorsService.EditActorAsync(viewModel);
            return RedirectToAction("AllActors", "Actors");
        }
        [HttpGet]
        public async Task<IActionResult> DeleteActor(int id)
        {
            return PartialView("_DeleteActorPartial", await _actorsService.PrepareDeleteViewModelAsync(id));
        }
        [HttpPost]
        public async Task<IActionResult> DeleteActor([FromForm] DeleteActorViewModel viewModel)
        {
            if(viewModel.Id == null)
            {
                return NotFound();
            }

            await _actorsService.DeleteByIdAsync(viewModel.Id);
            return RedirectToAction("AllActors", "Actors");
        }
    }
}
