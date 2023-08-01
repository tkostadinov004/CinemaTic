using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTic.ViewModels.Users
{
    public class ChangeProfilePictureViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Attach an image")]
        public IFormFile Image { get; set; }
    }
}
