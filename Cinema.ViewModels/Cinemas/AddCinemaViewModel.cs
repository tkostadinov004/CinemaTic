using Cinema.Extensions;
using Cinema.ViewModels.Contracts;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Cinema.ViewModels.Cinemas
{
    public class AddCinemaViewModel : IViewModel
    {
        [Required(ErrorMessage ="Enter a name")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Name should be between {2} and {1} characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter a description")]
        [StringLength(300, MinimumLength = 15, ErrorMessage = "Description should be between {2} and {1} characters")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Enter a date")]
        [PastDateValidation(ErrorMessage = "The given date is in the future")]
        public DateTime FoundedOn { get; set; }
        [Required(ErrorMessage = "Enter a rows count")]
        [Range(typeof(int), "0", "1000", ErrorMessage = "The rows count should be between {2} and {1}")]
        public string SeatRows { get; set; }
        [Required(ErrorMessage = "Enter a columns count")]
        [Range(typeof(int), "0", "1000", ErrorMessage = "The columns count should be between {2} and {1}")]
        public string SeatCols { get; set; }
        [Required(ErrorMessage = "Upload an image")]
        public IFormFile Image { get; set; }
    }
}
