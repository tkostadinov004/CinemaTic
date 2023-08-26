using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTic.Core.DTOs.Charts
{
    public class TicketsBoughtDTO
    {
        public string[] Labels { get; set; }
        public int[] TicketsCounts { get; set; }
    }
}
