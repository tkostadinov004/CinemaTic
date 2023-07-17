using Cinema.Data.Models;
using Cinema.ViewModels.Contracts;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Cinema.ViewModels.Actors
{
    public class EditActorViewModel : IViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime Birthdate { get; set; }
        public string Nationality { get; set; }
        public decimal Rating { get; set; }
        public IFormFile? Image { get; set; }
    }
}
