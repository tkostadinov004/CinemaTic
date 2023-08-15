using CinemaTic.Core.Utilities;
using CinemaTic.Data.Models;
using CinemaTic.Extensions;
using CinemaTic.ViewModels.Movies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CinemaTic.Core.Contracts
{
    public interface IMoviesService
    {
        Task<IEnumerable<Movie>> GetAllAsync();
        Task<Movie> GetByIdAsync(int? id);
        Task DeleteByIdAsync(int? id);
        Task<bool> ExistsByIdAsync(int? id);
        Task<MovieDetailsViewModel> GetDetailsViewModelAsync(int? id, string userEmail);
        Task<SelectList> GetGenresDropdownAsync();
        Task CreateMovieAsync(CreateMovieViewModel item, string userEmail);
        Task AddMovieToCinemasAsync(MovieDetailsViewModel viewModel);
        Task EditMovieAsync(EditMovieViewModel item);
        Task<CreateMovieViewModel> GetCreateViewModelAsync();
        Task<FilterMoviesViewModel> GetFilterViewModelAsync();
        Task<EditMovieViewModel> GetEditViewModelAsync(int? id);
        Task<DeleteMovieViewModel> GetDeleteViewModelAsync(int? id);
        Task<PaginatedList<MovieInfoCardViewModel>> QueryMoviesAsync(string searchText, string filterValue, string sortBy, int? pageNumber);
        Task<SetMovieScheduleViewModel> GetSetMovieScheduleViewModelAsync(int? cinemaId, int? movieId);
        Task<EditCinemaMovieDataViewModel> GetEditCinemaMovieDataViewModelAsync(int? cinemaId, int? movieId);
        Task SetMovieScheduleAsync(SetMovieScheduleViewModel viewModel);
        Task EditCinemaMovieDataAsync(EditCinemaMovieDataViewModel viewModel);
    }
}
