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
    }
}
