using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTic.ViewModels.Admin
{
    public class AdminAllCinemasViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Status { get; set; }
        public string AddedOn { get; set; }
        public string AddedBy { get; set; }
        public string AddedById { get; set; }
    }
}
