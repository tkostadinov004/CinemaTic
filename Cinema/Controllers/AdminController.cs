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
            return View(_context.Users.ToListAsync().Result);
        }

        [HttpGet]
        public IActionResult DeleteFromOwnerRole(string ownerId)
        {
            var owner = _userManager.FindByIdAsync(ownerId).Result;
            if (owner == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(owner);
        }
        [HttpPost, ActionName("DeleteFromOwnerRole")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var owner = _userManager.FindByIdAsync(id).Result;
            if (_userManager.IsInRoleAsync(owner, "Owner").Result)
            {
                await _userManager.RemoveFromRoleAsync(owner, "Owner");
                await _userManager.AddToRoleAsync(owner, "Visitor");
            }
            
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> SetToOwner(string userId)
        {
            return View(await _userManager.FindByIdAsync(userId));
        }
        [HttpPost, ActionName("SetToOwner")]
        public async Task<IActionResult> SetToOwnerConfirmed(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (_userManager.IsInRoleAsync(user, "Owner").Result == false)
            {
                await _userManager.RemoveFromRoleAsync(user, "Visitor");
                await _userManager.AddToRoleAsync(user, "Owner");
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
