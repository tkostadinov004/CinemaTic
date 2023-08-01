using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaTic.Data.Models
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
        public string Title { get; set; }
        public int? GenreId { get; set; }
        public string? ImageUrl { get; set; }
        public string Description { get; set; }
        public decimal? UserRating { get; set; }
        public int RatingCount { get; set; }
        public int RunningTime { get; set; }
        public string TrailerUrl { get; set; }
        public string Director { get; set; }
        public string AddedById { get; set; }
        public virtual ApplicationUser AddedBy { get; set; }
        public virtual Genre Genre { get; set; }
        public virtual ICollection<ActorMovie> Actors { get; set; }
        public virtual ICollection<CinemaMovie> Cinemas { get; set; } = new List<CinemaMovie>();
        public virtual ICollection<Ticket> TicketsBought { get; set; } = new List<Ticket>();
    }
}
