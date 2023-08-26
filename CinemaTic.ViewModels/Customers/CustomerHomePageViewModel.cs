using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTic.ViewModels.Customers
{
    public class CustomerHomePageViewModel
    {
        public IEnumerable<CustomerCinemaViewModel> Cinemas { get; set; }
        public string FullName { get; set; }
        public string MostPopularMovieName { get; set; }
        public string MostPopularCinemaName { get; set; }
        public string MostPopularMoviePosterUrl { get; set; }
        public decimal TotalMoneySpent { get; set; }
        public int CinemasCount { get; set; }
        public int MoviesCount { get; set; }
        public int TicketsCount { get; set; }
    }
}
