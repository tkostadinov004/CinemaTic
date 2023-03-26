using Cinema.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Controllers
{
    [Authorize(Roles = "Owner")]
    public class VisitorsController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;

        public VisitorsController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }
        
        public IActionResult Index()
        {
            IEnumerable<ApplicationUser> users = userManager.Users
                .Include(i => i.Tickets).ThenInclude(i => i.Seat)
                .Include(i => i.Tickets).ThenInclude(ticket => ticket.Movie)
                .ToList()
                .Where(i => userManager.IsInRoleAsync(i, "Visitor").Result);

            return View(users);
        }
    }
}
