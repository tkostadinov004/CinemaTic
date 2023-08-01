using CinemaTic.Data.Models;
using CinemaTic.ViewModels.Cinemas;
using CinemaTic.ViewModels.Customers;
using CinemaTic.ViewModels.Movies;
using CinemaTic.ViewModels.Sectors;
using CinemaTic.ViewModels.Tickets;
using CinemaTic.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTic.Core.Contracts
{
    public interface ICustomersService
    {
        Task<CustomerHomePageViewModel> GetCinemasForUserAsync(string userEmail);
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
