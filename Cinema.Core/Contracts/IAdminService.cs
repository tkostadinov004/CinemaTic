using Cinema.Data.Models;
using Cinema.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Core.Contracts
{
    public interface IAdminService
    {
        Task<IEnumerable<ApplicationUser>> GetAllAsync();
        Task DemoteUser(string id);
        Task DeleteAccount(string id);
        Task PromoteUser(string id);
        Task<ApplicationUser> FindById(string id);
    }
}
