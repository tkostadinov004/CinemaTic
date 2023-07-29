using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Data.Seeder.DTOs.New
{
    public class AddMovieDTO
    {
        public string Title { get; set; }
        public string Year { get; set; }  
        public string Runtime { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public string Actors { get; set; }
        public string Plot { get; set; }
        public string Poster { get; set; }
        public string Trailer { get; set; }
    }
}
