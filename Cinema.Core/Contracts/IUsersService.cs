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
        Task<ApplicationUser> GetByEmailAsync(string userEmail);
        Task<SidebarUserViewModel> GetSidebarViewModelAsync(string userEmail);
    }
}
