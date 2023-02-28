﻿using Cinema.Models.Enums;
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
        public int GenreId { get; set; }
        public string ImageUrl { get; set; }
        [Display(Name = "Кратко описание")]
        public string Description { get; set; }
        [Range(1.0, 10.0)]
        public decimal? UserRating { get; set; }
        public int RatingCount { get; set; }
        [Display(Name = "Дата")]
        public DateTime Date { get; set; }
        [Display(Name = "Цена"), Range(1, double.MaxValue)]
        public decimal Price { get; set; }
        [Display(Name = "Жанр")]
        public virtual Genre Genre { get; set; }
        public virtual ICollection<ActorMovie> Actors { get; set; }
    }
}
