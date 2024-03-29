﻿using CinemaTic.Data.Models;
using CinemaTic.ViewModels.Cinemas;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaTic.ViewModels.Movies
{
    [NotMapped]
    public class MovieDetailsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? ImageUrl { get; set; }
        public string Description { get; set; }
        public string Director { get; set; }
        public int RunningTime { get; set; }
        public string TrailerId { get; set; }
        public string? Genre { get; set; }
        public decimal AverageRating { get; set; }
        public int RatingCount { get; set; }
        public decimal? CurrentUserRating { get; set; }
        public List<string>? Actors { get; set; }
        public List<CinemaCheckboxViewModel>? UserCinemas { get; set; }
    }
}
