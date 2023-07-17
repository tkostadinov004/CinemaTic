using Cinema.Core.Contracts.Common;
using Cinema.Data.Models;
using Cinema.ViewModels.Actors;
using Cinema.ViewModels.Cinemas;
using Cinema.ViewModels.Contracts;
using Cinema.ViewModels.Movies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Core.Contracts
{
    public interface IActorsService
    {
        Task<ActorDetailsViewModel> GetByIdAsync(int? id);
        Task<bool> ExistsByIdAsync(int? id);
        Task DeleteByIdAsync(int? id);
        Task CreateAsync(IViewModel item, string country);
        Task EditActorAsync(IViewModel item, int id, string country);
        Task<IEnumerable<ActorListViewModel>> SearchAndFilterActorsAsync(string searchText, string filterValue, string sortBy);
        Task<IEnumerable<MovieInfoCardViewModel>> SearchMoviesByActor(string searchText, string actorId);
        Task<EditActorViewModel> GetEditViewModelByIdAsync(int cinemaId);
        Task<DeleteActorViewModel> PrepareDeleteViewModelAsync(int id);
    }
}
