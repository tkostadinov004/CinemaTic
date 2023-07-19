using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Data.Models
{
    public class Sector
    {
        public int Id { get; set; }
        public string SectorName { get; set; }
        public int StartRow { get; set; }
        public int StartCol { get; set; }
        public int EndRow { get; set; }
        public int EndCol { get; set; }
        public int CinemaId { get; set; }
        public virtual Cinema Cinema { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
