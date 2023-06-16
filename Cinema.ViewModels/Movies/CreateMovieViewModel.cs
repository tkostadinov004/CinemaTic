using Cinema.Data.Models;
using Cinema.ViewModels.Contracts;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Cinema.ViewModels.Movies
{
    public class CreateMovieViewModel : Movie, IViewModel
    {
        [Display(Name = "Image")]
        [Required(ErrorMessage = "Add an image!")]
        public IFormFile Image { get; set; }
    }
}
