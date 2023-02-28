using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Models
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string EnglishName { get; set; }
        [Required]
        public string BulgarianName { get; set; }
    }
}
