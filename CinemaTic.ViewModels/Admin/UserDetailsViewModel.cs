using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTic.ViewModels.Admin
{
    public class UserDetailsViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string ImageUrl { get; set; }
        public string Roles { get; set; }
        public IEnumerable<UserActionViewModel> Actions { get; set; }
    }
}
