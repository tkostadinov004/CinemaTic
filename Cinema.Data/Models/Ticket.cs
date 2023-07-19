using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Data.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }
        public string SerialNumber { get; set; }
        public int MovieId { get; set; }
        public int CinemaId { get; set; }
        public int SectorId { get; set; }
        public string CustomerId { get; set; }
        public decimal Price { get; set; }
        public DateTime ForDate { get; set; }

        public virtual Sector Sector { get; set; }
        public virtual ApplicationUser Customer { get; set; }
        public virtual Cinema Cinema { get; set; }
        public virtual Movie Movie { get; set; }
    }
}
