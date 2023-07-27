using Cinema.Core.Contracts.Common;
using Cinema.Data.Models;
using Cinema.ViewModels.Movies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cinema.Core.Contracts
{
    public interface IMoviesService : ICinemaService<Movie>
    {
        Task<ApplicationUser> GetCurrentUserAsync(ControllerBase controllerBase);
        Task<IEnumerable<UserMovie>> GetRatingsByMovieIdAsync(int? id);
        Task<MovieDetailsViewModel> GetDetailsViewModel(Movie movie, IEnumerable<UserMovie> ratings, string userEmail);
        Task<SelectList> GetActorsDropDownAsync();
        Task<SelectList> GetGenresDropDownAsync();
        Task CreateMovieAsync(CreateMovieViewModel item, string userEmail);
        Task<string> UploadPhoto(IFormFile image);
        Task EditByIdAsync(EditMovieViewModel item);
        Task<CreateMovieViewModel> PrepareForAddingAsync();
        Task<IEnumerable<MovieInfoCardViewModel>> GetAllMoviesAsync();
        Task<IEnumerable<MovieInfoCardViewModel>> SearchAndFilterMoviesAsync(string searchText, string filterValue, string sortBy);
        Task<FilterMoviesViewModel> PrepareFilterViewModelAsync();
        Task<EditMovieViewModel> PrepareForEditing(int? id);
        Task<DeleteMovieViewModel> PrepareForDeleting(int? id);
    }
}
