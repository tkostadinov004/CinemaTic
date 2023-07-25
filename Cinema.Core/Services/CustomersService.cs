using Cinema.Core.Contracts;
using Cinema.Core.Utilities;
using Cinema.Data;
using Cinema.Data.Models;
using Cinema.ViewModels.Contracts;
using Cinema.ViewModels.Customers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
