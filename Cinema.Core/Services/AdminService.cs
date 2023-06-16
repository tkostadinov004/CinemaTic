using Cinema.Data.Models;
using Cinema.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cinema.Core.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Core.Services
{
    public class AdminService : IAdminService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly CinemaDbContext _context;

        public AdminService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, CinemaDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }
        public async Task<IEnumerable<ApplicationUser>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task DeleteAccount(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
        }

        public async Task DemoteUser(string id)
        {
            var owner = await _userManager.FindByIdAsync(id);
            if (_userManager.IsInRoleAsync(owner, "Owner").Result)
            {
                await _userManager.RemoveFromRoleAsync(owner, "Owner");
                await _userManager.AddToRoleAsync(owner, "Visitor");
            }
        }

        public async Task<ApplicationUser> FindById(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task PromoteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (_userManager.IsInRoleAsync(user, "Owner").Result == false)
            {
                await _userManager.RemoveFromRoleAsync(user, "Visitor");
                await _userManager.AddToRoleAsync(user, "Owner");
            }
        }
    }
}
