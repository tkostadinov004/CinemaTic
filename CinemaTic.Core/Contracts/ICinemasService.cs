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
    public interface ICinemasService
    {
        Task CreateCinemaAsync(CreateCinemaViewModel item, string userEmail);
        Task<IEnumerable<CinemaListViewModel>> GetAllByUserAsync(string userEmail);
        Task<CinemaDetailsViewModel> GetDetailsViewModelByIdAsync(int? id);
        Task EditCinemaAsync(EditCinemaViewModel item);
        Task DeleteByIdAsync(int? id);
        Task<bool> ExistsByIdAsync(int? id);
        Task<bool> OwnerHasCinemaAsync(int? cinemaId, string userEmail);
        Task<IEnumerable<CinemaListViewModel>> QueryCinemasAsync(string searchText, string filterValue, string sortBy, string userEmail);
        Task<IEnumerable<MovieInfoCardViewModel>> QueryMoviesByCinemaAsync(int? cinemaId, string searchText, string sortBy);
        Task<EditCinemaViewModel> GetEditViewModelByIdAsync(int? cinemaId);
        Task<DeleteCinemaViewModel> GetDeleteViewModelAsync(int? id);
        Task<CinemaPagePreviewViewModel> GetPreviewViewModelAsync(string userEmail, int? cinemaId);
        Task<IEnumerable<CinemaContainingMovieViewModel>> QueryCinemasContainingMovieAsync(int? movieId, string userEmail, string sortBy);
    }
}
