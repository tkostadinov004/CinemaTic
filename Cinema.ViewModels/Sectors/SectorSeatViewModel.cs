using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.ViewModels.Sectors
{
    public class SectorSeatViewModel
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public bool IsOccupied { get; set; }
    }
}
