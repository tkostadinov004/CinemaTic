using Cinema.Data.Models;
using Cinema.ViewModels.Contracts;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Cinema.ViewModels.Actors
{
    public class EditActorViewModel : Actor, IViewModel
    {
        [Display(Name = "Image")]
        public IFormFile Image { get; set; }
    }
}
