using Cinema.Extensions;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Cinema.ViewModels.Cinemas
{
    using static ValidationConstants;
    public class AddCinemaViewModel
    {
        [Required(ErrorMessage ="Enter a name")]
        [StringLength(CinemaNameMaxLength, MinimumLength = CinemaNameMinLength, ErrorMessage = "Name should be between {2} and {1} characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter a description")]
        [StringLength(CinemaDescriptionMaxLength, MinimumLength = CinemaDescriptionMinLength, ErrorMessage = "Description should be between {2} and {1} characters")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Enter a date")]
        [PastDateValidation(ErrorMessage = "The given date is in the future")]
        public DateTime FoundedOn { get; set; }
        [Required(ErrorMessage = "Enter a rows count")]
        [Range(MinimumRowsCount, MaximumRowsCount, ErrorMessage = "The rows count should be between {2} and {1}")]
        public string SeatRows { get; set; }
        [Required(ErrorMessage = "Enter a columns count")]
        [Range(MinimumColsCount, MaximumColsCount, ErrorMessage = "The columns count should be between {2} and {1}")]
        public string SeatCols { get; set; }
        [Required(ErrorMessage = "Enter a correct HEX color")]
        [RegularExpression(HexColorRegex, ErrorMessage = "Enter a correct HEX color")]
        public string BackgroundColor { get; set; }
        [Required(ErrorMessage = "Enter a correct HEX color")]
        [RegularExpression(HexColorRegex, ErrorMessage = "Enter a correct HEX color")]
        public string BoardColor { get; set; }
        [Required(ErrorMessage = "Enter a correct HEX color")]
        [RegularExpression(HexColorRegex, ErrorMessage = "Enter a correct HEX color")]
        public string TextColor { get; set; }
        [Required(ErrorMessage = "Enter a correct HEX color")]
        [RegularExpression(HexColorRegex, ErrorMessage = "Enter a correct HEX color")]
        public string ButtonBackgroundColor { get; set; }
        [Required(ErrorMessage = "Enter a correct HEX color")]
        [RegularExpression(HexColorRegex, ErrorMessage = "Enter a correct HEX color")]
        public string ButtonTextColor { get; set; }
        [Required(ErrorMessage = "Enter a correct HEX color")]
        [RegularExpression(HexColorRegex, ErrorMessage = "Enter a correct HEX color")]
        public string AccentColor { get; set; }
        [Required(ErrorMessage = "Upload an image")]
        public IFormFile Image { get; set; }
    }
}
