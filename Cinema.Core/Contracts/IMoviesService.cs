using Cinema.Core.Contracts.Common;
using Cinema.Data.Models;
using Cinema.ViewModels;
using Cinema.ViewModels.Contracts;
using Cinema.ViewModels.Movies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Core.Contracts
{
    public interface IMoviesService : ICinemaService<Movie>
    {
        Task<ApplicationUser> GetCurrentUserAsync(ControllerBase controllerBase);
        Task<IEnumerable<UserMovie>> GetRatingsByMovieIdAsync(int? id);
        Task<MovieDetailsViewModel> GetDetailsViewModel(Movie movie, IEnumerable<UserMovie> ratings, string userEmail);
        Task<SelectList> GetActorsDropDownAsync();
        Task<SelectList> GetGenresDropDownAsync();
        Task CreateMovieAsync(IViewModel item, IEnumerable<string> actors, int genreId);
        Task<string> UploadPhoto(IFormFile image);
        Task<EditMovieViewModel> GetEditViewModelById(int? id);
        Task EditByIdAsync(IViewModel item, int id, int genreId, IEnumerable<string> actors);
        Task SetRatingAsync(int id, decimal rating, string userEmail);
        Task<StatisticsViewModel> GetStatistics();
    }
}
