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
        Task<CustomerHomePageViewModel> GetCinemasForUserAsync(string userEmail);
        Task<ChangePasswordViewModel> GetChangePasswordViewModelAsync(string userEmail);
        Task<ChangeProfilePictureViewModel> GetChangeProfilePictureViewModelAsync(string userEmail);
        Task ChangeProfilePictureViewModelAsync(ChangeProfilePictureViewModel viewModel);
        Task ChangePasswordAsync(ChangePasswordViewModel viewModel);
        Task<IEnumerable<CinemasViewModel>> GetCinemasByUserAsync(bool? all, string userEmail);
        Task AddCinemaToFavoritesAsync(int? cinemaId, string userEmail);
        Task RemoveCinemaFromFavoritesAsync(int? cinemaId, string userEmail);
        Task SetRatingToMovieAsync(int? id, decimal rating, string userEmail);
        Task<IEnumerable<CustomerTicketViewModel>> GetTicketsForCustomerAsync(string userEmail);
        Task<CustomerCinemaPageViewModel> PrepareCinemaViewModelAsync(string userEmail, int? cinemaId);
        Task<IEnumerable<CinemaMovieViewModel>> GetMoviesByDateAsync(int? cinemaId, DateTime date);
        Task<BuyTicketViewModel> GetBuyTicketViewModelAsync(int? cinemaId, int? movieId, DateTime time);
        Task BuyTicketAsync(int? sectorId, int? movieId, SectorDetailsViewModel viewModel, DateTime forDate, string userEmail);
    }
}
