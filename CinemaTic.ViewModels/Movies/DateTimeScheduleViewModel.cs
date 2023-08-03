using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTic.ViewModels.Movies
{
    public class DateTimeScheduleViewModel
    {
        public DateTime Date { get; set; }
        public List<DateTime>? Times { get; set; }
        public int CinemaId { get; set; }
        public int MovieId { get; set; }
    }
}
