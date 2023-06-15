using Cinema.Core.Contracts;
using Cinema.Data.Models;
using Cinema.Models.C;
using Cinema.ViewModels.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.ViewModels
{
    [NotMapped]
    public class BuyTicketViewModel : IViewModel
    {
        public Movie Movie { get; set; }
        public string? Sector { get; set; }
        public DateTime ForDate { get; set; }
        public int StartingRow { get; set; }
        public int StartingCol { get; set; }
        public bool[,] Occupied { get; set; }
        public int EndingRow { get; set; }
        public int EndingCol { get; set; }
    }
}
