using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Data.Models
{
    public class ActorMovie
    {
        public int ActorId { get; set; }
        public virtual Actor Actor { get; set; }

        public int MovieId { get; set; }
        public virtual Movie Movie { get; set; }
    }
}
