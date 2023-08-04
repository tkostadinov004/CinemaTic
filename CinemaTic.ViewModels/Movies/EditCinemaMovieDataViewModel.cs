using CinemaTic.Extensions.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTic.ViewModels.Movies
{
    public class EditCinemaMovieDataViewModel
    {
        public int CinemaId { get; set; }
        public int MovieId { get; set; }
        [Required(ErrorMessage = "Enter a starting date")]
        [PastDateValidation]
        public DateTime FromDate { get; set; }
        [Required(ErrorMessage = "Enter an ending date")]
        [PastDateValidation]
        public DateTime ToDate { get; set; }
        [Required(ErrorMessage = "Enter a ticket price")]
        [Range(1, double.MaxValue, ErrorMessage = "The price should be at least $1")]
        public decimal TicketPrice { get; set; }
    }
}
