using Cinema.Data;
using Cinema.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public AdminController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Users.ToListAsync().Result.Where(i => _userManager.IsInRoleAsync(i, "Owner").Result == true));
        }

        [HttpGet]
        public IActionResult DeleteOwner(string ownerId)
        {
            var owner = _context.Users.ToListAsync().Result.FirstOrDefault(i => i.Id == ownerId &&  _userManager.IsInRoleAsync(i, "Owner").Result == true);
            if (owner == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(owner);
        }
        [HttpPost, ActionName("DeleteOwner")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var owner = _context.Users.ToListAsync().Result.FirstOrDefault(i => i.Id == id);
            _context.Users.Remove(owner);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
