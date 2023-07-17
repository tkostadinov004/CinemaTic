using Cinema.Data.Models;
using Cinema.ViewModels.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Core.Contracts
{
    public interface ICustomersService
    {
        Task<IEnumerable<ApplicationUser>> GetAllAsync();
        Task<CustomerHomePageViewModel> GetCinemasForUserAsync(string userEmail);
    }
}
