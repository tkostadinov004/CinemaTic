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
        [Required(ErrorMessage ="Въведете име на английски!")]
        [Display(Name = "Име на филма (на английски)")]
        public string EnglishTitle { get; set; }
        [Required(ErrorMessage = "Въведете име на български!")]
        [Display(Name = "Име на филма (на български)")]
        public string BulgarianTitle { get; set; }
        [Required(ErrorMessage = "Изберете жанр!")]
        [Display(Name = "Жанр")]
        public int GenreId { get; set; }
        [Display(Name = "Снимка")]
        public string? ImageUrl { get; set; }
        [Required(ErrorMessage = "Въведете кратко описание")]
        [Display(Name = "Кратко описание")]
        public string Description { get; set; }
        public decimal? UserRating { get; set; }
        public int RatingCount { get; set; }
        [Required(ErrorMessage = "Въведете дата!")]
        [Display(Name = "Дата")]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Въведете цена!")]
        [Range(1, double.MaxValue, ErrorMessage = "Цената трябва да бъде повече от 1 лев!")]
        [Display(Name = "Цена")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Въведете времетраене!")]
        [Range(1, double.MaxValue, ErrorMessage = "Филмът трябва да бъде поне 1 минута!")]
        [Display(Name = "Времетраене")]
        public int RunningTime { get; set; }
        [Required(ErrorMessage = "Въведете линк за трейлър!")]
        [Display(Name = "Линк към трейлъра")]
        public string TrailerUrl { get; set; }
        [Display(Name = "Жанр")]
        public virtual Genre Genre { get; set; }
        [Display(Name = "Актьори")]
        public virtual ICollection<ActorMovie> Actors { get; set; }
    }
}
