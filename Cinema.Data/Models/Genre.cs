using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Data.Models
{
    public class Genre
    {
        public Genre()
        {
            Movies = new List<Movie>();
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter a genre name!")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
    }
}
