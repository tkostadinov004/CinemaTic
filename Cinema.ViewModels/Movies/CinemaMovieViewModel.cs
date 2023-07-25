using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.ViewModels.Movies
{
    public class CinemaMovieViewModel
    {
        public int Id { get; set; }
        public int CinemaId { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public string TrailerId { get; set; }
        public string RunningTime { get; set; }
        public string ImageUrl { get; set; }
        public List<DateTime> Schedule { get; set; }
    }
}
