using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cinema.Data;
using Cinema.Models;
using Cinema.Models.ViewModels;
using Cinema.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;

namespace Cinema.Controllers
{
    [Authorize(Roles = "Owner")]
    public class ActorsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ActorsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Actors
        public async Task<IActionResult> Index()
        {
            return View(await _context.Actors.Include(i => i.Movies).ThenInclude(m => m.Movie).ToListAsync());
        }

        // GET: Actors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actor = await _context.Actors
                .Include(i => i.Movies).ThenInclude(m => m.Movie)
                .Include(i => i.Movies).ThenInclude(m => m.Actor)
                .FirstOrDefaultAsync(m => m.Id == id);
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
            actorVM.Nationality = country;
            if (ModelState.IsValid)
            {
                string photoName = GlobalMethods.UploadPhoto("Actors", actorVM.Image, _webHostEnvironment);
                Actor actor = new Actor
                {
                    Birthdate = actorVM.Birthdate,
                    FirstName = actorVM.FirstName,
                    LastName = actorVM.LastName,
                    ImageUrl = photoName,
                    Nationality = actorVM.Nationality,
                    BulgarianFullName = actorVM.BulgarianFullName,
                    Rating = actorVM.Rating
                };

                _context.Add(actor);
                await _context.SaveChangesAsync();      
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

            var actor = await _context.Actors.FindAsync(id);
            if (actor == null)
            {
                return NotFound();
            }
            var vm = new EditActorViewModel
            {
                Id = actor.Id,
                FirstName = actor.FirstName,
                LastName = actor.LastName,
                BulgarianFullName = actor.BulgarianFullName,
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
                    var actor = await _context.Actors.FirstOrDefaultAsync(i => i.Id == id);
                    string photoName = GlobalMethods.UploadPhoto("Actors", actorVM.Image, _webHostEnvironment);
                    actor.FirstName = actorVM.FirstName;
                    actor.LastName = actorVM.LastName;
                    actor.BulgarianFullName = actorVM.BulgarianFullName;
                    actor.Birthdate = actorVM.Birthdate;
                    actor.Rating = actorVM.Rating;

                    if (country != null)
                    {
                        actor.Nationality = country;
                    }
                    if (actorVM.Image != null)
                    {
                        actor.ImageUrl = photoName;
                    }
                    _context.Update(actor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActorExists(actorVM.Id))
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

            var actor = await _context.Actors
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var actor = await _context.Actors.FindAsync(id);
            _context.Actors.Remove(actor);
            await GlobalMethods.DeleteImage("Actors", actor.ImageUrl, _context, _webHostEnvironment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActorExists(int id)
        {
            return _context.Actors.Any(e => e.Id == id);
        }
    }
}
