using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.ViewModels.Actors
{
    public class ActorDropdownViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public bool IsChecked { get; set; } = false;
    }
}
