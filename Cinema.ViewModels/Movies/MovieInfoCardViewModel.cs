using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.ViewModels.Movies
{
    public class MovieInfoCardViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public decimal AverageRating { get; set; }
        public int RatingCount { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public string AddedBy { get; set; }
    }
}
