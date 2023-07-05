using Cinema.ViewModels.Contracts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.ViewModels.Cinemas
{
    public class EditCinemaViewModel : IViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime FoundedOn { get; set; }
        public int SeatRows { get; set; }
        public int SeatCols { get; set; }
        public IFormFile? Image { get; set; }
    }
}
