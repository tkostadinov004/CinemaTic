using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }
        public string SerialNumber { get; set; }
        public int MovieId { get; set; }
        public int SeatId { get; set; }
        public decimal Price { get; set; }
        public DateTime ForDate { get; set; }

        public virtual Seat Seat { get; set; }
        public virtual ApplicationUser Visitor { get; set; }
        public virtual Movie Movie { get; set; }
    }
}
