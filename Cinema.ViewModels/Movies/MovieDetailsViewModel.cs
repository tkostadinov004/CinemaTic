using Cinema.Data.Models;
using Cinema.ViewModels.Contracts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Cinema.ViewModels.Movies
{
    [NotMapped]
    public class MovieDetailsViewModel : IViewModel
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string? ImageUrl { get; set; }
        public string Description { get; set; }
        public string Director { get; set; }
        public string Date { get; set; }
        public int RunningTime { get; set; }
        public string TrailerUrl { get; set; }
        public Genre Genre { get; set; }
        public decimal AverageRating { get; set; }
        public int RatingCount { get; set; }
        public decimal? CurrentUserRating { get; set; }
        public List<string> Actors { get; set; }
    }
}
