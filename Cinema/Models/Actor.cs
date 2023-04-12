using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Models
{
    public class Actor
    {
        public Actor()
        {
            Movies = new List<ActorMovie>();
        }
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Въведете собствено име!"), MaxLength(100), RegularExpression("^[A-Za-z]+$")]
        [Display(Name ="Име")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Въведете фамилно име!"), MaxLength(100), RegularExpression("^[A-Za-z]+$")]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Въведете име и фамилия на български!")]
        [Display(Name = "Име и фамилия на български")]
        public string BulgarianFullName { get; set; }
        [Required(ErrorMessage = "Въведете дата на раждане!")]
        [Display(Name = "Дата на раждане")]
        public DateTime Birthdate { get; set; }
        [Display(Name = "Националност")]
        public string Nationality { get; set; }
        [Required(ErrorMessage = "Въведете рейтинг от IMDB!")]
        [Range(1, 10, ErrorMessage = "Въведете рейтинг от 1 до 10!")]
        [Display(Name = "Рейтинг от IMDB")]
        public decimal Rating { get; set; }
        [Display(Name = "Снимка")]
        public string? ImageUrl { get; set; }

        [Display(Name = "Филми")]
        public virtual ICollection<ActorMovie> Movies { get; set; }
    }
}
