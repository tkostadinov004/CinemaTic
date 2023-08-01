﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTic.Data.Models
{
    public class CustomerCinema
    {
        public CustomerCinema()
        {
            
        }
        public CustomerCinema(int cinemaId, string customerId)
        {
            CinemaId = cinemaId;
            CustomerId = customerId;
        }
        public string CustomerId { get; set; }
        public ApplicationUser Customer { get; set; }
        public int CinemaId { get; set; }
        public Cinema Cinema { get; set; }
    }
}
