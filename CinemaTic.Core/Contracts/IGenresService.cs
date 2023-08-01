using CinemaTic.Data.Models;
using CinemaTic.ViewModels.Genres;
using CinemaTic.ViewModels.Movies;

namespace CinemaTic.Core.Contracts
{
    public interface IGenresService
    {
        Task<IEnumerable<Genre>> GetAllAsync();
        Task<Genre> GetByIdAsync(int? id);
        Task DeleteByIdAsync(int? id);
        Task<bool> ExistsByIdAsync(int? id);
        Task CreateAsync(CreateGenreViewModel item);
        Task EditByIdAsync(EditGenreViewModel item);
        Task<IEnumerable<GenreListViewModel>> SortGenresAsync(string sortBy);
        Task<IEnumerable<MovieInfoCardViewModel>> SearchAndSortMoviesByGenre(int? genreId, string searchText, string sortBy, int? pageNumber);
        Task<EditGenreViewModel> GetEditViewModelByIdAsync(int? genreId);
        Task<DeleteGenreViewModel> GetDeleteViewModelByIdAsync(int? id);
        Task<GenreDetailsViewModel> GetDetailsViewModelByIdAsync(int? id);
    }
}
