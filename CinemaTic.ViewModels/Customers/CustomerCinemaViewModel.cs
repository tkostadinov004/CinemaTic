using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTic.ViewModels.Customers
{
    public class CustomerCinemaViewModel
    {
        public int CinemaId { get; set; }
        public string CinemaName { get; set; }
        public string Description { get; set; }
        public string FavoriteSince { get; set; } 
        public string ImageUrl { get; set; } 
    }
}
