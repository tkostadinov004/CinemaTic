using Cinema.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [Display(Name = "Име на филма")]
        public string Title { get; set; }
        [Display(Name = "Жанр")]
        public int GenreId { get; set; }
        [Display(Name = "Снимка")]
        public string? ImageUrl { get; set; }
        [Display(Name = "Кратко описание")]
        public string Description { get; set; }
        public decimal? UserRating { get; set; }
        public int RatingCount { get; set; }
        [Display(Name = "Дата")]
        public DateTime Date { get; set; }
        [Display(Name = "Цена")]
        public decimal Price { get; set; }
        [Display(Name = "Времетраене")]
        public int RunningTime { get; set; }
        [Display(Name = "Линк към трейлъра")]
        public string TrailerUrl { get; set; }
        [Display(Name = "Жанр")]
        public virtual Genre Genre { get; set; }
        [Display(Name = "Актьори")]
        public virtual ICollection<ActorMovie> Actors { get; set; }
    }
}
