using Cinema.Data.Models;
using Cinema.ViewModels.Cinemas;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.ViewModels.Movies
{
    [NotMapped]
    public class MovieDetailsViewModel
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string? ImageUrl { get; set; }
        public string Description { get; set; }
        public string Director { get; set; }
        public string? Date { get; set; }
        public int RunningTime { get; set; }
        public string TrailerId { get; set; }
        public Genre? Genre { get; set; }
        public decimal AverageRating { get; set; }
        public int RatingCount { get; set; }
        public decimal? CurrentUserRating { get; set; }
        public List<string>? Actors { get; set; }
        public List<CinemaCheckboxViewModel>? UserCinemas { get; set; }
        public int? GenreId { get; set; }
        public IEnumerable<SelectListItem>? Genres { get; set; }
        public IEnumerable<SelectListItem>? ActorsDropdown { get; set; }
        public IFormFile? Image { get; set; }
    }
}
