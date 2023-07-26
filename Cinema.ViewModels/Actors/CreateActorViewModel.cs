using Cinema.Data.Models;
using Cinema.ViewModels.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.ViewModels.Actors
{
    public class CreateActorViewModel : IViewModel
    {
        public string FullName { get; set; }
        public string Nationality { get; set; }
        public DateTime? Birthdate { get; set; }
        public string Rating { get; set; }
        [Display(Name = "Image")]
        [Required(ErrorMessage = "Add an image!")]
        public IFormFile Image { get; set; }

        public IEnumerable<SelectListItem>? Countries { get; set; }
    }
}
