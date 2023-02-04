using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Models.Enums
{
    public enum Genre
    {
        //Enum does not allow the usage of a hyphen (-) in the subtype names
        Adventure, Drama, History, Thriller, Fantasy, Sci_Fi, Comedy, Romance, Action
    }
}
