using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.ViewModels.Cinemas
{
    public class CinemaPagePreviewViewModel
    {
        public string CinemaLogoUrl { get; set; }
        public string ProfilePictureUrl { get; set; }
        public Dictionary<string, string> Dates { get; set; }
    }
}
