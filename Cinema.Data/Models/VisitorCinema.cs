using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Data.Models
{
    public class VisitorCinema
    {
        public string VisitorId { get; set; }
        public ApplicationUser Visitor { get; set; }
        public int CinemaId { get; set; }
        public Cinema Cinema { get; set; }
    }
}
