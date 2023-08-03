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
        [Required]
        [PastDateValidation]
        public DateTime FromDate { get; set; }
        [Required]
        [PastDateValidation]
        public DateTime ToDate { get; set; }
        [Required]
        [Range(1, double.MaxValue)]
        public decimal TicketPrice { get; set; }
    }
}
