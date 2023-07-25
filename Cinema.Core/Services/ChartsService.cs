using Cinema.Core.Contracts;
using Cinema.Data;
using Cinema.Data.Models;
using Cinema.ViewModels.Charts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Core.Services
{
    public class ChartsService : IChartsService
    {
        private readonly CinemaDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ChartsService(CinemaDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<CinemaShareViewModel> GetMarketShareByUserAsync(string userEmail)
        {
            var tickets = await _context.Tickets.Include(i => i.Cinema).Select(i => new
            {
                CinemaId = i.CinemaId,
                Price = i.Price
            }).ToListAsync();
            var user = await _userManager.FindByEmailAsync(userEmail);
            var userCinemas = await _context.Cinemas.Where(i => i.OwnerId == user.Id).ToListAsync();
            return new CinemaShareViewModel
            {
                PersonalIncome = tickets.Where(i => userCinemas.Any(c => c.Id == i.CinemaId)).Select(i => i.Price).Sum(),
                TotalIncome = tickets.Sum(i => i.Price)
            };
        }

        public async Task<TotalIncomesViewModel> GetTotalIncomesAsync(string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            var cinemasIncomes = (await _context.Tickets.Include(i => i.Cinema).Include(i => i.Cinema).Where(i => i.Cinema.OwnerId == user.Id).Select(i => new
            {
                CinemaId = i.CinemaId,
                Price = i.Price,
                Name = i.Cinema.Name
            }).ToListAsync()).GroupBy(i => i.Name).ToDictionary(key => key.Key, value => value.Select(i => i.Price).Sum());
            return new TotalIncomesViewModel
            {
                Labels = cinemasIncomes.Keys.ToArray(),
                Incomes = cinemasIncomes.Values.ToArray()
            };
        }

        public async Task<CustomersPerCinemaViewModel> GetCustomersPerCinemaAsync(string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            var cinemasCustomers = (await _context.Cinemas.Include(i => i.Customers).Where(i => i.OwnerId == user.Id).Select(i => new
            {
                Name = i.Name,
                CustomersCount = i.Customers.Count
            }).ToListAsync());
            return new CustomersPerCinemaViewModel
            {
                Labels = cinemasCustomers.Select(i => i.Name).ToArray(),
                CustomersCounts = cinemasCustomers.Select(i => i.CustomersCount).ToArray()
            };
        }

        public async Task<BestSellingMoviesPerCinemaViewModel> GetBestSellingMoviesPerCinemaAsync(string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);

            var movies = _context.Cinemas.Include(i => i.Movies).ThenInclude(i => i.Movie).ThenInclude(i => i.TicketsBought).Where(i => i.OwnerId == user.Id).Select(i => i.Movies.OrderByDescending(m => m.Movie.TicketsBought.Sum(t => t.Price)).FirstOrDefault().Movie.Title).GroupBy(i => i);
            return new BestSellingMoviesPerCinemaViewModel
            {
                Labels = movies.Select(i => i.Key ?? "None").ToArray(),
                MoviesCounts = movies.Select(i => i.Count()).ToArray()
            };
        }

        public async Task<UsersPerMonthViewModel> GetRegisteredUsersByMonthAsync()
        {
            var months = Enumerable.Range(1, DateTime.Now.Month);

            var users = months.ToDictionary(key => key, value => _context.Users.Where(u => u.CreationDate.Month == value).Count());
            return new UsersPerMonthViewModel
            {
                Labels = months.Select(month => DateTimeFormatInfo.CurrentInfo.GetMonthName(month)).ToArray(),
                UsersCounts = users.Select(i => i.Value).ToArray()
            };
        }

        public async Task<UsersGrowthViewModel> GetUsersGrowthAsync()
        {
            var months = Enumerable.Range(1, DateTime.Now.Month);
            var users = months.ToDictionary(key => key, value => _context.Users.Where(u => u.CreationDate.Month <= value).Count());
            return new UsersGrowthViewModel
            {
                Labels = months.Select(month => DateTimeFormatInfo.CurrentInfo.GetMonthName(month)).ToArray(),
                UsersCounts = users.Select(i => i.Value).ToArray()
            };
        }
    }
}
