using Cinema.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Models.Roles
{
    public class Owner : IUserRole
    {
        public bool NeedsRegistration { get; set; } = true;
    }
}
