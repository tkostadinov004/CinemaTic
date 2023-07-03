using Cinema.Data.Models;
using Cinema.ViewModels.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Cinema.ViewModels.Movies
{
    public class CreateMovieViewModel : IViewModel
    {
        [Required(ErrorMessage = "Enter a title!")]
        [Display(Name = "Movie title")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Choose a genre!")]
        [Display(Name = "Genre")]
        public int GenreId { get; set; }
        [Required(ErrorMessage = "Enter a brief plot description!")]
        [Display(Name = "Plot description")]
        public string Description { get; set; }
        [Range(1, double.MaxValue, ErrorMessage = "The movie should be at least 1 minute long!")]
        [Display(Name = "Duration")]
        public int RunningTime { get; set; }
        [Required(ErrorMessage = "Enter a trailer url!")]
        [Display(Name = "Trailer URL")]
        public string TrailerUrl { get; set; }
        [Required(ErrorMessage = "Enter a director!")]
        public string Director { get; set; }
        public ICollection<string>? Actors { get; set; }
        [Display(Name = "Image")]
        [Required(ErrorMessage = "Add an image!")]
        public IFormFile Image { get; set; }
        public IEnumerable<SelectListItem>? ActorsDropdown { get; set; }
        public IEnumerable<SelectListItem>? Genres { get; set; }
    }
}
