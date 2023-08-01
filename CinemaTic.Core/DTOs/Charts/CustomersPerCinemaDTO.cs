using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTic.Core.DTOs.Charts
{
    public class CustomersPerCinemaDTO
    {
        public string[] Labels { get; set; }
        public int[] CustomersCounts { get; set; }
    }
}
