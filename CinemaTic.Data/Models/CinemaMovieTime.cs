using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTic.Data.Models
{
    public class CinemaMovieTime
    {
        public CinemaMovieTime()
        {
            
        }
        public CinemaMovieTime(int id, int cinemaId, DateTime forDateTime, int movieId)
        {
            Id = id;
            CinemaId = cinemaId;
            ForDateTime = forDateTime;
            MovieId = movieId;
        }
        public int Id { get; set; }
        public int CinemaId { get; set; }
        public Cinema Cinema { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public DateTime ForDateTime { get; set; }
    }
}
