using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Models.Interfaces
{
    public interface IUserRole
    {
        public bool NeedsRegistration { get; set; }
    }
}
