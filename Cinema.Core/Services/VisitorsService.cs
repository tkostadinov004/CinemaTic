using Cinema.Core.Contracts;
using Cinema.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Core.Services
{
    public class VisitorsService : IVisitorsService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public VisitorsService(UserManager<ApplicationUser> userManager)
        {
            this._userManager = userManager;
        }
        public async Task<IEnumerable<ApplicationUser>> GetAllAsync()
        {
            var users = await _userManager.Users
                .Include(i => i.Tickets).ThenInclude(i => i.Seat)
                .Include(i => i.Tickets).ThenInclude(ticket => ticket.Movie)
                .ToListAsync();
            return users.Where(i => this.IsVisitorAsync(i).Result);
        }
        private async Task<bool> IsVisitorAsync(ApplicationUser user)
        {
            return await _userManager.IsInRoleAsync(user, "Visitor");
        }
    }
}
