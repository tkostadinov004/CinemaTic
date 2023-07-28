using Cinema.Core.Contracts;
using Cinema.Data;
using Cinema.Data.Models;
using Cinema.ViewModels.Users;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Core.Services
{
    public class UsersService : IUsersService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ApplicationUser> GetByEmailAsync(string userEmail)
        {
            return await _userManager.FindByEmailAsync(userEmail);
        }

        public async Task<SidebarUserViewModel> GetSidebarViewModelAsync(string userEmail)
        {
            var user = await this.GetByEmailAsync(userEmail);
            return new SidebarUserViewModel
            {
                Name = $"{user.FirstName} {user.LastName}",
                ImageUrl = user.ProfilePictureUrl
            };
        }
    }
}
