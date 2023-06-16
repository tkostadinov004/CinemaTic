using Cinema.Core.Contracts;
using Cinema.Data.Models;
using Cinema.ViewModels.Contracts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.ViewModels.Movies
{
    public class EditMovieViewModel : Movie, IViewModel
    {
        [Display(Name = "Image")]
        public IFormFile Image { get; set; }
    }
}
