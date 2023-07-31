using Cinema.Data.Models;
using Cinema.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Core.Contracts
{
    public interface IUsersService
    {
        Task<ChangePasswordViewModel> GetChangePasswordViewModelAsync(string userEmail);
        Task ChangePasswordAsync(ChangePasswordViewModel viewModel);
        Task<ChangeProfilePictureViewModel> GetChangeProfilePictureViewModelAsync(string userEmail);
        Task ChangeProfilePictureViewModelAsync(ChangeProfilePictureViewModel viewModel);
        Task<ApplicationUser> GetUserByEmailAsync(string userEmail);
        Task<SidebarUserViewModel> GetSidebarViewModelByEmailAsync(string userEmail);
    }
}
