using CinemaTic.Core.DTOs.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTic.Core.Contracts
{
    public interface IChartsService
    {
        // Owner charts
        Task<CinemaShareDTO> GetMarketShareByUserAsync(string userEmail);
        Task<TotalIncomesDTO> GetTotalIncomesAsync(string userEmail);
        Task<CustomersPerCinemaDTO> GetCustomersPerCinemaAsync(string userEmail);
        Task<BestSellingMoviesPerCinemaDTO> GetBestSellingMoviesPerCinemaAsync(string userEmail);
        // Admin charts
        Task<UsersPerMonthDTO> GetRegisteredUsersByMonthAsync();
        Task<UsersGrowthDTO> GetUsersGrowthAsync();
        // Customer charts
        Task<TicketsBoughtDTO> GetTicketsBoughtByCustomerAsync(string userEmail);
    }
}
