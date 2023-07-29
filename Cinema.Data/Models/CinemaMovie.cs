using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Data.Models
{
    public class CinemaMovie
    {
        public CinemaMovie()
        {
            
        }
        public CinemaMovie(int cinemaId, int movieId, DateTime fromDate, decimal ticketPrice, DateTime toDate)
        {
            CinemaId = cinemaId;
            MovieId = movieId;
            FromDate = fromDate;
            TicketPrice = ticketPrice;
            ToDate = toDate;
        }
        public int CinemaId { get; set; }
        public virtual Cinema Cinema { get; set; }
        public int MovieId { get; set; }
        public virtual Movie Movie { get; set; }
        public decimal TicketPrice { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
