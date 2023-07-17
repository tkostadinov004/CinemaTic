using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.ViewModels.Genres
{
    public class GenreDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MoviesCount { get; set; }
    }
}
