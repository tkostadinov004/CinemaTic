using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cinema.Data;
using Cinema.Models;
using Microsoft.AspNetCore.Identity;
using System.Drawing;
using Microsoft.AspNetCore.Authorization;
using Cinema.Models.ViewModels;
using Cinema.Utilities;

namespace Cinema.Controllers
{
    public class TicketsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public TicketsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Tickets
        public async Task<IActionResult> Index()
        {
            var tickets = _context.Tickets.Include(t => t.Visitor).Include(t => t.Movie).Include(t => t.Seat);
            if (!User.IsInRole("Owner"))
            {
                return View(await tickets.Where(i => i.Visitor.Email == User.Identity.Name).ToListAsync());
            }
            return View(await tickets.ToListAsync());
        }
        private Tuple<int, int> SeatCoords(string seatNumber)
        {
            int row = int.Parse(seatNumber[1..3]);
            int col = int.Parse(seatNumber[4..]);

            return new Tuple<int, int>(row, col);
        }
        // GET: Tickets/Create
        [Authorize(Roles = "Visitor")]
        [AllowAnonymous]
        public IActionResult Create(int movieId, string? sector, DateTime forDate)
        {
            bool CheckPlace(int row, int col)
            {
                return _context.Tickets.Include(k => k.Movie).Include(k => k.Seat).ToList().Where(i => i.Movie.Id == movieId).Select(i => new { Coords = SeatCoords(i.Seat.SeatNumber), ForDate = i.ForDate }).Any(i => i.Coords.Item1 == row && i.Coords.Item2 == col && i.ForDate == forDate);
            }

            //ViewData["Colors"] = colors;
            //var movie = _context.Movies.FirstOrDefault(i => i.Id == movieId);
            //return View(movie);
            bool[,] isOccupied = new bool[Constants.HallRows, Constants.HallCols];
            for (int i = 0; i < Constants.HallRows; i++)
            {
                for (int j = 0; j < Constants.HallCols; j++)
                {
                    isOccupied[i, j] = CheckPlace(i + 1, j + 1);
                }
            }
            var vm = new BuyTicketViewModel
            {
                Movie = _context.Movies.FirstOrDefault(i => i.Id == movieId),
                Sector = sector,
               // Seats = _context.Seats.ToList().Where(i => i.Sector == sector).ToList(),
               Occupied = isOccupied,
               ForDate = forDate
            };
            if (string.IsNullOrEmpty(sector) == false)
            {
                vm.StartingRow = Constants.SectorBorderValues[sector].Item1.Row;
                vm.StartingCol = Constants.SectorBorderValues[sector].Item1.Col;

                vm.EndingRow = Constants.SectorBorderValues[sector].Item2.Row;
                vm.EndingCol = Constants.SectorBorderValues[sector].Item2.Col;
            }
            return View(vm);
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Visitor")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        //[Bind("Id,SerialNumber,MovieId,SeatId,Price")] Ticket ticket
        //[Bind("Movie, MovieId")] BuyTicketViewModel vm
        public async Task<IActionResult> Create(string seatCoords, int movieId, DateTime forDate)
        {
            int[] seatCoordsInt = seatCoords.Split().Select(int.Parse).Select(i => i + 1).ToArray();

            var ticketSeat = new Seat($"R{seatCoordsInt[0].ToString("D2")}C{seatCoordsInt[1].ToString("D2")}")
            {
                IsOccupied = true
            };
            var movie = await _context.Movies.FindAsync(movieId);
            var ticket = new Ticket
            {
                ForDate = forDate,
                Movie = movie,
                Visitor = User.Identity.Name == null ? null : await _userManager.FindByEmailAsync(User.Identity.Name)
            };
            ticket.Seat = ticketSeat;
            ticket.Price = ticket.Seat.Price + movie.Price;

            if (ModelState.IsValid)
            {
                _context.Add(ticket);
                await _context.SaveChangesAsync();
                if (User.Identity.Name == null)
                {
                    return RedirectToAction(nameof(Index), "Home");
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Title", ticket.MovieId);
            ViewData["SeatId"] = new SelectList(_context.Seats, "Id", "Id", ticket.SeatId);
            return View(ticket);
        }
        private bool TicketExists(int id)
        {
            return _context.Tickets.Any(e => e.Id == id);
        }
    }
}
