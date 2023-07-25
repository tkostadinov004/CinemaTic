﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.ViewModels.Customers
{
    public class CinemasViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FoundedOn { get; set; }
        public string ImageUrl { get; set; }
        public string Owner { get; set; }
        public bool IsInFavorites { get; set; }
    }
}
