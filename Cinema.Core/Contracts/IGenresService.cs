using Cinema.Core.Contracts.Common;
using Cinema.Data.Models;
using Cinema.ViewModels.Actors;
using Cinema.ViewModels.Contracts;
using Cinema.ViewModels.Genres;
using Cinema.ViewModels.Movies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Core.Contracts
{
    public interface IGenresService : ICinemaService<Genre>
    {
        Task CreateAsync(IViewModel item);
        Task EditByIdAsync(IViewModel item, int id);
        Task<IEnumerable<GenreListViewModel>> SortGenresAsync(string sortBy);
        Task<IEnumerable<MovieInfoCardViewModel>> SearchAndSortMoviesByGenre(string searchText, string genreId, string sortBy);
        Task<EditGenreViewModel> GetEditViewModelByIdAsync(int genreId);
        Task<DeleteGenreViewModel> PrepareDeleteViewModelAsync(int id);
        Task<GenreDetailsViewModel> PrepareDetailsViewModelAsync(int? id);
    }
}
