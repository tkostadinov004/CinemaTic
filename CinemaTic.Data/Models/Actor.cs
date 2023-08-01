﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaTic.Data.Models
{
    public class Actor
    {
        public Actor()
        {
            Movies = new List<ActorMovie>();
        }
        public Actor(int id, DateTime birthdate, string fullName, string? imageUrl, string nationality, decimal rating)
        {
            Id = id;
            Birthdate = birthdate;
            FullName = fullName;
            ImageUrl = imageUrl;
            Nationality = nationality;
            Rating = rating;
            Movies = new List<ActorMovie>();
        }

        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter a full name!"), MaxLength(100)]
        [Display(Name ="Full name")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Enter a birthdate!")]
        [Display(Name = "Birthdate")]
        public DateTime Birthdate { get; set; }
        [Display(Name = "Nationality")]
        public string Nationality { get; set; }
        [Required(ErrorMessage = "Enter a rating from IMDB!")]
        [Range(1, 10, ErrorMessage = "The rating should be in the range of 1 to 10")]
        [Display(Name = "IMDB rating")]
        public decimal Rating { get; set; }
        [Display(Name = "Image")]
        public string? ImageUrl { get; set; }
        [Display(Name = "Movies")]
        public virtual ICollection<ActorMovie> Movies { get; set; }
    }
}