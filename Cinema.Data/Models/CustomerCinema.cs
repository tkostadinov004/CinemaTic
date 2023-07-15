using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Data.Models
{
    public class CustomerCinema
    {
        public string CustomerId { get; set; }
        public ApplicationUser Customer { get; set; }
        public int CinemaId { get; set; }
        public Cinema Cinema { get; set; }
    }
}
