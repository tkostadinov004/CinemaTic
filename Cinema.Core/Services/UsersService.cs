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

        public async Task<ApplicationUser> GetUserByEmailAsync(string userEmail)
        {
            return await _userManager.FindByEmailAsync(userEmail);
        }

        public async Task<SidebarUserViewModel> GetSidebarViewModelByEmailAsync(string userEmail)
        {
            var user = await this.GetUserByEmailAsync(userEmail);
            if(user != null)
            {
                return new SidebarUserViewModel
                {
                    Name = $"{user.FirstName} {user.LastName}",
                    ImageUrl = user.ProfilePictureUrl
                };
            }
            return null;
        }
    }
}
