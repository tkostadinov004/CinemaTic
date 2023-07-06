using Cinema.Core.Contracts;
using Cinema.Data;
using Cinema.Data.Models;
using Cinema.Utilities;
using Cinema.ViewModels;
using Cinema.ViewModels.Cinemas;
using Cinema.ViewModels.Contracts;
using Cinema.ViewModels.Movies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Core.Services
{
    public class MoviesService : IMoviesService
    {
        private readonly CinemaDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public MoviesService(CinemaDbContext context, UserManager<ApplicationUser> userManager, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task CreateMovieAsync(IViewModel item, IEnumerable<string> actors, string userEmail)
        {
            CreateMovieViewModel viewModel = item as CreateMovieViewModel;
            Movie movie = new Movie
            {
                Description = viewModel.Description,
                RunningTime = viewModel.RunningTime,
                Title = viewModel.Title,
                ImageUrl = await this.UploadPhoto(viewModel.Image),
                TrailerUrl = viewModel.TrailerUrl,
                UserRating = 0,
                GenreId = viewModel.GenreId,
                AddedBy = await _userManager.FindByEmailAsync(userEmail),
                Director = viewModel.Director
            };

            foreach (string id in actors)
            {
                movie.Actors.Add(_context.Actors.FirstOrDefault(i => i.Id == int.Parse(id)));
            }
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int? id)
        {
            var movie = await _context.Movies.FindAsync(id);
            _context.Movies.Remove(movie);
            await GlobalMethods.DeleteImage("Movies", movie.ImageUrl, _context, _webHostEnvironment);
            await _context.SaveChangesAsync();
        }

        public async Task EditByIdAsync(IViewModel item, int id, int genreId, IEnumerable<string> actors)
        {
            EditMovieViewModel viewModel = item as EditMovieViewModel;

            var movie = await _context.Movies.FirstOrDefaultAsync(i => i.Id == id);

            movie.Title = viewModel.Title;
            movie.Genre = await _context.Genres.FirstOrDefaultAsync(i => i.Id == genreId);
            movie.Description = viewModel.Description;
            movie.RunningTime = int.Parse(viewModel.RunningTime);
            movie.TrailerUrl = viewModel.TrailerUrl;
           // movie.Date = viewModel.Date;
           // movie.Price = viewModel.Price;

            if (viewModel.Image != null)
            {
                string photoName = await this.UploadPhoto(viewModel.Image);
                movie.ImageUrl = photoName;
            }
            foreach (string actorId in actors)
            {
                if (!movie.Actors.Contains(await _context.Actors.FirstOrDefaultAsync(i => i.Id == int.Parse(actorId))))
                {
                    movie.Actors.Add(_context.Actors.FirstOrDefault(i => i.Id == int.Parse(actorId)));
                }
            }
            _context.Update(movie);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsByIdAsync(int? id)
        {
            return await _context.Movies.AnyAsync(e => e.Id == id);
        }

        public async Task<SelectList> GetActorsDropDownAsync()
        {
            var actors = await _context.Actors.AsNoTracking().ToListAsync();
            return new SelectList(from a in actors select new { Id = a.Id, FullName = $"{a.FirstName} {a.LastName}" }, nameof(Actor.Id), "FullName");
        }

        public async Task<IEnumerable<Movie>> GetAllAsync()
        {
            return await _context.Movies
                .Include(i => i.Actors)
                .Include(i => i.Genre)
                .Include(i => i.Cinemas)
                .Include(i => i.AddedBy)
                .ToListAsync();
        }

        public async Task<Movie> GetByIdAsync(int? id)
        {
            return await _context.Movies
                .Include(i => i.Actors)
                .Include(i => i.Genre)
                .Include(i => i.Cinemas)
                .Include(i => i.AddedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<ApplicationUser> GetCurrentUserAsync(ControllerBase controllerBase)
        {
            return await _userManager.GetUserAsync(controllerBase.HttpContext.User);
        }

        public async Task<MovieDetailsViewModel> GetDetailsViewModel(Movie movie, IEnumerable<UserMovie> ratings, string userEmail)
        {
            var currentUser = await _userManager.FindByEmailAsync(userEmail);
            var viewModel = new MovieDetailsViewModel
            {
                Director = movie.Director,
                Description = movie.Description,
                Genre = movie.Genre,
                ImageUrl = movie.ImageUrl,
                RunningTime = movie.RunningTime,
                Title = movie.Title,
                TrailerUrl = movie.TrailerUrl,
                MovieId = movie.Id,
                Actors = movie.Actors.Select(i => $"{i.FirstName} {i.LastName}").ToList(),
                AverageRating = ratings.ToList().Count == 0 ? 0 : ratings.Select(i => i.Rating).Average(),
                RatingCount = ratings.Count(),
                CurrentUserRating = ratings.FirstOrDefault(i => i.User.Email == currentUser.Email) == null ? null : ratings.FirstOrDefault(i => i.User.Email == currentUser.Email).Rating,
                UserCinemas = (await _context.Cinemas.Where(i => i.OwnerId == currentUser.Id).ToListAsync()).Select(i => new CinemaCheckboxViewModel
                {
                    Id = i.Id,
                    Name = i.Name
                }).ToList(),
                ActorsDropdown = await this.GetActorsDropDownAsync(),
                Genres = await this.GetGenresDropDownAsync(),
                GenreId = movie.GenreId,
            };
            return viewModel;
        }

        public async Task<SelectList> GetGenresDropDownAsync()
        {
            return new SelectList(await _context.Genres.AsNoTracking().ToListAsync(), nameof(Genre.Id), nameof(Genre.Name));
        }

        public async Task<IEnumerable<UserMovie>> GetRatingsByMovieIdAsync(int? id)
        {
            return await _context.UsersMovies.Include(i => i.Movie).Include(i => i.User).Where(i => i.Movie.Id == id).ToListAsync();
        }

        public async Task<StatisticsViewModel> GetStatistics()
        {
            var movies = await _context.Movies.ToListAsync();
            var tickets = await _context.Tickets.Include(i => i.Movie).ToListAsync();
            StatisticsViewModel vm = new StatisticsViewModel
            {
                Income = tickets.Select(i => i.Price).Sum(),
                TicketsSold = tickets.Count,
                MostPopularMovie = movies.OrderByDescending(i => i.RatingCount).First()
            };
            return vm;
        }

        public async Task<CreateMovieViewModel> PrepareForAddingAsync()
        {
            return new CreateMovieViewModel
            {
                ActorsDropdown = await this.GetActorsDropDownAsync(),
                Genres = await this.GetGenresDropDownAsync()
            };
        }

        public async Task SetRatingAsync(int id, decimal rating, string userEmail)
        {
            var currentUser = await _userManager.FindByEmailAsync(userEmail);
            var movie = _context.Movies.Include(i => i.Genre).ToListAsync().Result.FirstOrDefault(i => i.Id == id);
            var usersRatedMovie = _context.UsersMovies.Include(i => i.Movie).Include(i => i.User).ToList();

            decimal currentRating = rating;

            var userMovie = usersRatedMovie.FirstOrDefault(i => i.User.Email == currentUser.Email && i.Movie.Id == movie.Id);

            if (userMovie == null)
            {
                movie.UserRating = ((movie.UserRating * movie.RatingCount) + currentRating) / (movie.RatingCount + 1);
                movie.RatingCount++;

                _context.UsersMovies.Add(new UserMovie
                {
                    MovieId = movie.Id,
                    UserId = currentUser.Id,
                    Rating = currentRating
                });
            }
            else
            {
                currentRating = usersRatedMovie.FirstOrDefault(i => i.User.Email == currentUser.Email && i.Movie.Id == movie.Id).Rating - rating;

                userMovie.Rating = rating;
                movie.UserRating = ((movie.UserRating * movie.RatingCount) - currentRating) / movie.RatingCount;
            }
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<MovieInfoCardViewModel>> GetAllMoviesAsync()
        {
            var movies = await _context.Movies.Include(i => i.Genre).Include(i => i.AddedBy).ToListAsync();

            return movies.Select(i => new MovieInfoCardViewModel
            {
                Id = i.Id,
                Name = i.Title,
                ImageUrl = i.ImageUrl,
                AverageRating = i.UserRating.Value,
                Genre = i.Genre.Name,
                RatingCount = i.RatingCount,
                AddedBy = $"{i.AddedBy.FirstName} {i.AddedBy.LastName}"
            }).ToList();
        }
        public async Task<string> UploadPhoto(IFormFile image)
        {
            return GlobalMethods.UploadPhoto("Movies", image, _webHostEnvironment);
        }

        public async Task<IEnumerable<MovieInfoCardViewModel>> SearchAndFilterMoviesAsync(string searchText, string filterValue)
        {
            var movies = await this.GetAllAsync();
            if (string.IsNullOrEmpty(searchText) == false)
            {
                movies = movies.Where(i => i.Title.StartsWith(searchText));
            }
            if (string.IsNullOrEmpty(filterValue) == false && filterValue != "All")
            {
                var cinema = await _context.Cinemas.FirstOrDefaultAsync(i => i.Id == int.Parse(filterValue));
                movies = movies.Where(i => i.Cinemas.FirstOrDefault(i => i.CinemaId == cinema.Id) != null);
            }
            return movies.Select(i => new MovieInfoCardViewModel
            {
                Id = i.Id,
                Name = i.Title,
                ImageUrl = i.ImageUrl,
                AverageRating = i.UserRating.Value,
                Genre = i.Genre.Name,
                RatingCount = i.RatingCount,
                AddedBy = $"{i.AddedBy.FirstName} {i.AddedBy.LastName}"
            }).ToList();
        }

        public async Task<FilterMoviesViewModel> PrepareFilterViewModelAsync()
        {
            return new FilterMoviesViewModel
            {
                Cinemas = new SelectList(await _context.Cinemas.AsNoTracking().ToListAsync(), nameof(Data.Models.Cinema.Id), nameof(Data.Models.Cinema.Name))
            };
        }

        public async Task<EditMovieViewModel> PrepareForEditing(int? id)
        {
            var movie = await this.GetByIdAsync(id);
            return new EditMovieViewModel
            {
                Id = movie.Id,
                Title = movie.Title,
                Description = movie.Description,
                Director = movie.Description,
                GenreId = movie.GenreId,
                Actors = null,
                Image = null,
                RunningTime = movie.RunningTime.ToString(),
                TrailerUrl = movie.TrailerUrl,
                ActorsDropdown = await this.GetActorsDropDownAsync(),
                Genres = await this.GetGenresDropDownAsync()
            };
        }
    }
}