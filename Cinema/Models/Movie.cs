using Cinema.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        public Movie()
        {
            Actors = new List<ActorMovie>();
        }
        [Required]
        public string Title { get; set; }
        public Genre Genre { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        [Range(1.0, 10.0)]
        public decimal UserRating { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
        public virtual ICollection<ActorMovie> Actors { get; set; }
    }
}
