﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Data.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        public Movie()
        {
            Actors = new List<ActorMovie>();
        }
        public Movie(int id, string addedById, string description, string director, int genreId, string imageUrl, int ratingCount, int runningTime, string title, string trailerUrl, decimal userRating)
        {
            Id = id;
            AddedById = addedById;
            Description = description;
            Director = director;
            GenreId = genreId;
            ImageUrl = imageUrl;
            RatingCount = ratingCount;
            RunningTime = runningTime;
            Title = title;
            TrailerUrl = trailerUrl;
            UserRating = userRating;
            Actors = new List<ActorMovie>();
        }
        [Required(ErrorMessage ="Enter a title!")]
        [Display(Name = "Movie title")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Choose a genre!")]
        [Display(Name = "Genre")]
        public int GenreId { get; set; }
        [Display(Name = "Image")]
        public string? ImageUrl { get; set; }
        [Required(ErrorMessage = "Enter a brief plot description!")]
        [Display(Name = "Plot description")]
        public string Description { get; set; }
        public decimal? UserRating { get; set; }
        public int RatingCount { get; set; }
        [Required(ErrorMessage = "Enter a movie duration!")]
        [Range(1, double.MaxValue, ErrorMessage = "The movie should be at least 1 minute long!")]
        [Display(Name = "Duration")]
        public int RunningTime { get; set; }
        [Required(ErrorMessage = "Enter a trailer url!")]
        [Display(Name = "Trailer URL")]
        public string TrailerUrl { get; set; }
        [Required(ErrorMessage = "Enter a director!")]
        public string Director { get; set; }
        public string AddedById { get; set; }
        public virtual ApplicationUser AddedBy { get; set; }
        [Display(Name = "Genre")]
        public virtual Genre Genre { get; set; }
        [Display(Name = "Actors")]
        public virtual ICollection<ActorMovie> Actors { get; set; }
        public virtual ICollection<CinemaMovie> Cinemas { get; set; } = new List<CinemaMovie>();
        public virtual ICollection<Ticket> TicketsBought { get; set; } = new List<Ticket>();
    }
}
