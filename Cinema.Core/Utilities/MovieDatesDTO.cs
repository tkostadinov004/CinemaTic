using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Core.Utilities
{
    public class MovieDatesDTO
    {
        [JsonProperty("movieId")]
        public int MovieId { get; set; }
        [JsonProperty("movieData")]
        public CinemaDTO[] Cinemas { get; set; }
    }
}
