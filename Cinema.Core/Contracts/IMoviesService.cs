using Cinema.Core.Utilities;
using Cinema.Data.Models;
using Cinema.ViewModels.Movies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cinema.Core.Contracts
{
    public interface IMoviesService
    {
        Task<IEnumerable<Movie>> GetAllAsync();
        Task<Movie> GetByIdAsync(int? id);
        Task DeleteByIdAsync(int? id);
        Task<bool> ExistsByIdAsync(int? id);
        Task<MovieDetailsViewModel> GetDetailsViewModelAsync(int? id, string userEmail);
        Task<SelectList> GetActorsDropdownAsync();
        Task<SelectList> GetGenresDropdownAsync();
        Task CreateMovieAsync(CreateMovieViewModel item, string userEmail);
        Task AddMovieToCinemasAsync(MovieDetailsViewModel viewModel);
        Task<string> UploadPhotoAsync(IFormFile image);
        Task EditMovieAsync(EditMovieViewModel item);
        Task<CreateMovieViewModel> GetCreateViewModelAsync();
        Task<FilterMoviesViewModel> GetFilterViewModelAsync();
        Task<EditMovieViewModel> GetEditViewModelAsync(int? id);
        Task<DeleteMovieViewModel> GetDeleteViewModelAsync(int? id);
        Task<PaginatedList<MovieInfoCardViewModel>> SearchAndFilterMoviesAsync(string searchText, string filterValue, string sortBy, int? pageNumber);
    }
}
