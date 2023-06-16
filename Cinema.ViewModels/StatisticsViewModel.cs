using Cinema.Data.Models;
using Cinema.ViewModels.Contracts;

namespace Cinema.ViewModels
{
    public class StatisticsViewModel : IViewModel
    {
        public int TicketsSold { get; set; }
        public decimal Income { get; set; }
        public Movie MostPopularMovie { get; set; }
    }
}
