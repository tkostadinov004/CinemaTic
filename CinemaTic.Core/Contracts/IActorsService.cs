using CinemaTic.Core.Utilities;
using CinemaTic.Data.Models;
using CinemaTic.Extensions;
using CinemaTic.ViewModels.Actors;
using CinemaTic.ViewModels.Movies;

namespace CinemaTic.Core.Contracts
{
    public interface IActorsService
    {
        Task CreateActorAsync(CreateActorViewModel item);
        Task<Actor> GetByIdAsync(int? id);
        Task<bool> ExistsByIdAsync(int? id);
        Task EditActorAsync(EditActorViewModel item);
        Task DeleteByIdAsync(int? id);
        Task<CreateActorViewModel> GetCreateViewModelAsync();
        Task<ActorDetailsViewModel> GetDetailsViewModelByIdAsync(int? id);
        Task<EditActorViewModel> GetEditViewModelByIdAsync(int? cinemaId);
        Task<DeleteActorViewModel> GetDeleteViewModelByIdAsync(int? id);
        Task<PaginatedList<ActorListViewModel>> SearchAndFilterActorsAsync(string searchText, string filterValue, string sortBy, int? pageNumber);
        Task<IEnumerable<MovieInfoCardViewModel>> SearchMoviesByActorAsync(string searchText, string sortBy, int? actorId);
    }
}
