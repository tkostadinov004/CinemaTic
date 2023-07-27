using Cinema.ViewModels.Actors;
using Cinema.ViewModels.Movies;

namespace Cinema.Core.Contracts
{
    public interface IActorsService
    {
        Task<ActorDetailsViewModel> GetByIdAsync(int? id);
        Task<bool> ExistsByIdAsync(int? id);
        Task DeleteByIdAsync(int? id);
        Task AddActorAsync(CreateActorViewModel item);
        Task EditActorAsync(EditActorViewModel item);
        Task<IEnumerable<ActorListViewModel>> SearchAndFilterActorsAsync(string searchText, string filterValue, string sortBy);
        Task<IEnumerable<MovieInfoCardViewModel>> SearchMoviesByActor(string searchText, int actorId);
        Task<EditActorViewModel> GetEditViewModelByIdAsync(int cinemaId);
        Task<DeleteActorViewModel> PrepareDeleteViewModelAsync(int id);
        Task<CreateActorViewModel> PrepareForAddingAsync();
    }
}
