using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.ViewModels.Sectors
{
    public class SectorDetailsViewModel
    {
        public int Id { get; set; }
        public int ForMovieId { get; set; }
        public string Name { get; set; }
        public int CinemaId { get; set; }
        public int StartingRow { get; set; }
        public int StartingCol { get; set; }
        public DateTime ForDateTime { get; set; }
        public List<List<SectorSeatViewModel>> Seats { get; set; }
    }
}
