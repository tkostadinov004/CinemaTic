using Cinema.Core.Contracts;
using Cinema.Data;
using Cinema.Data.Models;
using Cinema.Utilities;
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
            return await _context.Tickets.Include(t => t.Customer).Include(t => t.Movie).Include(t => t.Seat).ToListAsync();
        }
        public async Task<IEnumerable<Ticket>> GetTicketsByUserAsync(string userEmail)
        {
            var tickets = await this.GetAllAsync();
            return tickets.Where(i => i.Customer.Email == userEmail);
        }

        private Tuple<int, int> GetSeatCoordinates(string seatNumber)
        {
            int row = int.Parse(seatNumber[1..3]);
            int col = int.Parse(seatNumber[4..]);

            return new Tuple<int, int>(row, col);
        }
        private bool CheckPlace(int row, int col, int movieId, DateTime forDate)
        {
            return _context.Tickets.Include(k => k.Movie).Include(k => k.Seat).ToList().Where(i => i.Movie.Id == movieId).Select(i => new { Coords = this.GetSeatCoordinates(i.Seat.SeatNumber), ForDate = i.ForDate }).Any(i => i.Coords.Item1 == row && i.Coords.Item2 == col && i.ForDate == forDate);
        }

        public async Task<BuyTicketViewModel> GetPurchaseViewModelAsync(int movieId, string? sector, DateTime forDate)
        {
            bool[,] isOccupied = new bool[Constants.HallRows, Constants.HallCols];
            for (int i = 0; i < Constants.HallRows; i++)
            {
                for (int j = 0; j < Constants.HallCols; j++)
                {
                    isOccupied[i, j] = CheckPlace(i + 1, j + 1, movieId, forDate);
                }
            }
            var viewModel = new BuyTicketViewModel
            {
                Movie = await _context.Movies.FirstOrDefaultAsync(i => i.Id == movieId),
                Sector = sector,
                Occupied = isOccupied,
                ForDate = forDate
            };
            if (string.IsNullOrEmpty(sector) == false)
            {
                viewModel.StartingRow = Constants.SectorBorderValues[sector].Item1.Row;
                viewModel.StartingCol = Constants.SectorBorderValues[sector].Item1.Col;

                viewModel.EndingRow = Constants.SectorBorderValues[sector].Item2.Row;
                viewModel.EndingCol = Constants.SectorBorderValues[sector].Item2.Col;
            }
            return viewModel;
        }

        public async Task BuyTicket(string seatCoords, int movieId, DateTime forDate, string? userEmail)
        {
            int[] seatCoordsInt = seatCoords.Split().Select(int.Parse).Select(i => i + 1).ToArray();
            //$"R{seatCoordsInt[0].ToString("D2")}C{seatCoordsInt[1].ToString("D2")}"
            string seatNumber = $"R{seatCoordsInt[0].ToString("D2")}C{seatCoordsInt[1].ToString("D2")}";
            var ticketSeat = new Seat()
            {
                SeatNumber =seatNumber,
                Sector = this.DefineSector(seatNumber),
                Price = this.CalculatePrice(seatNumber),
                IsOccupied = true,
            };
            var movie = await _context.Movies.FindAsync(movieId);
            var ticket = new Ticket
            {
                ForDate = forDate,
                Movie = movie,
                Customer = userEmail == null ? null : await _userManager.FindByEmailAsync(userEmail)
            };
            ticket.Seat = ticketSeat;
            //ticket.Price = ticket.Seat.Price + movie.Price;

            _context.Add(ticket);
            await _context.SaveChangesAsync();
        }
        private string DefineSector(string seatNumber)
        {
            int row = int.Parse(seatNumber[1..3]);
            int col = int.Parse(seatNumber[4..]);

            string sector = "";
            if (row < Constants.HallRows / 2)
            {
                if (col <= Constants.HallCols / 3)
                {
                    sector = "A";
                }
                else if (col > Constants.HallCols / 3 && col <= (Constants.HallCols / 3) * 2)
                {
                    sector = "B";
                }
                else
                {
                    sector = "C";
                }
            }
            else
            {
                if (col <= Constants.HallCols / 3)
                {
                    sector = "D";
                }
                else if (col > Constants.HallCols / 3 && col <= (Constants.HallCols / 3) * 2)
                {
                    sector = "E";
                }
                else
                {
                    sector = "F";
                }
            }
            return sector;
        }
        private decimal CalculatePrice(string seatNumber)
        {
            decimal middlePrice = 5;
            decimal difference = 0.2m;

            int row = int.Parse(seatNumber[1..3]);
            int col = int.Parse(seatNumber[4..]);

            int middleRow = 11;
            int middleCol = 16;

            int rowDiff = Math.Abs(row - middleRow);
            int colDiff = Math.Abs(col - middleCol);

            decimal totalPrice = middlePrice - (difference * rowDiff) - (difference * colDiff);
            return totalPrice;
        }
    }
}