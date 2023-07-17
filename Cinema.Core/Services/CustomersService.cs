using Cinema.Core.Contracts;
using Cinema.Data;
using Cinema.Data.Models;
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

        public CustomersService(UserManager<ApplicationUser> userManager, CinemaDbContext context)
        {
            _userManager = userManager;
            _context = context;
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
                    Description=i.Cinema.Description,
                    FavoriteSince = "",
                    ImageUrl = i.Cinema.ImageUrl
                })
            };
        }

        private async Task<bool> IsCustomerAsync(ApplicationUser user)
        {
            return await _userManager.IsInRoleAsync(user, "Customer");
        }
    }
}
