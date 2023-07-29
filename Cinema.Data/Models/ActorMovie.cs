using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Data.Models
{
    public class ActorMovie
    {
        public ActorMovie()
        {
            
        }
        public ActorMovie(int actorId, int movieId)
        {
            ActorId = actorId;
            MovieId = movieId;
        }
        public int ActorId { get; set; }
        public virtual Actor Actor { get; set; }
        public int MovieId { get; set; }
        public virtual Movie Movie { get; set; }
    }
}
