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
        Task<IEnumerable<MovieInfoCardViewModel>> GetAllMoviesAsync();
        Task AddMovieToCinemas(string[] cinemas, int movieId);
        Task EditCinema(IViewModel item);
        Task DeleteByIdAsync(int? id);
        Task<IEnumerable<CinemaListViewModel>> SearchCinemasAsync(string searchText, string userEmail);
        Task<IEnumerable<MovieInfoCardViewModel>> SearchMoviesByCinema(string searchText, string cinemaId);
        Task<EditCinemaViewModel> GetEditViewModelByIdAsync(int cinemaId);
    }
}
