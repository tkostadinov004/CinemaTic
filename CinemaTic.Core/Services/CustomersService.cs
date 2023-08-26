using CinemaTic.Core.Contracts;
using CinemaTic.Core.Utilities;
using CinemaTic.Data;
using CinemaTic.Data.Enums;
using CinemaTic.Data.Models;
using CinemaTic.Extensions;
using CinemaTic.ViewModels.Cinemas;
using CinemaTic.ViewModels.Customers;
using CinemaTic.ViewModels.Movies;
using CinemaTic.ViewModels.Sectors;
using CinemaTic.ViewModels.Tickets;
using CinemaTic.ViewModels.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text.RegularExpressions;

namespace CinemaTic.Core.Services
{
    public class CustomersService : ICustomersService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly CinemaDbContext _context;
        private readonly ILogService _logger;

        public CustomersService(UserManager<ApplicationUser> userManager, CinemaDbContext context, ILogService logger)
        {
            _userManager = userManager;
            _context = context;
            _logger = logger;
        }
        /// <summary>
        /// <para>Gets the home page view model of the customer's dashboard.</para>
        /// </summary>
        /// <returns>A <see cref="CustomerHomePageViewModel"/> object</returns>
        public async Task<CustomerHomePageViewModel> GetHomePageViewModelAsync(string userEmail)
        {
            var user = await _context.Users.Include(i => i.CinemasVisited).ThenInclude(i => i.Cinema).FirstOrDefaultAsync(i => i.Email == userEmail);

            if (user != null)
            {
                var mostPopularMovieGroup = _context.Tickets.Include(i => i.Movie).Where(i => i.CustomerId == user.Id).ToList().GroupBy(i => i.MovieId).OrderByDescending(i => i.Count()).FirstOrDefault();
                var mostPopularMovie = mostPopularMovieGroup != null ? mostPopularMovieGroup.FirstOrDefault() : null;
                var totalMoneySpent = await _context.Tickets.Where(i => i.CustomerId == user.Id).SumAsync(i => i.Price);
                var tickets = _context.Tickets.Include(i => i.Cinema).Where(i => i.CustomerId == user.Id);
                var mostPopularCinema = tickets.ToList().GroupBy(i => i.CinemaId).OrderByDescending(i => i.Count()).FirstOrDefault();
                return new CustomerHomePageViewModel
                {
                    Cinemas = user.CinemasVisited.Select(i => new CustomerCinemaViewModel
                    {
                        CinemaId = i.CinemaId,
                        CinemaName = i.Cinema.Name,
                        Description = i.Cinema.Description,
                        FavoriteSince = "",
                        ImageUrl = i.Cinema.ImageUrl
                    }),
                    FullName = $"{user.FirstName} {user.LastName}",
                    MostPopularMovieName = mostPopularMovie != null ? mostPopularMovie.Movie.Title : "",
                    MostPopularMoviePosterUrl = mostPopularMovie != null ? mostPopularMovie.Movie.ImageUrl : "",
                    MostPopularCinemaName = mostPopularCinema != null ? mostPopularCinema.FirstOrDefault().Cinema.Name : "",
                    TotalMoneySpent = totalMoneySpent,
                    CinemasCount = user.CinemasVisited.Count,
                    MoviesCount = await tickets.GroupBy(i => i.MovieId).CountAsync(),
                    TicketsCount = await tickets.CountAsync()
                };
            }
            return null;
        }
        /// <summary>
        /// <para>Gets cinemas from the database.</para>
        /// <para>If the value of <paramref name="all"/> is false, the method will return the given <see cref="ApplicationUser"/>'s favorite cinemas only.</para>
        /// </summary>
        /// <returns>A <see cref="PaginatedList{T}"/> of <see cref="CinemasViewModel"/></returns>
        public async Task<PaginatedList<CinemasViewModel>> QueryCinemasAsync(bool? all, string searchText, int? pageNumber, string userEmail)
        {
            var cinemas = await _context.Cinemas.Include(i => i.Owner).Include(i => i.Customers).ToListAsync();
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (string.IsNullOrEmpty(searchText) == false)
            {
                cinemas = cinemas.Where(i => i.Name.ToLower().StartsWith(searchText.ToLower())).ToList();
            }
            if (all.HasValue && all.Value == false)
            {
                cinemas = cinemas.Where(i => i.Customers.Any(c => c.CustomerId == user.Id)).ToList();
            }
            return await PaginatedList<CinemasViewModel>.CreateAsync(cinemas.Select(i => new CinemasViewModel
            {
                Id = i.Id,
                Description = i.Description,
                ImageUrl = i.ImageUrl,
                Name = i.Name,
                Owner = $"{i.Owner.FirstName} {i.Owner.LastName}",
                FoundedOn = i.FoundedOn.ToString(Constants.DateTimeFormat),
                IsInFavorites = _context.CustomersCinemas.Any(cc => cc.CinemaId == i.Id && cc.CustomerId == user.Id)
            }).OrderBy(i => i.Name), pageNumber ?? 1, 5);
        }
        /// <summary>
        /// <para>Adds a given <see cref="Cinema"/> to a given <see cref="ApplicationUser"/>'s collection of favorite cinemas.</para>
        /// </summary>
        public async Task AddCinemaToFavoritesAsync(int? cinemaId, string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            var cinema = await _context.Cinemas.FirstOrDefaultAsync(i => i.Id == cinemaId);

            _context.CustomersCinemas.Add(new CustomerCinema
            {
                CinemaId = cinema.Id,
                CustomerId = user.Id
            });
            await _context.SaveChangesAsync();
            await _logger.LogActionAsync(UserActionType.Create, LogMessages.AddCinemaToFavoritesMessage, cinema.Name);
        }
        /// <summary>
        /// <para>Removes a given <see cref="Cinema"/> from a given <see cref="ApplicationUser"/>'s collection of favorite cinemas.</para>
        /// </summary>
        public async Task RemoveCinemaFromFavoritesAsync(int? cinemaId, string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            var cinema = await _context.Cinemas.FirstOrDefaultAsync(i => i.Id == cinemaId);

            var customerCinema = await _context.CustomersCinemas.FirstOrDefaultAsync(i => i.CinemaId == cinema.Id && i.CustomerId == user.Id);
            _context.CustomersCinemas.Remove(customerCinema);
            await _context.SaveChangesAsync();
            await _logger.LogActionAsync(UserActionType.Delete, LogMessages.RemoveCinemaToFavoritesMessage, cinema.Name);
        }
        /// <summary>
        /// <para>Gets a collection of all tickets that a given <see cref="ApplicationUser"/> has.</para>
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="CustomerTicketViewModel"/></returns>
        public async Task<IEnumerable<CustomerTicketViewModel>> QueryTicketsAsync(string userEmail, string searchText, int? pageNumber)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            var tickets = await _context.Tickets.Include(i => i.Cinema).Include(i => i.Movie).Include(i => i.Sector).Where(i => i.CustomerId == user.Id).ToListAsync();
            if (string.IsNullOrEmpty(searchText) == false)
            {
                tickets = tickets.Where(i => i.Movie.Title.ToLower().StartsWith(searchText.ToLower())).ToList();
            }

