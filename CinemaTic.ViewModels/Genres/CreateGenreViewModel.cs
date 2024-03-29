﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTic.ViewModels.Genres
{
    using static ValidationConstants;
    public class CreateGenreViewModel
    {
        [Display(Name = "Genre name")]
        [Required(ErrorMessage = "Enter a genre name")]
        [StringLength(GenreNameMaxLength, MinimumLength = GenreNameMinLength, ErrorMessage = "{0} should be between {2} and {1} characters")]
        public string Name { get; set; }
    }
}
