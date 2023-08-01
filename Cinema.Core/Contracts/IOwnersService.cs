using Cinema.Core.Utilities;
using Cinema.Data.Enums;
using Cinema.Data.Models;
using Cinema.ViewModels.Actors;
using Cinema.ViewModels.Cinemas;
using Cinema.ViewModels.Movies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;

namespace Cinema.Core.Contracts
{
    public interface IOwnersService
    {
        Task CreateCinemaAsync(CreateCinemaViewModel item, string userEmail);
        Task<IEnumerable<CinemaListViewModel>> GetAllByUserAsync(string userEmail);
        Task<CinemaDetailsViewModel> GetByIdAsync(int? id);
        Task EditCinemaAsync(EditCinemaViewModel item);
        Task DeleteByIdAsync(int? id);
        Task<bool> ExistsByIdAsync(int? id);
        Task<IEnumerable<CinemaListViewModel>> SearchAndFilterCinemasAsync(string searchText, string filterValue, string sortBy, string userEmail);
        Task<IEnumerable<MovieInfoCardViewModel>> SearchMoviesByCinemaAsync(string searchText, string sortBy, int? cinemaId);
        Task<EditCinemaViewModel> GetEditViewModelByIdAsync(int? cinemaId);
        Task<DeleteCinemaViewModel> PrepareDeleteViewModelAsync(int? id);
        Task<CinemaPagePreviewViewModel> GetPreviewViewModelAsync(string userEmail, int? cinemaId);
        Task<IEnumerable<CinemaContainingMovieViewModel>> GetCinemasContainingMovieAsync(int? movieId, string userEmail);
    }
}
