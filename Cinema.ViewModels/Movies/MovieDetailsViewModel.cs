using Cinema.Data.Models;
using Cinema.ViewModels.Contracts;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.ViewModels.Movies
{
    [NotMapped]
    public class MovieDetailsViewModel : IViewModel
    {
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public decimal AverageRating { get; set; }
        public int RatingCount { get; set; }
        public decimal? CurrentUserRating { get; set; }
    }
}
