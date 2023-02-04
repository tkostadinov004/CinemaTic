using Cinema.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Models.Roles
{
    public class Visitor : User, IUserRole
    {
        public Visitor()
        {
            Tickets = new List<Ticket>();
        }

        public bool NeedsRegistration { get; set; } = true;
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
