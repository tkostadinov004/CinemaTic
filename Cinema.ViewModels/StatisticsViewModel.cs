using Cinema.Core.Contracts;
using Cinema.Data.Models;
using Cinema.ViewModels.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.ViewModels
{
    public class StatisticsViewModel : IViewModel
    {
        public int TicketsSold { get; set; }
        public decimal Income { get; set; }
        public Movie MostPopularMovie { get; set; }
    }
}
