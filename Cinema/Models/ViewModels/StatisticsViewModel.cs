using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Models.ViewModels
{
    public class StatisticsViewModel
    {
        public int TicketsSold { get; set; }
        public decimal Income { get; set; }
        public Movie MostPopularMovie { get; set; }
    }
}
