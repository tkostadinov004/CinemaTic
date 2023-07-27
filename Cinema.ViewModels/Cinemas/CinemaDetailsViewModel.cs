using Cinema.Data.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.ViewModels.Cinemas
{
    public class CinemaDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string FoundedOn { get; set; }
        public int SeatRows { get; set; }
        public int SeatCols { get; set; }
        public string Status { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
    }
}
