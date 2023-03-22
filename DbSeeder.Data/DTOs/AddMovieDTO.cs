using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbSeeder.Data.DTOs
{
    public class AddMovieDTO
    {
        public string EnglishTitle { get; set; }
        public string BulgarianTitle { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
        public string Price { get; set; }
        public string RunningTime { get; set; }
        public string TrailerUrl { get; set; }
    }
}
