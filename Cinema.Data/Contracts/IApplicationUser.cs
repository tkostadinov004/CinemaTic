using Cinema.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Data.Contracts
{
    public interface IApplicationUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }
}
