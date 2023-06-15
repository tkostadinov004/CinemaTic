using Cinema.Data.Contracts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Data.Models
{
    public class ApplicationUser : IdentityUser, IApplicationUser
    {
        public ApplicationUser()
        {
            Tickets = new List<Ticket>();
        }
        [Display(Name = "First name")]
        [Required, MaxLength(100), RegularExpression("^[A-Za-z]+$")]
        public string FirstName { get; set; }
        [Display(Name = "Last name")]
        [Required, MaxLength(100), RegularExpression("^[A-Za-z]+$")]
        public string LastName { get; set ; }
        [Display(Name = "Tickets for: ")]
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
