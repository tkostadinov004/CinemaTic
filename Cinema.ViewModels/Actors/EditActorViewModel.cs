using Cinema.Data.Models;
using Cinema.Extensions;
using Cinema.ViewModels.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Cinema.ViewModels.Actors
{
    using static ValidationConstants;
    public class EditActorViewModel : IViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Full name")]
        [Required(ErrorMessage = "Enter a full name")]
        [StringLength(ActorFullNameMaxLength, MinimumLength = ActorFullNameMinLength, ErrorMessage = "{0} should be between {2} and {1} characters")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Enter a birthdate")]
        [PastDateValidation(ErrorMessage = "The entered date is in the future")]
        public DateTime Birthdate { get; set; }
        [Required(ErrorMessage = "Select a nationality")]
        public string Nationality { get; set; }
        [Required(ErrorMessage = "Enter a rating")]
        [Range(typeof(decimal), RatingMinValue, RatingMaxValue, ErrorMessage = "{0} should be between {1} and {2}")]
        public decimal Rating { get; set; }
        public IFormFile? Image { get; set; }

        public IEnumerable<SelectListItem>? Countries { get; set; }
    }
}
