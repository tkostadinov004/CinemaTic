using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTic.ViewModels.Movies
{
    public class SetMovieScheduleViewModel
    {
        public int CinemaId { get; set; }
        public int MovieId { get; set; }
        public List<DateTimeScheduleViewModel>? Dates { get; set; }
    }
}
