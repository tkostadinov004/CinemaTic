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
    }
}