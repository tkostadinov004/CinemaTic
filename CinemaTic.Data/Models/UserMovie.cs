using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaTic.Data.Models
{
    public class UserMovie
    {
        public UserMovie()
        {
            
        }
        public UserMovie(string customerId, int movieId, decimal rating)
        {
            CustomerId = customerId;
            MovieId = movieId;
            Rating = rating;
        }
        public string CustomerId { get; set; }
        public virtual ApplicationUser Customer { get; set; }

        public int MovieId { get; set; }
        public virtual Movie Movie { get; set; }

        [Range(1.0, 10.0)]
        public decimal Rating { get; set; }
    }
}
