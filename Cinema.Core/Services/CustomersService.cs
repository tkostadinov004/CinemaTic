using Cinema.Core.Contracts;
using Cinema.Core.Utilities;
using Cinema.Data;
using Cinema.Data.Models;
using Cinema.ViewModels.Cinemas;
using Cinema.ViewModels.Contracts;
using Cinema.ViewModels.Customers;
using Cinema.ViewModels.Movies;
using Cinema.ViewModels.Sectors;
using Cinema.ViewModels.Tickets;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cinema.Core.Services
{
    public class CustomersService : ICustomersService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly CinemaDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public CustomersService(UserManager<ApplicationUser> userManager, CinemaDbContext context, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _context = context;
            _signInManager = signInManager;
        }
        public async Task<IEnumerable<ApplicationUser>> GetAllAsync()
        {
            //var users = await _userManager.Users
            //    .Include(i => i.Tickets).ThenInclude(i => i.Seat)
            //    .Include(i => i.Tickets).ThenInclude(ticket => ticket.Movie)
            //    .ToListAsync();
            //return users.Where(i => this.IsVisitorAsync(i).Result);
            return null;
        }

        public async Task<CustomerHomePageViewModel> GetCinemasForUserAsync(string userEmail)
        {
            var user = await _context.Users.Include(i => i.CinemasVisited).ThenInclude(i => i.Cinema).FirstOrDefaultAsync(i => i.Email == userEmail);

            return new CustomerHomePageViewModel
            {
                Cinemas = user.CinemasVisited.Select(i => new CustomerCinemaViewModel
                {
                    CinemaId = i.CinemaId,
                    CinemaName = i.Cinema.Name,
                    Description = i.Cinema.Description,
                    FavoriteSince = "",
                    ImageUrl = i.Cinema.ImageUrl
                })
            };
        }

        public async Task<ChangePasswordViewModel> GetChangePasswordViewModelAsync(string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            return new ChangePasswordViewModel
            {
                Id = user.Id,
                Email = user.Email
            };
        }

        private async Task<bool> IsCustomerAsync(ApplicationUser user)
        {
            return await _userManager.IsInRoleAsync(user, "Customer");
        }

        public async Task ChangePasswordAsync(ChangePasswordViewModel viewModel)
        {
            var user = await _userManager.FindByIdAsync(viewModel.Id);
            await _userManager.ChangePasswordAsync(user, viewModel.OldPassword, viewModel.NewPassword);
            await _signInManager.RefreshSignInAsync(user);
        }

        public async Task<IEnumerable<CinemasViewModel>> GetCinemasAsync(bool? all, string userEmail)
        {
            var cinemas = await _context.Cinemas.Include(i => i.Owner).Include(i => i.Customers).ToListAsync();
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (all.HasValue && all.Value == false)
            {
                cinemas = cinemas.Where(i => i.Customers.Any(c => c.CustomerId == user.Id)).ToList();
            }
            return cinemas.Select(i => new CinemasViewModel
            {
                Id = i.Id,
                Description = i.Description,
                ImageUrl = i.ImageUrl,
                Name = i.Name,
                Owner = $"{i.Owner.FirstName} {i.Owner.LastName}",
                FoundedOn = i.FoundedOn.ToString(Constants.DateTimeFormat),
                IsInFavorites = _context.CustomersCinemas.Any(cc => cc.CinemaId == i.Id && cc.CustomerId == user.Id)
            });
        }

        public async Task AddCinemaToFavoritesAsync(int cinemaId, string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            var cinema = await _context.Cinemas.FirstOrDefaultAsync(i => i.Id == cinemaId);

            _context.CustomersCinemas.Add(new CustomerCinema
            {
                CinemaId = cinema.Id,
                CustomerId = user.Id
            });
            await _context.SaveChangesAsync();
        }

        public async Task RemoveCinemaFromFavoritesAsync(int cinemaId, string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            var cinema = await _context.Cinemas.FirstOrDefaultAsync(i => i.Id == cinemaId);

            var customerCinema = await _context.CustomersCinemas.FirstOrDefaultAsync(i => i.CinemaId == cinema.Id && i.CustomerId == user.Id);
            _context.CustomersCinemas.Remove(customerCinema);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CustomerTicketViewModel>> GetTicketsForCustomerAsync(string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            var tickets = await _context.Tickets.Include(i => i.Cinema).Include(i => i.Movie).Include(i => i.Sector).Where(i => i.CustomerId == user.Id).ToListAsync();

            return tickets.Select(i => new CustomerTicketViewModel
            {
                Id = i.Id,
                Movie = i.Movie.Title,
                MoviePosterUrl = i.Movie.ImageUrl,
                Sector = i.Sector.SectorName,
                SerialNumber = i.SerialNumber,
                Cinema = i.Cinema.Name,
                Date = i.ForDate.ToString(Constants.DateTimeFormat),
                Price = i.Price.ToString()
            });
        }
        public async Task<CustomerCinemaPageViewModel> PrepareCinemaViewModelAsync(string userEmail, string cinemaId)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            var cinema = await _context.Cinemas.Include(i => i.Movies).ThenInclude(i => i.Movie).ThenInclude(i => i.Genre).FirstOrDefaultAsync(i => i.Id == int.Parse(cinemaId));
            var dates = Enumerable.Range(0, 7).Select(i => DateTime.Now.AddDays(i))
                .ToDictionary(key =>
                {
                    return key.Date != DateTime.Now.Date ? key.DayOfWeek.ToString().Substring(0, 3) : "Today";
                }, value => value.ToString(Constants.DateTimeFormat));
            return new CustomerCinemaPageViewModel
            {
                Id = cinema.Id,
                CinemaLogoUrl = cinema.ImageUrl,
                ProfilePictureUrl = user.ProfilePictureUrl,
                Dates = dates,
                AccentColor = cinema.AccentColor,
                BackgroundColor = cinema.BackgroundColor,
                BoardColor = cinema.BoardColor,
                ButtonBackgroundColor = cinema.ButtonBackgroundColor,
                ButtonTextColor = cinema.ButtonTextColor,
                TextColor = cinema.TextColor,
                Movies = cinema.Movies.Select(i => new CinemaMovieViewModel
                {
                    Id = i.MovieId,
                    Genre = i.Movie.Genre.Name,
                    TrailerId = Regex.Match(i.Movie.TrailerUrl, Constants.TrailerUrlRegex).Groups[3].Value,
                    Name = i.Movie.Title,
                    RunningTime = i.Movie.RunningTime.ToString(),
                    ImageUrl = i.Movie.ImageUrl,
                    Schedule = { }
                })
            };
        }
        public async Task<IEnumerable<CinemaMovieViewModel>> GetMoviesByDateAsync(string cinemaId, string date)
        {
            var cinema = await _context.Cinemas.Include(i => i.Schedule).Include(i => i.Movies).ThenInclude(i => i.Movie).ThenInclude(i => i.Genre).FirstOrDefaultAsync(i => i.Id == int.Parse(cinemaId));

            var movies = cinema.Movies;
            DateTime? convertedDate = null;
            if (string.IsNullOrEmpty(date) == false)
            {
                convertedDate = DateTime.ParseExact(date, Constants.DateTimeFormat, CultureInfo.InvariantCulture);
                movies = movies.Where(i => i.FromDate <= convertedDate && i.ToDate >= convertedDate).ToList();
            }
            else
            {
                convertedDate = DateTime.Today;
            }
            return movies.Select(i => new CinemaMovieViewModel
            {
                Id = i.MovieId,
                CinemaId = i.CinemaId,
                Genre = i.Movie.Genre.Name,
                Name = i.Movie.Title,
                TrailerId = Regex.Match(i.Movie.TrailerUrl, Constants.TrailerUrlRegex).Groups[3].Value,
                RunningTime = i.Movie.RunningTime.ToString(),
                ImageUrl = i.Movie.ImageUrl,
                Schedule = cinema.Schedule.Where(k => k.CinemaId == cinema.Id && k.MovieId == i.MovieId && k.ForDateTime.Date == convertedDate.Value.Date)
                .Select(t => t.ForDateTime)
                    .ToList(),
            });
        }
        public async Task<BuyTicketViewModel> GetBuyTicketViewModelAsync(int cinemaId, int movieId, string forDate)
        {
            var cinema = await _context.Cinemas.FirstOrDefaultAsync(i => i.Id == cinemaId);
            var movie = await _context.Movies.Include(i => i.Genre).FirstOrDefaultAsync(i => i.Id == movieId);
            DateTime date = DateTime.Parse(forDate);
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
                Time = forDate,
                Title = movie.Title,
                ForDateTime = date
            };
        }
        public async Task BuyTicketAsync(int sectorId, int movieId, SectorDetailsViewModel viewModel, DateTime forDate, string userEmail)
        {
            var sector = await _context.Sectors.FirstOrDefaultAsync(i => i.Id == sectorId);
            var cinemaMovie = await _context.CinemasMovies.FirstOrDefaultAsync(i => i.CinemaId == sector.CinemaId && i.MovieId == movieId);
            var selectedSeats = viewModel.Seats.SelectMany(i => i).Where(i => i.IsChecked && i.IsOccupied == false);
            foreach (var seat in selectedSeats)
            {
                Ticket ticket = new Ticket
                {
                    CinemaId = sector.CinemaId,
                    SerialNumber = $"R{seat.Row}C{seat.Col}",
                    ForDate = forDate,
                    MovieId = movieId,
                    SectorId = sectorId,
                    CustomerId = (await _userManager.FindByEmailAsync(userEmail)).Id,
                    Price = cinemaMovie.TicketPrice
                };
                _context.Tickets.Add(ticket);
            }
            await _context.SaveChangesAsync();
        }
    }
}
