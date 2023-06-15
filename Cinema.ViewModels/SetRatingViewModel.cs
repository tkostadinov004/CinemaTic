using Cinema.Core.Contracts;
using Cinema.ViewModels.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.ViewModels
{
    [NotMapped]
    public class SetRatingViewModel : IViewModel
    {
        public int MovieId { get; set; }
        public decimal? PersonalUserRating { get; set; }
    }
}
