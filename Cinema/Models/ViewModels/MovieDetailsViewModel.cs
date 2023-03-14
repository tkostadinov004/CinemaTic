using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Models.ViewModels
{
    [NotMapped]
    public class MovieDetailsViewModel
    {
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public decimal AverageRating { get; set; }
        public int RatingCount { get; set; }
        public decimal? CurrentUserRating { get; set; }
    }
}
