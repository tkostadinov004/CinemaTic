using Cinema.Core.Contracts.Common;
using Cinema.Data.Models;
using Cinema.ViewModels.Cinemas;
using Cinema.ViewModels.Contracts;
using Cinema.ViewModels.Movies;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Core.Contracts
{
    public interface IOwnersService
    {
        Task AddAsync(IViewModel item, string userEmail);
        Task<IEnumerable<CinemaListViewModel>> GetAllByUserAsync(string userEmail);
        Task<CinemaDetailsViewModel> GetByIdAsync(int? id);
        Task<OwnerStatisticsViewModel> GetStatisticsAsync(string userEmail);
        Task AddMovieToCinemas(string[] cinemas, int movieId);
        Task EditCinema(IViewModel item);
        Task DeleteByIdAsync(int? id);
        Task<IEnumerable<CinemaListViewModel>> SearchAndFilterCinemasAsync(string searchText, string userEmail, string filterValue);
        Task<IEnumerable<MovieInfoCardViewModel>> SearchMoviesByCinema(string searchText, string cinemaId);
        Task<EditCinemaViewModel> GetEditViewModelByIdAsync(int cinemaId);
    }
}
