using Cinema.Data.Enums;
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
        public ApprovalStatus ApprovalStatus { get; set; } = ApprovalStatus.PendingApproval;
        public string BackgroundColor { get; set; }
        public string BoardColor { get; set; }
        public string TextColor { get; set; }
        public string ButtonBackgroundColor { get; set; }
        public string ButtonTextColor { get; set; }
        public string AccentColor { get; set; }
        [Required]
        public string OwnerId { get; set; }
        public virtual ApplicationUser Owner { get; set; }
        public virtual ICollection<CinemaMovie> Movies { get; set; } = new List<CinemaMovie>();
        public virtual ICollection<VisitorCinema> Visitors { get; set; } = new List<VisitorCinema>();
        public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
        public virtual ICollection<Sector> Sectors { get; set; } = new List<Sector>();
    }
}
