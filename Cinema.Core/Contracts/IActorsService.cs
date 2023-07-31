using Cinema.Core.Utilities;
using Cinema.Data.Models;
using Cinema.ViewModels.Actors;
using Cinema.ViewModels.Movies;

namespace Cinema.Core.Contracts
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
        Task<IEnumerable<MovieInfoCardViewModel>> SearchMoviesByActorAsync(string searchText, int? actorId); 
    }
}
