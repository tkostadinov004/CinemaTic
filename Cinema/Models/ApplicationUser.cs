using Cinema.Models.Roles;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Tickets = new List<Ticket>();
        }

        [Required, MaxLength(100), RegularExpression("^[A-Za-z]+$")]
        public string FirstName { get; set; }
        [Required, MaxLength(100), RegularExpression("^[A-Za-z]+$")]
        public string LastName { get; set; }
        public Role? Role { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
