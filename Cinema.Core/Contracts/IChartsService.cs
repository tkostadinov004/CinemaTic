using Cinema.Core.DTOs.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Core.Contracts
{
    public interface IChartsService
    {
        Task<CinemaShareDTO> GetMarketShareByUserAsync(string userEmail);
        Task<TotalIncomesDTO> GetTotalIncomesAsync(string userEmail);
        Task<CustomersPerCinemaDTO> GetCustomersPerCinemaAsync(string userEmail);
        Task<BestSellingMoviesPerCinemaDTO> GetBestSellingMoviesPerCinemaAsync(string userEmail);
        Task<UsersPerMonthDTO> GetRegisteredUsersByMonthAsync();
        Task<UsersGrowthDTO> GetUsersGrowthAsync();
    }
}
