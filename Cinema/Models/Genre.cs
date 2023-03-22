using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Models
{
    public class Genre
    {
        public Genre()
        {
            Movies = new List<Movie>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Име на английски")]
        public string EnglishName { get; set; }
        [Required]
        [Display(Name = "Име на български")]
        public string BulgarianName { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
    }
}
