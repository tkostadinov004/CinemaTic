using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Models.ViewModels
{
    public class EditMovieViewModel : Movie
    {
        [Display(Name = "Image")]
        public IFormFile Image { get; set; }
    }
}
