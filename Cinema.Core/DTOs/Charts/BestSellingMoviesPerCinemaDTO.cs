using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Core.DTOs.Charts
{
    public class BestSellingMoviesPerCinemaDTO
    {
        public string[] Labels { get; set; }
        public int[] MoviesCounts { get; set; }
    }
}
