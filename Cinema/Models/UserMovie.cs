using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Models
{
    public class UserMovie
    {
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public int MovieId { get; set; }
        public virtual Movie Movie { get; set; }

        [Range(1.0, 10.0)]
        public decimal Rating { get; set; }
    }
}
