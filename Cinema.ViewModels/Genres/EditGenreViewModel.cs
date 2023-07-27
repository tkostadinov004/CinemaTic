using Cinema.ViewModels.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Cinema.ViewModels.Genres
{
    using static ValidationConstants;
    public class EditGenreViewModel : IViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Genre name")]
        [Required(ErrorMessage = "Enter a genre name")]
        [StringLength(GenreNameMaxLength, MinimumLength = GenreNameMinLength, ErrorMessage = "{0} should be between {2} and {1} characters")]
        public string Name { get; set; }
    }
}
