using Cinema.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.ViewModels.Admin
{
    public class UserActionViewModel
    {
        public Guid Id { get; set; }
        public string Action { get; set; }
        public string Date { get; set; }
        public string Type { get; set; }
    }
}
