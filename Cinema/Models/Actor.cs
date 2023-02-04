using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Models
{
    public class Actor
    {
        public Actor()
        {
            Movies = new List<ActorMovie>();
        }
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(100), RegularExpression("^[A-Za-z]+$")]
        public string FirstName { get; set; }
        [Required, MaxLength(100), RegularExpression("^[A-Za-z]+$")]
        public string LastName { get; set; }
        [Required]
        public DateTime Birthdate { get; set; }
        [Required]
        public string Nationality { get; set; }
        public decimal Rating { get; set; }
        [Required]
        public string ImageUrl { get; set; }

        public virtual ICollection<ActorMovie> Movies { get; set; }
    }
}
