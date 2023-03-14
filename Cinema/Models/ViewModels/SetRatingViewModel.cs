using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Models.ViewModels
{
    [NotMapped]
    public class SetRatingViewModel
    {
        public int MovieId { get; set; }
        public decimal? PersonalUserRating { get; set; }
    }
}
