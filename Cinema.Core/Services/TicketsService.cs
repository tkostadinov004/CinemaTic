using Cinema.Core.Contracts;
using Cinema.Data;
using Cinema.Data.Models;
using Cinema.Core.Utilities;
using Cinema.ViewModels.Sectors;
using Cinema.ViewModels.Tickets;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient.Server;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Core.Services
{
    public class TicketsService : ITicketsService
    {
        private readonly CinemaDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public TicketsService(CinemaDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<bool> ExistsByIdAsync(int? id)
        {
            return await _context.Tickets.AnyAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Ticket>> GetAllAsync()
        {
            return await _context.Tickets.Include(t => t.Customer).Include(t => t.Movie).ToListAsync();
        }
        public async Task<IEnumerable<Ticket>> GetTicketsByUserAsync(string userEmail)
        {
            var tickets = await this.GetAllAsync();
            return tickets.Where(i => i.Customer.Email == userEmail);
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
                    Price = cinemaMovie.TicketPrice //change
                };
                _context.Tickets.Add(ticket);
            }
            await _context.SaveChangesAsync();
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
    }
}