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
using Microsoft.AspNetCore.Identity;
using System.Globalization;
using Cinema.Models.ViewModels;
using Cinema.Utilities;
using Microsoft.AspNetCore.Hosting;

namespace Cinema.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public MoviesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Movies
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> Index(DateTime? date)
        {
            var movies = _context.Movies.Include(i => i.Genre).Include(i => i.Actors).ThenInclude(a => a.Actor).ToListAsync().Result.ToList();
            return View(movies);
        }

        // GET: Movies/Details/5
        //[Authorize(Roles = "Owner")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .Include(i => i.Actors)
                .ThenInclude(i => i.Actor)
                .Include(i => i.Genre)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            var ratings = await _context.UsersMovies.Include(i => i.Movie).Include(i => i.User).Where(i => i.Movie.Id == movie.Id).ToListAsync();
            var vm = new MovieDetailsViewModel
            {
                Movie = movie,
                MovieId = movie.Id,
                AverageRating = ratings.Count == 0 ? 0 : ratings.Select(i => i.Rating).Average(),
                RatingCount = ratings.Count,
                CurrentUserRating = ratings.FirstOrDefault(i => i.User.Email == User.Identity.Name) == null ? null : ratings.FirstOrDefault(i => i.User.Email == User.Identity.Name).Rating
            };
            return View(vm);
        }

        // GET: Movies/Create
        [Authorize(Roles = "Owner")]
        public IActionResult Create()
        {
            ViewBag.Genres = new SelectList(_context.Genres.AsNoTracking().ToList(), nameof(Genre.Id), nameof(Genre.BulgarianName));

            var actors = _context.Actors.AsNoTracking().ToList();
            ViewBag.Actors = new SelectList(from a in actors select new { Id = a.Id, FullName = a.BulgarianFullName }, nameof(Actor.Id), "FullName");
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> Create(CreateMovieViewModel movieVM, IEnumerable<string> acts, int genreId)
        {
            if (ModelState.IsValid)
            {
                string photoName = GlobalMethods.UploadPhoto("Movies", movieVM.Image, _webHostEnvironment);
                Movie movie = new Movie
                {
                    Date = movieVM.Date,
                    Description = movieVM.Description,
                    RunningTime = movieVM.RunningTime,
                    BulgarianTitle = movieVM.BulgarianTitle,
                    EnglishTitle = movieVM.EnglishTitle,
                    ImageUrl = photoName,
                    Price = movieVM.Price,
                    TrailerUrl = movieVM.TrailerUrl
                };

                var genre = _context.Genres.Include(i => i.Movies).FirstOrDefault(i => i.Id == genreId);
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
                _context.Movies.Add(movie);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Movies/Edit/5
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies.Include(i => i.Actors).ThenInclude(i => i.Actor).FirstOrDefaultAsync(i => i.Id == id);
            if (movie == null)
            {
                return NotFound();
            }
            var vm = new EditMovieViewModel
            {
                Id = movie.Id,
                BulgarianTitle = movie.BulgarianTitle,
                EnglishTitle = movie.EnglishTitle,
                Genre = movie.Genre,
                GenreId = movie.GenreId,
                Description = movie.Description,
                RunningTime = movie.RunningTime,
                TrailerUrl = movie.TrailerUrl,
                Date = movie.Date,
                Price = movie.Price,
                Actors = movie.Actors,
                ImageUrl = movie.ImageUrl
            };
            ViewBag.Genres = new SelectList(_context.Genres.AsNoTracking().ToList(), nameof(Genre.Id), nameof(Genre.BulgarianName));

            var actors = _context.Actors.AsNoTracking().ToList();
            ViewBag.Actors = new SelectList(from a in actors select new { Id = a.Id, FullName = $"{a.FirstName} {a.LastName}" }, nameof(Actor.Id), "FullName");
            return View(vm);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> Edit(EditMovieViewModel vm, int id, int genreId, IEnumerable<string> acts)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var movie = await _context.Movies.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
                    string photoName = GlobalMethods.UploadPhoto("Movies", vm.Image, _webHostEnvironment);

                    movie.BulgarianTitle = vm.BulgarianTitle;
                    movie.EnglishTitle = vm.EnglishTitle;
                    movie.Genre = await _context.Genres.FirstOrDefaultAsync(i => i.Id == genreId);
                    movie.Description = vm.Description;
                    movie.RunningTime = vm.RunningTime;
                    movie.TrailerUrl = vm.TrailerUrl;
                    movie.Date = vm.Date;
                    movie.Price = vm.Price;

                    if (vm.Image != null)
                    {
                        movie.ImageUrl = photoName;
                    }
                    foreach (string actorId in acts)
                    {
                        if (!movie.Actors.Contains(_context.ActorsMovies.FirstOrDefaultAsync(i => i.ActorId == int.Parse(actorId)).Result))
                        {
                            movie.Actors.Add(new ActorMovie
                            {
                                ActorId = int.Parse(actorId),
                                MovieId = movie.Id
                            });
                        }
                    }
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(vm.Id))
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
            return View(vm);
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
                .Include(i => i.Genre)
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
            await GlobalMethods.DeleteImage("Movies", movie.ImageUrl, _context, _webHostEnvironment);
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

            var currentUser = _context.UsersMovies.FirstOrDefault(i => i.MovieId == movie.Id && i.UserId == GetCurrentUserAsync().Result.Id);

            var vm = new SetRatingViewModel
            {
                MovieId = movie.Id,
                PersonalUserRating = currentUser == null ? 0 : currentUser.Rating
            };
            return View(vm);
        }
        [HttpPost]
        [Authorize(Roles = "Visitor")]
        public async Task<IActionResult> SetRating(int movieId, decimal userRating)
        {
            if (userRating == 0)
            {
                return RedirectToAction("Details", new { id = movieId });
            }

            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            var movie = _context.Movies.Include(i => i.Genre).ToListAsync().Result.FirstOrDefault(i => i.Id == movieId);
            var usersRatedMovie = _context.UsersMovies.Include(i => i.Movie).Include(i => i.User).ToList();

            decimal currentRating = userRating;

            var userMovie = usersRatedMovie.FirstOrDefault(i => i.User.Email == user.Email && i.Movie.Id == movie.Id);

            if (userMovie == null)
            {
                movie.UserRating = ((movie.UserRating * movie.RatingCount) + currentRating) / (movie.RatingCount + 1);
                movie.RatingCount++;

                _context.UsersMovies.Add(new UserMovie
                {
                    MovieId = movie.Id,
                    UserId = user.Id,
                    Rating = currentRating
                });
            }
            else
            {
                currentRating = usersRatedMovie.FirstOrDefault(i => i.User.Email == user.Email && i.Movie.Id == movie.Id).Rating - userRating;

                userMovie.Rating = userRating;
                movie.UserRating = ((movie.UserRating * movie.RatingCount) - currentRating) / movie.RatingCount;
            }
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", new { id = movie.Id });
        }
        [AllowAnonymous]
        public async Task<IActionResult> WatchTrailer(int id)
        {
            return View(await _context.Movies.FindAsync(id));
        }
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> Statistics()
        {
            var movies = await _context.Movies.ToListAsync();
            var tickets = await _context.Tickets.Include(i => i.Movie).ToListAsync();
            StatisticsViewModel vm = new StatisticsViewModel
            {
                Income = tickets.Select(i => i.Price).Sum(),
                TicketsSold = tickets.Count,
                MostPopularMovie = movies.OrderByDescending(i => i.RatingCount).First()
            };

            return View(vm);
        }
        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}
