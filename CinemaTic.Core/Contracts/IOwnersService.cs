using CinemaTic.Core.Utilities;
using CinemaTic.Data.Enums;
using CinemaTic.Data.Models;
using CinemaTic.ViewModels.Actors;
using CinemaTic.ViewModels.Cinemas;
using CinemaTic.ViewModels.Movies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;

namespace CinemaTic.Core.Contracts
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
        Task<IEnumerable<MovieInfoCardViewModel>> SearchAndSortMoviesByCinemaAsync(string searchText, string sortBy, int? cinemaId);
        Task<EditCinemaViewModel> GetEditViewModelByIdAsync(int? cinemaId);
        Task<DeleteCinemaViewModel> PrepareDeleteViewModelAsync(int? id);
        Task<CinemaPagePreviewViewModel> GetPreviewViewModelAsync(string userEmail, int? cinemaId);
        Task<IEnumerable<CinemaContainingMovieViewModel>> GetCinemasContainingMovieAsync(int? movieId, string sortBy, string userEmail);
    }
}
