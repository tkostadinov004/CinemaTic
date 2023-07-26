using Cinema.Data.Models;
using Cinema.ViewModels.Cinemas;
using Cinema.ViewModels.Customers;
using Cinema.ViewModels.Movies;
using Cinema.ViewModels.Sectors;
using Cinema.ViewModels.Tickets;
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
        Task<ChangePasswordViewModel> GetChangePasswordViewModelAsync(string userEmail);
        Task ChangePasswordAsync(ChangePasswordViewModel viewModel);
        Task<IEnumerable<CinemasViewModel>> GetCinemasAsync(bool? all, string userEmail);
        Task AddCinemaToFavoritesAsync(int cinemaId, string userEmail);
        Task RemoveCinemaFromFavoritesAsync(int cinemaId, string userEmail);
        Task<IEnumerable<CustomerTicketViewModel>> GetTicketsForCustomerAsync(string userEmail);
        Task<CustomerCinemaPageViewModel> PrepareCinemaViewModelAsync(string userEmail, string cinemaId);
        Task<IEnumerable<CinemaMovieViewModel>> GetMoviesByDateAsync(string cinemaId, string date);
        Task<BuyTicketViewModel> GetBuyTicketViewModelAsync(int cinemaId, int movieId, string time);
        Task BuyTicketAsync(int sectorId, int movieId, SectorDetailsViewModel viewModel, DateTime forDate, string userEmail);
    }
}
