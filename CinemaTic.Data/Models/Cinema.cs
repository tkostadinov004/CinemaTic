using CinemaTic.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTic.Data.Models
{
    public class Cinema
    {
        public Cinema()
        {

        }
        public Cinema(int id, string accentColor, ApprovalStatus approvalStatus, string backgroundColor, string boardColor, string buttonBackgroundColor, string buttonTextColor, string description, DateTime foundedOn, string imageUrl, string name, string ownerId, int seatCols, int seatRows, string textColor)
        {
            Id = id;
            AccentColor = accentColor;
            ApprovalStatus = approvalStatus;
            BackgroundColor = backgroundColor;
            BoardColor = boardColor;
            ButtonBackgroundColor = buttonBackgroundColor;
            ButtonTextColor = buttonTextColor;
            Description = description;
            FoundedOn = foundedOn;
            ImageUrl = imageUrl;
            Name = name;
            OwnerId = ownerId;
            SeatCols = seatCols;
            SeatRows = seatRows;
            TextColor = textColor;
        }
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
        public virtual ICollection<CustomerCinema> Customers { get; set; } = new List<CustomerCinema>();
        public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
        public virtual ICollection<Sector> Sectors { get; set; } = new List<Sector>();
        public virtual IEnumerable<CinemaMovieTime> Schedule { get; set; }
    }
}
