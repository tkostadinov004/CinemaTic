using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTic.ViewModels.Actors
{
    public class ActorListViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Birthdate { get; set; }
        public string Nationality { get; set; }
        public string Rating { get; set; }
        public string MoviesCount { get; set; }
        public string ImageUrl { get; set; }
    }
}
