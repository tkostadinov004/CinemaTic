using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Data.Models
{
    [NotMapped]
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "First name")]
        [Required, MaxLength(100), RegularExpression("^[A-Za-z]+$")]
        public string FirstName { get; set; }
        [Display(Name = "Last name")]
        [Required, MaxLength(100), RegularExpression("^[A-Za-z]+$")]
        public string LastName { get; set ; }
        public string ProfilePictureUrl { get; set; }
        public virtual ICollection<Cinema> CinemasOwned { get; set; } = new List<Cinema>();
        public virtual ICollection<Movie> MoviesAdded { get; set; } = new List<Movie>();
        [Display(Name = "Tickets for: ")]
        public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
        public virtual ICollection<CustomerCinema> CinemasVisited { get; set; } = new List<CustomerCinema>();
        public virtual ICollection<UserAction> UserActions { get; set;} = new List<UserAction>();
    }
}
