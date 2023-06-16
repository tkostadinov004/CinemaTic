using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Data.Models
{
    public class Seat
    {
        [Key]
        public int Id { get; set; }
        public string SeatNumber { get; set; }
        public decimal Price {get; set;}
        public string Sector { get; set; }
        public bool IsOccupied { get; set; } = false;

    }
}
