using Cinema.Core.Contracts.Common;
using Cinema.Data.Models;
using Cinema.ViewModels.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Core.Contracts
{
    public interface IActorsService : ICinemaService<Actor>
    {
        Task CreateAsync(IViewModel item, string country);
        Task EditByIdAsync(IViewModel item, int id, string country);
    }
}
