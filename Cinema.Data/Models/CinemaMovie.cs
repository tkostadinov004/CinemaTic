using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Data.Models
{
    public class CinemaMovie
    {
        public int CinemaId { get; set; }
        public virtual Cinema Cinema { get; set; }
        public int MovieId { get; set; }
        public virtual Movie Movie { get; set; }
        public decimal TicketPrice { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
