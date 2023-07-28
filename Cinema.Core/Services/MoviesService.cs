using Cinema.Core.Contracts;
using Cinema.Data;
using Cinema.Data.Enums;
using Cinema.Data.Models;
using Cinema.Core.Utilities;
using Cinema.ViewModels;
using Cinema.ViewModels.Actors;
using Cinema.ViewModels.Cinemas;
using Cinema.ViewModels.Movies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace Cinema.Core.Services
{
    public class MoviesService : IMoviesService
    {
        private readonly CinemaDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogService _logger;
        private readonly IImageService _imageService;
        public MoviesService(CinemaDbContext context, UserManager<ApplicationUser> userManager, IWebHostEnvironment webHostEnvironment, ILogService logger, IImageService imageService)
        {
            _context = context;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
            _imageService = imageService;
        }

        public async Task CreateMovieAsync(CreateMovieViewModel viewModel, string userEmail)
        {
            var actors = viewModel.ActorsDropdown.Where(i => i.IsChecked);
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
                Director = viewModel.Director,
                Actors = actors.Select(i => _context.Actors.FirstOrDefault(a => a.Id == i.Id)).ToList()
            };
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
            await _logger.LogActionAsync(UserActionType.Create, LogMessages.AddEntityMessage, "movie", movie.Title, $"({movie.Director} - {movie.RunningTime} minutes)");
        }

        public async Task DeleteByIdAsync(int? id)
        {
            var movie = await _context.Movies.FindAsync(id);
            _context.Movies.Remove(movie);
            await _imageService.DeleteImageAsync("Movies", movie.ImageUrl);
            await _context.SaveChangesAsync();
            await _logger.LogActionAsync(UserActionType.Delete, LogMessages.DeleteEntityMessage, "movie", movie.Title, $"({movie.Director} - {movie.RunningTime} minutes)");
        }

        public async Task EditByIdAsync(EditMovieViewModel viewModel)
        {
            var movie = await _context.Movies.Include(i => i.Actors).FirstOrDefaultAsync(i => i.Id == viewModel.Id);

            movie.Title = viewModel.Title;
            movie.Genre = await _context.Genres.FirstOrDefaultAsync(i => i.Id == viewModel.GenreId);
            movie.Description = viewModel.Description;
            movie.RunningTime = int.Parse(viewModel.RunningTime);
            movie.TrailerUrl = viewModel.TrailerUrl;

            if (viewModel.Image != null)
            {
                string photoName = await this.UploadPhoto(viewModel.Image);
                movie.ImageUrl = photoName;
            }
            var movieActors = movie.Actors;
            foreach (var actorViewModel in viewModel.ActorsDropdown)
            {
                var actor = await _context.Actors.FirstOrDefaultAsync(i => i.Id == actorViewModel.Id);
                if (movieActors.Any(i => i.Id == actor.Id))
                {
                    if (actorViewModel.IsChecked == false)
                    {
                        movieActors.Remove(actor);
                    }
                }
                else
                {
                    if (actorViewModel.IsChecked == true)
                    {
                        movieActors.Add(actor);
                    }
                }
            }
            _context.Update(movie);
            await _context.SaveChangesAsync();
            await _logger.LogActionAsync(UserActionType.Update, LogMessages.EditEntityMessage, "movie", movie.Title, "");
        }

        public async Task<bool> ExistsByIdAsync(int? id)
        {
            return await _context.Movies.AnyAsync(e => e.Id == id);
        }

        public async Task<SelectList> GetActorsDropDownAsync()
        {
            var actors = await _context.Actors.AsNoTracking().ToListAsync();
            return new SelectList(from a in actors select new { Id = a.Id, FullName = $"{a.FullName}" }, nameof(Actor.Id), "FullName");
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

        public async Task<MovieDetailsViewModel> GetDetailsViewModel(Movie movie, IEnumerable<UserMovie> ratingsS, string userEmail)
        {
            var currentUser = await _userManager.FindByEmailAsync(userEmail);
            var ratings = await _context.UsersMovies.Include(i => i.Customer).Where(i => i.MovieId == movie.Id).ToListAsync();
            var viewModel = new MovieDetailsViewModel
            {
                Director = movie.Director,
                Description = movie.Description,
                Genre = movie.Genre,
                ImageUrl = movie.ImageUrl,
                RunningTime = movie.RunningTime,
                Title = movie.Title,
                TrailerId = Regex.Match(movie.TrailerUrl, Constants.TrailerUrlRegex).Groups[3].Value,
                MovieId = movie.Id,
                Actors = movie.Actors.Select(i => i.FullName).ToList(),
                AverageRating = ratings.Count == 0 ? 0 : ratings.Select(i => i.Rating).Average(),
                RatingCount = ratings.Count,
                CurrentUserRating = ratings.FirstOrDefault(i => i.Customer.Email == currentUser.Email) == null ? null : ratings.FirstOrDefault(i => i.Customer.Email == currentUser.Email).Rating,
                UserCinemas = (await _context.Cinemas.Include(i => i.Movies).Where(i => i.OwnerId == currentUser.Id && i.ApprovalStatus == ApprovalStatus.Approved && !i.Movies.Any(m => m.MovieId == movie.Id)).ToListAsync()).Select(i => new CinemaCheckboxViewModel
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
            return await _context.UsersMovies.Include(i => i.Movie).Include(i => i.Customer).Where(i => i.Movie.Id == id).ToListAsync();
        }

        public async Task<CreateMovieViewModel> PrepareForAddingAsync()
        {
            return new CreateMovieViewModel
            {
                ActorsDropdown = await _context.Actors.Select(i => new ActorDropdownViewModel
                {
                    Id = i.Id,
                    FullName = i.FullName,
                    IsChecked = false
                }).ToListAsync(),
                Genres = await this.GetGenresDropDownAsync()
            };
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
            return await _imageService.UploadPhotoAsync("Movies", image);
        }

        public async Task<IEnumerable<MovieInfoCardViewModel>> SearchAndFilterMoviesAsync(string searchText, string filterValue, string sortBy)
        {
            var movies = await _context.Movies
                .Include(i => i.Actors)
                .Include(i => i.Genre)
                .Include(i => i.Cinemas)
                .Include(i => i.AddedBy)
                .ToListAsync();
            if (string.IsNullOrEmpty(searchText) == false)
            {
                movies = movies.Where(i => i.Title.ToLower().StartsWith(searchText.ToLower())).ToList();
            }
            int filter;
            if (string.IsNullOrEmpty(filterValue) == false && int.TryParse(filterValue, System.Globalization.NumberStyles.Integer, null, out filter))
            {
                var cinema = await _context.Cinemas.FirstOrDefaultAsync(i => i.Id == filter);
                movies = movies.Where(i => i.Cinemas.FirstOrDefault(i => i.CinemaId == cinema.Id) != null).ToList();
            }
            if (string.IsNullOrEmpty(sortBy) == false)
            {
                var sortParameter = sortBy.Split('-')[0];
                var sortDirection = sortBy.Split('-')[^1];

                switch (sortParameter)
                {
                    case "name":
                        movies = movies.OrderBy(i => i.Title).ToList();
                        if (sortDirection == "desc")
                        {
                            movies = movies.OrderByDescending(i => i.Title).ToList();
                        }
                        break;
                    case "genre":
                        movies = movies.OrderBy(i => i.Genre.Name).ToList();
                        if (sortDirection == "desc")
                        {
                            movies = movies.OrderByDescending(i => i.Genre.Name).ToList();
                        }
                        break;
                    case "rating":
                        movies = movies.OrderBy(i => i.UserRating).ToList();
                        if (sortDirection == "desc")
                        {
                            movies = movies.OrderByDescending(i => i.UserRating).ToList();
                        }
                        break;
                    case "ratingcount":
                        movies = movies.OrderBy(i => i.RatingCount).ToList();
                        if (sortDirection == "desc")
                        {
                            movies = movies.OrderByDescending(i => i.RatingCount).ToList();
                        }
                        break;
                    case "addedby":
                        movies = movies.OrderBy(i => $"{i.AddedBy.FirstName} {i.AddedBy.LastName}").ToList();
                        if (sortDirection == "desc")
                        {
                            movies = movies.OrderByDescending(i => $"{i.AddedBy.FirstName} {i.AddedBy.LastName}").ToList();
                        }
                        break;
                }
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
                Director = movie.Director,
                GenreId = movie.GenreId,
                Image = null,
                RunningTime = movie.RunningTime.ToString(),
                TrailerUrl = movie.TrailerUrl,
                ActorsDropdown = (await _context.Actors.ToListAsync()).Select(i => new ActorDropdownViewModel
                {
                    Id = i.Id,
                    FullName = i.FullName,
                    IsChecked = movie.Actors.Any(a => a.Id == i.Id)
                }).ToList(),
                Genres = await this.GetGenresDropDownAsync()
            };
        }

        public async Task<DeleteMovieViewModel> PrepareForDeleting(int? id)
        {
            var movie = await this.GetByIdAsync(id);
            return new DeleteMovieViewModel
            {
                Id = movie.Id,
                Name = movie.Title,
                ImageUrl = movie.ImageUrl
            };
        }
    }
}