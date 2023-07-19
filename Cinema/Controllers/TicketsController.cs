using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Cinema.Core.Contracts;
using Cinema.ViewModels.Tickets;
using Cinema.ViewModels.Sectors;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Cinema.Controllers
{
    public class TicketsController : Controller
    {
        private readonly ITicketsService _ticketsService;

        public TicketsController(ITicketsService ticketsService)
        {
            _ticketsService = ticketsService;
        }

        // GET: Tickets
        public async Task<IActionResult> Index()
        {
            var tickets = await _ticketsService.GetAllAsync();
            if (!User.IsInRole("Owner"))
            {
                return View(await _ticketsService.GetTicketsByUserAsync(User.Identity.Name));
            }
            return View(tickets);
        }
        // GET: Tickets/Create
        [Authorize(Roles = "Customer")]
        [AllowAnonymous]
        public async Task<IActionResult> Create(int movieId, string? sector, DateTime forDate)
        {
            //var viewModel = await _ticketsService.GetPurchaseViewModelAsync(movieId, sector, forDate);
            //return View(viewModel);
            return null;
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Customer")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string seatCoords, int movieId, DateTime forDate)
        {
            //await _ticketsService.BuyTicket(seatCoords, movieId, forDate, User.Identity.Name);
            if (User.Identity.Name == null)
            {
                return RedirectToAction(nameof(Index), "Home");
            }
            return RedirectToAction(nameof(Index));
            ////ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Title", ticket.MovieId);
            ////ViewData["SeatId"] = new SelectList(_context.Seats, "Id", "Id", ticket.SeatId);
            //return View(ticket);
        }
        [HttpPost]
        public async Task<IActionResult> BuyTicket(int sectorId, int movieId, SectorDetailsViewModel viewModel, DateTime forDate)
        {
            await _ticketsService.BuyTicketAsync(sectorId, movieId, viewModel, forDate, User.Identity.Name);
            return RedirectToAction("Cinema", "Owners", new { userEmail = User.Identity.Name, cinemaId = TempData["CinemaId"] });
        }
    }
}
