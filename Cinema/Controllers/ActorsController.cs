using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cinema.Utilities;
using Microsoft.AspNetCore.Authorization;
using Cinema.Core.Contracts;
using Cinema.ViewModels.Actors;

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

        // GET: Actors
        public async Task<IActionResult> Index()
        {
            return View(await _actorsService.GetAllAsync());
        }

        // GET: Actors/Details/5
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
        public IActionResult Create()
        {
            ViewBag.Countries = new SelectList(GlobalMethods.GetCountries());
            return View();
        }

        // POST: Actors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateActorViewModel actorVM, string country)
        { 
            if (ModelState.IsValid)
            {
                await _actorsService.CreateAsync(actorVM, country);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Actors/Edit/5
        public async Task<IActionResult> Edit(int? id)
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
            var vm = new EditActorViewModel
            {
                Id = actor.Id,
                FirstName = actor.FirstName,
                LastName = actor.LastName,
                Nationality = actor.Nationality,
                Birthdate = actor.Birthdate,
                Rating = actor.Rating,
                ImageUrl = actor.ImageUrl
            };
            ViewBag.Countries = new SelectList(GlobalMethods.GetCountries());
            return View(vm);
        }

        // POST: Actors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditActorViewModel actorVM,int id, string? country)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _actorsService.EditByIdAsync(actorVM, id, country);
                }
                catch (DbUpdateConcurrencyException)
                {
                    bool actorExists = await _actorsService.ExistsByIdAsync(actorVM.Id);
                    if (!actorExists)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(actorVM);
        }

        // GET: Actors/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Actors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _actorsService.DeleteByIdAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
