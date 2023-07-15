using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Data.Models
{
    public class UserMovie
    {
        public string CustomerId { get; set; }
        public virtual ApplicationUser Customer { get; set; }

        public int MovieId { get; set; }
        public virtual Movie Movie { get; set; }

        [Range(1.0, 10.0)]
        public decimal Rating { get; set; }
    }
}
