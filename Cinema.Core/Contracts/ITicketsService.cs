using Cinema.Core.Contracts.Common;
using Cinema.Data.Models;
using Cinema.ViewModels.Contracts;
using Cinema.ViewModels.Sectors;
using Cinema.ViewModels.Tickets;
using Microsoft.Data.SqlClient.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Core.Contracts
{
    public interface ITicketsService
    {
        Task<IEnumerable<Ticket>> GetTicketsByUserAsync(string userEmail);
        Task<bool> ExistsByIdAsync(int? id);
        Task<IEnumerable<Ticket>> GetAllAsync();
    }
}
