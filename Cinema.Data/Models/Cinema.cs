using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Data.Models
{
    public class Cinema
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime FoundedOn { get; set; }
        public int SeatRows { get; set; }
        public int SeatCols { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        public bool IsApproved { get; set; } = false;
        [Required]
        public string OwnerId { get; set; }
        public virtual ApplicationUser Owner { get; set; }
        public virtual ICollection<CinemaMovie> Movies { get; set; } = new List<CinemaMovie>();
        public virtual ICollection<VisitorCinema> Visitors { get; set; } = new List<VisitorCinema>();
        public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
