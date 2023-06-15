using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Data.Models
{
    public class Sector
    {
        public Sector()
        {
            Seats = new List<Seat>();
        }

        [Key]
        public int Id { get; set; }
        public virtual ICollection<Seat> Seats { get; set; }
    }
}
