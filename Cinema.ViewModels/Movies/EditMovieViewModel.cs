using Cinema.Data.Models;
using Cinema.ViewModels.Actors;
using Cinema.ViewModels.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Cinema.ViewModels.Movies
{
    public class EditMovieViewModel : IViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int GenreId { get; set; }
        public string Director { get; set; }
        public string Description { get; set; }
        public string RunningTime { get; set; }
        public string TrailerUrl { get; set; }
        public List<string>? Actors { get; set; }
        [Display(Name = "Image")]
        public IFormFile Image { get; set; }
        public List<ActorDropdownViewModel>? ActorsDropdown { get; set; }
        public IEnumerable<SelectListItem>? Genres { get; set; }
    }
}
