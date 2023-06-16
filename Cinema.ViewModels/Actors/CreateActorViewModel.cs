﻿using Cinema.Data.Models;
using Cinema.ViewModels.Contracts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.ViewModels.Actors
{
    public class CreateActorViewModel : Actor, IViewModel
    {
        [Display(Name = "Image")]
        [Required(ErrorMessage = "Add an image!")]
        public IFormFile Image { get; set; }
    }
}