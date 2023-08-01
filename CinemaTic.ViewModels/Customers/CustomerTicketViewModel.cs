using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTic.ViewModels.Customers
{
    public class CustomerTicketViewModel
    {
        public int Id { get; set; }
        public string SerialNumber { get; set; }
        public string Movie { get; set; }
        public string MoviePosterUrl { get; set; }
        public string Date { get; set; }
        public string Cinema { get; set; }
        public string Sector { get; set; }
        public string Price { get; set; }
    }
}
