using Cinema.Models.Enums;
using Cinema.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Models
{
    public class ApplicationUser : IdentityUser, IApplicationUser
    {
        public ApplicationUser()
        {
            Tickets = new List<Ticket>();
        }
        [Display(Name = "Собствено име")]
        [Required, MaxLength(100), RegularExpression("^[A-Za-z]+$")]
        public string FirstName { get; set; }
        [Display(Name = "Фамилно име")]
        [Required, MaxLength(100), RegularExpression("^[A-Za-z]+$")]
        public string LastName { get; set ; }
        [Display(Name = "Билети за: ")]
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
