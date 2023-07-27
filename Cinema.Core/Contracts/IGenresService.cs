using Cinema.Core.Contracts.Common;
using Cinema.Data.Models;
using Cinema.ViewModels.Genres;
using Cinema.ViewModels.Movies;

namespace Cinema.Core.Contracts
{
    public interface IGenresService : ICinemaService<Genre>
    {
        Task CreateAsync(CreateGenreViewModel item);
        Task EditByIdAsync(EditGenreViewModel item);
        Task<IEnumerable<GenreListViewModel>> SortGenresAsync(string sortBy);
        Task<IEnumerable<MovieInfoCardViewModel>> SearchAndSortMoviesByGenre(string searchText, int genreId, string sortBy);
        Task<EditGenreViewModel> GetEditViewModelByIdAsync(int genreId);
        Task<DeleteGenreViewModel> PrepareDeleteViewModelAsync(int id);
        Task<GenreDetailsViewModel> PrepareDetailsViewModelAsync(int? id);
    }
}
