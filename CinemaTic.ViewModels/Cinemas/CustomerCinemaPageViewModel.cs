﻿using CinemaTic.ViewModels.Movies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTic.ViewModels.Cinemas
{
    public class CustomerCinemaPageViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CinemaLogoUrl { get; set; }
        public string ProfilePictureUrl { get; set; }
        public Dictionary<string, string> Dates { get; set; }
        public string BackgroundColor { get; set; }
        public string BoardColor { get; set; }
        public string TextColor { get; set; }
        public string ButtonBackgroundColor { get; set; }
        public string ButtonTextColor { get; set; }
        public string AccentColor { get; set; }
        public IEnumerable<SponsorRecommendedMovieViewModel> SponsorRecommendedMovies { get; set; }
    }
}
