using Cinema.ViewModels.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.ViewModels.Cinemas
{
    public class CinemaDetailsViewModel : IViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string FoundedOn { get; set; }
        public int SeatRows { get; set; }
        public int SeatCols { get; set; }
        public bool IsApproved { get; set; }
        public IEnumerable<MovieInfoCardViewModel> Movies { get; set; }
    }
}
