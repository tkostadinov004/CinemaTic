using CinemaTic.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTic.ViewModels.Cinemas
{
    public class CinemaListViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string FoundedOn { get; set; }
        public string ImageUrl { get; set; }
        public ApprovalStatus StatusCode { get; set; }
        public int MoviesCount { get; set; }
    }
}
