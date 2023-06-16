using Cinema.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.ViewModels.Tickets
{
    public class BuyTicketViewModel
    {
        public Movie Movie { get; set; }
        public string Sector { get; set; }
        public bool[,] Occupied { get; set; }
        public DateTime ForDate { get; set; }

        public int StartingRow { get; set; }
        public int StartingCol { get; set; }
        public int EndingRow { get; set; }
        public int EndingCol { get; set; }
    }
}