            return await PaginatedList<CustomerTicketViewModel>.CreateAsync(tickets.OrderByDescending(i => i.ForDate).Select(i => new CustomerTicketViewModel
            {
                Id = i.Id,
                Movie = i.Movie.Title,
                MoviePosterUrl = i.Movie.ImageUrl,
                Sector = i.Sector.SectorName,
                SerialNumber = i.SerialNumber,
                Cinema = i.Cinema.Name,
                Date = i.ForDate.ToString(Constants.DateTimeFormat),
                Price = i.Price.ToString()
            }), pageNumber ?? 1, Constants.TicketsPerPage);
        }
        /// <summary>
        /// <para>Gets the details view model of a <see cref="Cinema"/>.</para>
        /// </summary>
        /// <returns>A <see cref="CustomerCinemaPageViewModel"/> object</returns>
        public async Task<CustomerCinemaPageViewModel> PrepareCinemaViewModelAsync(string userEmail, int? cinemaId)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            var cinema = await _context.Cinemas.Include(i => i.Movies).ThenInclude(i => i.Movie).ThenInclude(i => i.Genre).FirstOrDefaultAsync(i => i.Id == cinemaId);
            var dates = Enumerable.Range(0, 7).Select(i => DateTime.Now.AddDays(i))
                .ToDictionary(key =>
                {
                    return key.Date != DateTime.Now.Date ? key.DayOfWeek.ToString().Substring(0, 3) : "Today";
                }, value => value.ToString(Constants.DateTimeFormat));
            return new CustomerCinemaPageViewModel
            {
                Id = cinema.Id,
                Name = cinema.Name,
                CinemaLogoUrl = cinema.ImageUrl,
                ProfilePictureUrl = user.ProfilePictureUrl,
                Dates = dates,
                AccentColor = cinema.AccentColor,
                BackgroundColor = cinema.BackgroundColor,
                BoardColor = cinema.BoardColor,
                ButtonBackgroundColor = cinema.ButtonBackgroundColor,
                ButtonTextColor = cinema.ButtonTextColor,
                TextColor = cinema.TextColor,
                SponsorRecommendedMovies = cinema.Movies.OrderBy(i => new Random().Next()).Take(3).Select(i => new SponsorRecommendedMovieViewModel
                {
                    Id = i.MovieId,
                    ImageUrl = i.Movie.ImageUrl
                })
            };
        }
        /// <summary>
        /// <para>Gets a collection of all movies, offered in a given <see cref="Cinema"/> at a given <see cref="DateTime"/>.</para>
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="CinemaMovieViewModel"/></returns>
        public async Task<IEnumerable<CinemaMovieViewModel>> GetMoviesByDateAsync(int? cinemaId, DateTime date)
        {
            var cinema = await _context.Cinemas.Include(i => i.Schedule).Include(i => i.Movies).ThenInclude(i => i.Movie).ThenInclude(i => i.Genre).FirstOrDefaultAsync(i => i.Id == cinemaId);

            var movies = cinema.Movies.Where(i => i.FromDate <= date && i.ToDate >= date).ToList();

            return movies.Select(i => new CinemaMovieViewModel
            {
                Id = i.MovieId,
                CinemaId = i.CinemaId,
                Genre = i.Movie.Genre.Name,
                Name = i.Movie.Title,
                TrailerId = Regex.Match(i.Movie.TrailerUrl, Constants.TrailerUrlRegex).Groups[3].Value,
                RunningTime = i.Movie.RunningTime.ToString(),
                ImageUrl = i.Movie.ImageUrl,
                Schedule = cinema.Schedule.Where(k => k.CinemaId == cinema.Id && k.MovieId == i.MovieId && k.ForDateTime.Date == date.Date)
                .Select(t => t.ForDateTime)
                    .ToList(),
            });
        }
        /// <summary>
        /// <para>Gets the view model used for buying tickets.</para>
        /// </summary>
        /// <returns>A <see cref="BuyTicketViewModel"/> object.</returns>
        public async Task<BuyTicketViewModel> GetBuyTicketViewModelAsync(int? cinemaId, int? movieId, DateTime forDate)
        {
            var cinema = await _context.Cinemas.FirstOrDefaultAsync(i => i.Id == cinemaId);
            var movie = await _context.Movies.Include(i => i.Genre).FirstOrDefaultAsync(i => i.Id == movieId);
            return new BuyTicketViewModel
            {
                CinemaId = cinema.Id,
                CinemaName = cinema.Name,
                Description = movie.Description,
                Director = movie.Director,
                Genre = movie.Genre.Name,
                ImageUrl = movie.ImageUrl,
                MovieId = movie.Id,
                RunningTime = movie.RunningTime,
                Title = movie.Title,
                ForDateTime = forDate
            };
        }
        /// <summary>
        /// <para>Buys a ticket for a given <see cref="Movie"/> at a given <see cref="Cinema"/>.</para>
        /// </summary>
        public async Task BuyTicketAsync(int? sectorId, int? movieId, string userEmail, SectorDetailsViewModel sectorViewModel, DateTime forDate)
        {
            var sector = await _context.Sectors.FirstOrDefaultAsync(i => i.Id == sectorId);
            var cinemaMovie = await _context.CinemasMovies.Include(i => i.Cinema).Include(i => i.Movie).FirstOrDefaultAsync(i => i.CinemaId == sector.CinemaId && i.MovieId == movieId);
            var selectedSeats = sectorViewModel.Seats.SelectMany(i => i).Where(i => i.IsChecked && i.IsOccupied == false);
            foreach (var seat in selectedSeats)
            {
                Ticket ticket = new Ticket
                {
                    CinemaId = sector.CinemaId,
                    SerialNumber = $"R{seat.Row}C{seat.Col}",
                    ForDate = forDate,
                    MovieId = movieId.Value,
                    SectorId = sectorId.Value,
                    CustomerId = (await _userManager.FindByEmailAsync(userEmail)).Id,
                    Price = cinemaMovie.TicketPrice
                };
                _context.Tickets.Add(ticket);
                await _logger.LogActionAsync(UserActionType.Create, LogMessages.PurchaseTicketMessage, $"R{seat.Row}C{seat.Col}", cinemaMovie.Movie.Title, cinemaMovie.Cinema.Name, forDate.Date.ToString(Constants.DateTimeFormat));
            }
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// <para>Sets or changes a given <see cref="ApplicationUser"/>'s rating of a given <see cref="Movie"/>.</para>
        /// </summary>
        public async Task SetRatingToMovieAsync(int? id, decimal rating, string userEmail)
        {
            var currentUser = await _userManager.FindByEmailAsync(userEmail);
            var movie = _context.Movies.Include(i => i.Genre).ToListAsync().Result.FirstOrDefault(i => i.Id == id);
            var usersRatedMovie = _context.UsersMovies.Include(i => i.Movie).Include(i => i.Customer).ToList();

            decimal currentRating = rating;

            var userMovie = usersRatedMovie.FirstOrDefault(i => i.Customer.Email == currentUser.Email && i.Movie.Id == movie.Id);

            string outputMessage = "";
            if (userMovie == null)
            {
                movie.UserRating = ((movie.UserRating * movie.RatingCount) + currentRating) / (movie.RatingCount + 1);
                movie.RatingCount++;

                _context.UsersMovies.Add(new UserMovie
                {
                    MovieId = movie.Id,
                    CustomerId = currentUser.Id,
                    Rating = currentRating
                });
                await _context.SaveChangesAsync();
                await _logger.LogActionAsync(UserActionType.Create, LogMessages.RateMovieMessage, movie.Title, $"{currentRating:f1}");
            }
            else
            {
                currentRating = usersRatedMovie.FirstOrDefault(i => i.Customer.Email == currentUser.Email && i.Movie.Id == movie.Id).Rating - rating;

                decimal oldRating = userMovie.Rating;
                userMovie.Rating = rating;
                movie.UserRating = ((movie.UserRating * movie.RatingCount) - currentRating) / movie.RatingCount;
                await _context.SaveChangesAsync();
                await _logger.LogActionAsync(UserActionType.Update, LogMessages.ChangeRatingMovieMessage, movie.Title, $"{oldRating:f1}", $"{rating:f1}");
            }
        }
        /// <summary>
        /// <para>Checks whether an <see cref="ApplicationUser"/> has a given <see cref="Cinema"/> in their favorites collection.</para>
        /// </summary>
        /// <returns><see cref="bool"/></returns>
        public async Task<bool> CustomerHasCinemaAsync(int? cinemaId, string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            return _context.CustomersCinemas.Where(i => i.CustomerId == user.Id).Any(i => i.CinemaId == cinemaId);
        }
    }
}
