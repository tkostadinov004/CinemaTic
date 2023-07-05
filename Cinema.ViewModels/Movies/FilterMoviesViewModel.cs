using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.ViewModels.Movies
{
    public class FilterMoviesViewModel
    {
        public IEnumerable<SelectListItem>? Cinemas { get; set; }
    }
}
