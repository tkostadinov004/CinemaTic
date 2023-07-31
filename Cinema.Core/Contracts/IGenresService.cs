using Cinema.Data.Models;
using Cinema.ViewModels.Genres;
using Cinema.ViewModels.Movies;

namespace Cinema.Core.Contracts
{
    public interface IGenresService
    {
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
