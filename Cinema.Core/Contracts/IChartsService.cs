using Cinema.ViewModels.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Core.Contracts
{
    public interface IChartsService
    {
        Task<CinemaShareViewModel> GetMarketShareByUserAsync(string userEmail);
        Task<TotalIncomesViewModel> GetTotalIncomesAsync(string userEmail);
        Task<CustomersPerCinemaViewModel> GetCustomersPerCinemaAsync(string userEmail);
        Task<BestSellingMoviesPerCinemaViewModel> GetBestSellingMoviesPerCinemaAsync(string userEmail);
        Task<UsersPerMonthViewModel> GetRegisteredUsersByMonthAsync();
        Task<UsersGrowthViewModel> GetUsersGrowthAsync();
    }
}
