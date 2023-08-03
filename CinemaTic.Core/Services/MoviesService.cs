using CinemaTic.Core.Contracts;
using CinemaTic.Data;
using CinemaTic.Data.Enums;
using CinemaTic.Data.Models;
using CinemaTic.Core.Utilities;
using CinemaTic.ViewModels;
using CinemaTic.ViewModels.Actors;
using CinemaTic.ViewModels.Cinemas;
using CinemaTic.ViewModels.Movies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using CinemaTic.Extensions;

namespace CinemaTic.Core.Services
{
    public class MoviesService : IMoviesService
    {
        private readonly CinemaDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogService _logger;
        private readonly IImageService _imageService;
        public MoviesService(CinemaDbContext context, UserManager<ApplicationUser> userManager, ILogService logger, IImageService imageService)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
            _imageService = imageService;
        }

        public async Task CreateMovieAsync(CreateMovieViewModel viewModel, string userEmail)
        {
            Movie movie = new Movie
            {
                Description = viewModel.Description,
                RunningTime = int.Parse(viewModel.RunningTime),
                Title = viewModel.Title,
                ImageUrl = await this.UploadPhotoAsync(viewModel.Image),
                TrailerUrl = viewModel.TrailerUrl,
                UserRating = 0,
                GenreId = viewModel.GenreId,
                AddedBy = await _userManager.FindByEmailAsync(userEmail),
                Director = viewModel.Director
            };
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
            if (viewModel.ActorsDropdown != null)
            {
                movie.Actors = viewModel.ActorsDropdown.Where(i => i.IsChecked).Select(i => new ActorMovie
                {
                    ActorId = i.Id,
                    MovieId = movie.Id
                }).ToList();
            }
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

        public async Task EditMovieAsync(EditMovieViewModel viewModel)
        {
            var movie = await _context.Movies.Include(i => i.Actors).FirstOrDefaultAsync(i => i.Id == viewModel.Id);

            movie.Title = viewModel.Title;
            movie.Genre = await _context.Genres.FirstOrDefaultAsync(i => i.Id == viewModel.GenreId);
            movie.Description = viewModel.Description;
            movie.RunningTime = int.Parse(viewModel.RunningTime);
            movie.TrailerUrl = viewModel.TrailerUrl;

            if (viewModel.Image != null)
            {
                string photoName = await this.UploadPhotoAsync(viewModel.Image);
                movie.ImageUrl = photoName;
            }
            var movieActors = movie.Actors;
            foreach (var actorViewModel in viewModel.ActorsDropdown)
            {
                var actor = await _context.Actors.FirstOrDefaultAsync(i => i.Id == actorViewModel.Id);
                if (actor != null)
                {
                    var actorMovie = await _context.ActorsMovies.FirstOrDefaultAsync(i => i.MovieId == movie.Id && i.ActorId == actor.Id);
                    if (actorMovie != null)
                    {
                        if (actorViewModel.IsChecked == false)
                        {
                            movieActors.Remove(actorMovie);
                        }
                    }
                    else
                    {
                        if (actorViewModel.IsChecked == true)
                        {
                            movieActors.Add(new ActorMovie
                            {
                                ActorId = actor.Id,
                                MovieId = movie.Id
                            });
                        }
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

        public async Task<SelectList> GetActorsDropdownAsync()
        {
            var actors = await _context.Actors.OrderBy(i => i.FullName).AsNoTracking().ToListAsync();
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
                .ThenInclude(i => i.Actor)
                .Include(i => i.Genre)
                .Include(i => i.Cinemas)
                .Include(i => i.AddedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<MovieDetailsViewModel> GetDetailsViewModelAsync(int? id, string userEmail)
        {
            var currentUser = await _userManager.FindByEmailAsync(userEmail);
            if (id != null)
            {
                var movie = await _context.Movies.Include(i => i.Actors).ThenInclude(i => i.Actor).FirstOrDefaultAsync(i => i.Id == id);
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
                    Actors = movie.Actors.Select(i => i.Actor.FullName).ToList(),
                    AverageRating = ratings.Count == 0 ? 0 : ratings.Select(i => i.Rating).Average(),
                    RatingCount = ratings.Count,
                    CurrentUserRating = ratings.FirstOrDefault(i => i.Customer.Email == currentUser.Email) == null ? null : ratings.FirstOrDefault(i => i.Customer.Email == currentUser.Email).Rating,
                    UserCinemas = (await _context.Cinemas.Include(i => i.Movies).Where(i => i.OwnerId == currentUser.Id && i.ApprovalStatus == ApprovalStatus.Approved && !i.Movies.Any(m => m.MovieId == movie.Id)).ToListAsync()).Select(i => new CinemaCheckboxViewModel
                    {
                        Id = i.Id,
                        Name = i.Name
                    }).ToList(),
                    ActorsDropdown = await this.GetActorsDropdownAsync(),
                    Genres = await this.GetGenresDropdownAsync(),
                    GenreId = movie.GenreId
                };
                return viewModel;
            }
            return null;
        }

        public async Task<SelectList> GetGenresDropdownAsync()
        {
            return new SelectList(await _context.Genres.AsNoTracking().ToListAsync(), nameof(Genre.Id), nameof(Genre.Name));
        }

        public async Task<CreateMovieViewModel> GetCreateViewModelAsync()
        {
            return new CreateMovieViewModel
            {
                ActorsDropdown = await _context.Actors.OrderBy(i => i.FullName).Select(i => new ActorDropdownViewModel
                {
                    Id = i.Id,
                    FullName = i.FullName,
                    IsChecked = false
                }).ToListAsync(),
                Genres = await this.GetGenresDropdownAsync()
            };
        }
        public async Task<string> UploadPhotoAsync(IFormFile image)
        {
            return await _imageService.UploadPhotoAsync("Movies", image);
        }
        public async Task AddMovieToCinemasAsync(MovieDetailsViewModel viewModel)
        {
            var movie = await _context.Movies.Include(i => i.Cinemas).FirstOrDefaultAsync(i => i.Id == viewModel.MovieId);
            foreach (var cinemaViewModel in viewModel.UserCinemas)
            {
                var cinema = await _context.Cinemas.FirstOrDefaultAsync(i => i.Id == cinemaViewModel.Id);
                if (cinemaViewModel.FromDate != default && cinemaViewModel.ToDate != default && cinemaViewModel.TicketPrice > 0)
                {
                    if (!movie.Cinemas.Any(i => i.CinemaId == cinema.Id))
                    {
                        movie.Cinemas.Add(new CinemaMovie
                        {
                            CinemaId = cinema.Id,
                            FromDate = cinemaViewModel.FromDate,
                            ToDate = cinemaViewModel.ToDate,
                            TicketPrice = cinemaViewModel.TicketPrice
                        });
                    }
                }
                await _logger.LogActionAsync(UserActionType.Create, LogMessages.AddMovieToCinemaMessage, movie.Title, cinema.Name);
            }
            await _context.SaveChangesAsync();
        }
        public async Task<PaginatedList<MovieInfoCardViewModel>> SearchAndFilterMoviesAsync(string searchText, string filterValue, string sortBy, int? pageNumber)
        {
            var movies = _context.Movies
                .Include(i => i.Actors)
                .Include(i => i.Genre)
                .Include(i => i.Cinemas)
                .Include(i => i.AddedBy).OrderBy(i => i.Title).AsQueryable();
            if (string.IsNullOrEmpty(searchText) == false)
            {
                movies = movies.Where(i => i.Title.ToLower().StartsWith(searchText.ToLower()));
            }
            int filter;
            if (string.IsNullOrEmpty(filterValue) == false && int.TryParse(filterValue, System.Globalization.NumberStyles.Integer, null, out filter))
            {
                var cinema = await _context.Cinemas.FirstOrDefaultAsync(i => i.Id == filter);
                movies = movies.Where(i => i.Cinemas.FirstOrDefault(i => i.CinemaId == cinema.Id) != null);
            }
            if (string.IsNullOrEmpty(sortBy) == false)
            {
                var sortParameter = sortBy.Split('-')[0];
                var sortDirection = sortBy.Split('-')[^1];

                switch (sortParameter)
                {
                    case "name":
                        movies = movies.OrderBy(i => i.Title);
                        if (sortDirection == "desc")
                        {
                            movies = movies.OrderByDescending(i => i.Title);
                        }
                        break;
                    case "genre":
                        movies = movies.OrderBy(i => i.Genre.Name);
                        if (sortDirection == "desc")
                        {
                            movies = movies.OrderByDescending(i => i.Genre.Name);
                        }
                        break;
                    case "rating":
                        movies = movies.OrderBy(i => i.UserRating);
                        if (sortDirection == "desc")
                        {
                            movies = movies.OrderByDescending(i => i.UserRating);
                        }
                        break;
                    case "ratingcount":
                        movies = movies.OrderBy(i => i.RatingCount);
                        if (sortDirection == "desc")
                        {
                            movies = movies.OrderByDescending(i => i.RatingCount);
                        }
                        break;
                    case "addedby":
                        movies = movies.OrderBy(i => $"{i.AddedBy.FirstName} {i.AddedBy.LastName}");
                        if (sortDirection == "desc")
                        {
                            movies = movies.OrderByDescending(i => $"{i.AddedBy.FirstName} {i.AddedBy.LastName}");
                        }
                        break;
                }
            }
            return await PaginatedList<MovieInfoCardViewModel>.CreateAsync(movies.Select(i => new MovieInfoCardViewModel
            {
                Id = i.Id,
                Name = i.Title,
                ImageUrl = i.ImageUrl,
                AverageRating = i.UserRating.Value,
                Genre = i.Genre != null ? i.Genre.Name : "No genre",
                RatingCount = i.RatingCount,
                AddedBy = $"{i.AddedBy.FirstName} {i.AddedBy.LastName}"
            }).ToList(), pageNumber ?? 1, 8);
        }

        public async Task<FilterMoviesViewModel> GetFilterViewModelAsync()
        {
            return new FilterMoviesViewModel
            {
                Cinemas = new SelectList(await _context.Cinemas.AsNoTracking().ToListAsync(), nameof(Data.Models.Cinema.Id), nameof(Data.Models.Cinema.Name))
            };
        }

        public async Task<EditMovieViewModel> GetEditViewModelAsync(int? id)
        {
            var movie = await this.GetByIdAsync(id);
            if (movie != null)
            {
                return new EditMovieViewModel
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    Description = movie.Description,
                    Director = movie.Director,
                    GenreId = movie.GenreId ?? 0,
                    Image = null,
                    RunningTime = movie.RunningTime.ToString(),
                    TrailerUrl = movie.TrailerUrl,
                    ActorsDropdown = (await _context.Actors.OrderBy(i => i.FullName).ToListAsync()).Select(i => new ActorDropdownViewModel
                    {
                        Id = i.Id,
                        FullName = i.FullName,
                        IsChecked = movie.Actors.Any(a => a.ActorId == i.Id)
                    }).ToList(),
                    Genres = await this.GetGenresDropdownAsync()
                };
            }
            return null;
        }

        public async Task<DeleteMovieViewModel> GetDeleteViewModelAsync(int? id)
        {
            var movie = await this.GetByIdAsync(id);
            if (movie != null)
            {
                return new DeleteMovieViewModel
                {
                    Id = movie.Id,
                    Name = movie.Title,
                    ImageUrl = movie.ImageUrl
                };
            }
            return null;
        }

        public async Task<SetMovieScheduleViewModel> GetSetMovieSchedulePartialAsync(int? cinemaId, int? movieId)
        {
            var cinemaMovie = await _context.CinemasMovies.FirstOrDefaultAsync(i => i.CinemaId == cinemaId && i.MovieId == movieId);
            if (cinemaMovie != null)
            {
                var dates = await GlobalMethods.GetDateRangeAsync(cinemaMovie.FromDate, cinemaMovie.ToDate);
                var scheduleViewModels = dates.Select(i => new DateTimeScheduleViewModel
                {
                    Date = i.Date,
                    Times = _context.CinemasMoviesTimes.Where(c => c.CinemaId == cinemaId && c.MovieId == movieId && c.ForDateTime.Date == i.Date).Select(c => c.ForDateTime).ToList(),
                    CinemaId = cinemaMovie.CinemaId,
                    MovieId = cinemaMovie.MovieId
                }).ToList();
                return new SetMovieScheduleViewModel
                {
                    CinemaId = cinemaMovie.CinemaId,
                    MovieId = cinemaMovie.MovieId,
                    Dates = scheduleViewModels
                };
            }
            return null;
        }

        public async Task SetMovieScheduleAsync(SetMovieScheduleViewModel viewModel)
        {
            var movie = await _context.Movies.FirstOrDefaultAsync(i => i.Id == viewModel.MovieId);
            var cinema = await _context.Cinemas.FirstOrDefaultAsync(i => i.Id == viewModel.CinemaId);
            if (movie != null && cinema != null && viewModel.Dates != null)
            {
                foreach (var date in viewModel.Dates)
                {
                    if (date.Times != null)
                    {
                        var schedule = await _context.CinemasMoviesTimes.Where(i => i.CinemaId == cinema.Id && i.MovieId == movie.Id && i.ForDateTime.Date == date.Date).ToListAsync();
                        var timesForAdding = date.Times.DistinctBy(i => i.TimeOfDay).Where(i => !schedule.Any(s => s.ForDateTime.TimeOfDay == i.TimeOfDay)).Select(i => new CinemaMovieTime
                        {
                            CinemaId = viewModel.CinemaId,
                            MovieId = viewModel.MovieId,
                            ForDateTime = date.Date.Date + i.TimeOfDay
                        });
                        var timesForDeleting = schedule.Where(i => !date.Times.Any(t => t.TimeOfDay == i.ForDateTime.TimeOfDay));

                        _context.AddRange(timesForAdding);
                        _context.RemoveRange(timesForDeleting);
                    }
                }
                await _context.SaveChangesAsync();
                await _logger.LogActionAsync(UserActionType.Update, LogMessages.SetMovieSchedule, movie.Title, cinema.Name);
            }
        }

        public async Task<EditCinemaMovieDataViewModel> GetEditCinemaMovieDataViewModelAsync(int? cinemaId, int? movieId)
        {
            var cinemaMovie = await _context.CinemasMovies.FirstOrDefaultAsync(i => i.CinemaId == cinemaId && i.MovieId == movieId);
            if (cinemaMovie != null)
            {
                return new EditCinemaMovieDataViewModel
                {
                    CinemaId = cinemaMovie.CinemaId,
                    MovieId = cinemaMovie.MovieId,
                    FromDate = cinemaMovie.FromDate,
                    ToDate = cinemaMovie.ToDate,
                    TicketPrice = cinemaMovie.TicketPrice
                };
            }
            return null;
        }

        public async Task EditCinemaMovieDataAsync(EditCinemaMovieDataViewModel viewModel)
        {
            var cinemaMovie = await _context.CinemasMovies.FirstOrDefaultAsync(i => i.CinemaId == viewModel.CinemaId && i.MovieId == viewModel.MovieId);
            if (cinemaMovie != null)
            {
                var excludedDatesSchedules = Enumerable.Except(await GlobalMethods.GetDateRangeAsync(cinemaMovie.FromDate, cinemaMovie.ToDate), await GlobalMethods.GetDateRangeAsync(viewModel.FromDate, viewModel.ToDate))
                    .SelectMany(date =>
                    {
                        return _context.CinemasMoviesTimes.Where(ct => ct.CinemaId == cinemaMovie.CinemaId && ct.MovieId == cinemaMovie.MovieId && ct.ForDateTime.Date == date).ToList();
                    });
                _context.CinemasMoviesTimes.RemoveRange(excludedDatesSchedules);

                cinemaMovie.FromDate = viewModel.FromDate;
                cinemaMovie.ToDate = viewModel.ToDate;
                cinemaMovie.TicketPrice = viewModel.TicketPrice;
                await _context.SaveChangesAsync();
            }
        }
    }
}