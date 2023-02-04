using Cinema.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Username { get; set; }
        [Required, MaxLength(500)]
        public string Password { get; set; }
        [Required, MaxLength(100), RegularExpression("^[A-Za-z]+$")]
        public string FirstName { get; set; }
        [Required, MaxLength(100), RegularExpression("^[A-Za-z]+$")]
        public string LastName { get; set; }

        [NotMapped]
        public virtual IUserRole Role { get; set; }
    }
}
