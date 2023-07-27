using Cinema.Extensions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.ViewModels.Cinemas
{
    using static ValidationConstants;
    public class EditCinemaViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter a name")]
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
        public int SeatRows { get; set; }
        [Required(ErrorMessage = "Enter a columns count")]
        [Range(MinimumColsCount, MaximumColsCount, ErrorMessage = "The columns count should be between {2} and {1}")]
        public int SeatCols { get; set; }
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
        public IFormFile? Image { get; set; }
    }
}
