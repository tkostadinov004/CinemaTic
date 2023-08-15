using CinemaTic.Core.Contracts;
using CinemaTic.Core.DTOs.Charts;
using CinemaTic.Data;
using CinemaTic.Data.Enums;
using CinemaTic.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTic.Core.Services
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
        /// <summary>
        /// <para>Gets the sum of incomes of an <see cref="ApplicationUser"/>'s cinemas, as well as a sum of the incomes of all the cinemas, registered in CinemaTic.</para>
        /// <para>The data is used for showing the market share of an <see cref="ApplicationUser"/>.</para>
        /// </summary>
        /// <returns>A <see cref="CinemaShareDTO"/> object</returns>
        public async Task<CinemaShareDTO> GetMarketShareByUserAsync(string userEmail)
        {
            var tickets = await _context.Tickets.Include(i => i.Cinema).Select(i => new
            {
                CinemaId = i.CinemaId,
                Price = i.Price
            }).ToListAsync();
            var user = await _userManager.FindByEmailAsync(userEmail);
            var userCinemas = await _context.Cinemas.Where(i => i.OwnerId == user.Id).ToListAsync();
            return new CinemaShareDTO
            {
                PersonalIncome = tickets.Where(i => userCinemas.Any(c => c.Id == i.CinemaId)).Select(i => i.Price).Sum(),
                TotalIncome = tickets.Sum(i => i.Price)
            };
        }
        /// <summary>
        /// <para>Gets the incomes of an <see cref="ApplicationUser"/>'s cinemas</para>
        /// <para>The data is used for showing the total income of each cinema that an <see cref="ApplicationUser"/> owns.</para>
        /// </summary>
        /// <returns>A <see cref="TotalIncomesDTO"/> object</returns>
        public async Task<TotalIncomesDTO> GetTotalIncomesAsync(string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            var cinemasIncomes = (await _context.Tickets.Include(i => i.Cinema).Include(i => i.Cinema).Where(i => i.Cinema.OwnerId == user.Id).Select(i => new
            {
                CinemaId = i.CinemaId,
                Price = i.Price,
                Name = i.Cinema.Name
            }).ToListAsync()).GroupBy(i => i.Name).ToDictionary(key => key.Key, value => value.Select(i => i.Price).Sum());
            return new TotalIncomesDTO
            {
                Labels = cinemasIncomes.Keys.ToArray(),
                Incomes = cinemasIncomes.Values.ToArray()
            };
        }
        /// <summary>
        /// <para>Gets the amounts of customers of an <see cref="ApplicationUser"/>'s cinemas.</para>
        /// </summary>
        /// <returns>A <see cref="CustomersPerCinemaDTO"/> object</returns>
        public async Task<CustomersPerCinemaDTO> GetCustomersPerCinemaAsync(string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            var cinemasCustomers = (await _context.Cinemas.Include(i => i.Customers).Where(i => i.OwnerId == user.Id && i.ApprovalStatus == ApprovalStatus.Approved).Select(i => new
            {
                Name = i.Name,
                CustomersCount = i.Customers.Count
            }).ToListAsync());
            return new CustomersPerCinemaDTO
            {
                Labels = cinemasCustomers.Select(i => i.Name).ToArray(),
                CustomersCounts = cinemasCustomers.Select(i => i.CustomersCount).ToArray()
            };
        }
        /// <summary>
        /// <para>Gets the best selling movie of every <see cref="Cinema"/> that an <see cref="ApplicationUser"/> owns.</para>
        /// </summary>
        /// <returns>A <see cref="BestSellingMoviesPerCinemaDTO"/> object</returns>
        public async Task<BestSellingMoviesPerCinemaDTO> GetBestSellingMoviesPerCinemaAsync(string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);

            var movies = _context.Cinemas
                .Include(i => i.Movies)
                .ThenInclude(i => i.Movie)
                .ThenInclude(i => i.TicketsBought)
                .Where(i => i.OwnerId == user.Id && i.ApprovalStatus == ApprovalStatus.Approved)
                .Select(i => i.Movies.OrderByDescending(m => m.Movie.TicketsBought.Sum(t => t.Price)).FirstOrDefault().Movie.Title)
                .GroupBy(i => i);
            return new BestSellingMoviesPerCinemaDTO
            {
                Labels = movies.Select(i => i.Key ?? "None").ToArray(),
                MoviesCounts = movies.Select(i => i.Count()).ToArray()
            };
        }
        /// <summary>
        /// <para>Gets the amount of registered users per month of the current year.</para>
        /// </summary>
        /// <returns>A <see cref="UsersPerMonthDTO"/> object</returns>
        public async Task<UsersPerMonthDTO> GetRegisteredUsersByMonthAsync()
        {
            var months = Enumerable.Range(1, DateTime.Now.Month);

            var users = months.ToDictionary(key => key, value => _context.Users.Where(u => u.CreationDate.Month == value).Count());
            return new UsersPerMonthDTO
            {
                Labels = months.Select(month => DateTimeFormatInfo.CurrentInfo.GetMonthName(month)).ToArray(),
                UsersCounts = users.Select(i => i.Value).ToArray()
            };
        }
        /// <summary>
        /// <para>Gets the accumulated amount of registered users per month of the current year.</para>
        /// </summary>
        /// <returns>A <see cref="UsersGrowthDTO"/> object</returns>
        public async Task<UsersGrowthDTO> GetUsersGrowthAsync()
        {
            var months = Enumerable.Range(1, DateTime.Now.Month);
            var users = months.ToDictionary(key => key, value => _context.Users.Where(u => u.CreationDate.Month <= value).Count());
            return new UsersGrowthDTO
            {
                Labels = months.Select(month => DateTimeFormatInfo.CurrentInfo.GetMonthName(month)).ToArray(),
                UsersCounts = users.Select(i => i.Value).ToArray()
            };
        }
    }
}
