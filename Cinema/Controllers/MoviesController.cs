using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cinema.Data;
using Cinema.Models;
using Microsoft.AspNetCore.Authorization;
using Cinema.Models.Enums;
using Microsoft.AspNetCore.Identity;
using System.Globalization;
namespace Cinema.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public MoviesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Movies
        [AllowAnonymous]
        public async Task<IActionResult> Index(DateTime? date)
        {
            var movies = await _context.Movies.Include(i => i.Genre).Include(i => i.Actors).ThenInclude(a => a.Actor).Where(i => i.Date >= DateTime.Today).ToListAsync();
            if (date != null)
            {
                return View(movies.Where(i => Math.Abs((i.Date - date.Value).TotalDays) <= 7));
            }
            return View(movies);
        }

        // GET: Movies/Details/5
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        [Authorize(Roles = "Owner")]
        public IActionResult Create()
        {
            ViewBag.Genres = new SelectList(_context.Genres.AsNoTracking().ToList(), nameof(Genre.Id), nameof(Genre.BulgarianName));
            ViewBag.Actors = new SelectList(_context.Actors.AsNoTracking().ToList(), nameof(Actor.Id), nameof(Actor.FirstName));
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> Create([Bind("Id,Title,Genre,GenreId,ImageUrl,Description,UserRating,Date,Price,Actors")] Movie movie, IEnumerable<string> acts)
        {
            if (ModelState.IsValid)
            {
                var genre = _context.Genres.Include(i => i.Movies).FirstOrDefault(i => i.Id == movie.GenreId);
                movie.UserRating = 0;
                movie.Genre = genre;
                movie.Genre.Movies.Add(movie);

                foreach (string id in acts)
                {
                    movie.Actors.Add(new ActorMovie
                    {
                        ActorId = int.Parse(id),
                        MovieId = movie.Id
                    });
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Edit/5
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Genre,ImageUrl,Description,Date,Price")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var originalMovie = await _context.Movies.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
                    movie.UserRating = originalMovie.UserRating;

                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
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
            return View(movie);
        }

        // GET: Movies/Delete/5
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize(Roles = "Visitor")]
        public async Task<IActionResult> SetRating(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }
        [HttpPost]
        [Authorize(Roles = "Visitor")]
        public async Task<IActionResult> SetRating(int id, decimal rating)
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            var movie = await _context.Movies.FindAsync(id);
            var usersRatedMovie = _context.UsersMovies.Include(i => i.Movie).Include(i => i.User).ToList();

            decimal userRating = rating;

            var userMovie = usersRatedMovie.FirstOrDefault(i => i.User.Email == user.Email && i.Movie.Id == movie.Id);

            if (userMovie == null)
            {
                movie.UserRating = ((movie.UserRating * movie.RatingCount) + userRating) / (movie.RatingCount + 1);
                movie.RatingCount++;

                _context.UsersMovies.Add(new UserMovie
                {
                    MovieId = movie.Id,
                    UserId = user.Id,
                    Rating = userRating
                });
            }
            else
            {
                userRating = usersRatedMovie.FirstOrDefault(i => i.User.Email == user.Email && i.Movie.Id == movie.Id).Rating - rating;

                userMovie.Rating = rating;
                movie.UserRating = ((movie.UserRating * movie.RatingCount) - userRating) / movie.RatingCount;
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}
