using CinemaTic.Data.Models;
using CinemaTic.ViewModels.Actors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CinemaTic.ViewModels.Movies
{
    using static ValidationConstants;
    public class CreateMovieViewModel
    {
        [Required(ErrorMessage = "Enter a title!")]
        [Display(Name = "Movie title")]
        [StringLength(MovieTitleMaxLength, MinimumLength = MovieTitleMinLength, ErrorMessage = "{0} should be between {2} and {1} characters")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Choose a genre!")]
        [Display(Name = "Genre")]
        public int GenreId { get; set; }
        [Required(ErrorMessage = "Enter a brief plot description!")]
        [Display(Name = "Plot description")]
        [StringLength(MovieDescriptionMaxLength, MinimumLength = MovieDescriptionMinLength, ErrorMessage = "{0} should be between {2} and {1} characters")]
        public string Description { get; set; }
        [Range(typeof(int), "1", "400", ErrorMessage = "The movie should be at least 1 minute long!")]
        [Required(ErrorMessage = "Enter a running time")]
        [Display(Name = "Running time")]
        public string RunningTime { get; set; }
        [Required(ErrorMessage = "Enter a trailer url!")]
        [Display(Name = "Trailer URL")]
        [RegularExpression(YoutubeTrailerRegex, ErrorMessage = "Enter a correct YouTube URL")]
        public string TrailerUrl { get; set; }
        [Required(ErrorMessage = "Enter a director!")]
        [Display(Name = "Director's name")]
        [StringLength(DirectorNameMaxLength, MinimumLength = DirectorNameMinLength, ErrorMessage = "{0} should be between {2} and {1} characters")]
        public string Director { get; set; }
        [Display(Name = "Image")]
        [Required(ErrorMessage = "Add an image!")]
        public IFormFile Image { get; set; }
        public List<ActorDropdownViewModel>? ActorsDropdown { get; set; }
        public IEnumerable<SelectListItem>? Genres { get; set; }
    }
}
