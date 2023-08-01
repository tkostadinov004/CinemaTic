using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTic.ViewModels.Cinemas
{
    public class CinemaContainingMovieViewModel
    {
        public int Id { get; set; }
        public string Name { get; set;}
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public decimal TicketPrice { get; set; }
        public string CinemaLogoUrl { get; set; }
    }
}
