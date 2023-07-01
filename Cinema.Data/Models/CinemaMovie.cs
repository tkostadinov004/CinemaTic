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
        public Cinema Cinema { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}
