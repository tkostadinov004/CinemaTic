using Cinema.Core.Contracts.Common;
using Cinema.Data.Models;
using Cinema.ViewModels.Tickets;
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
        Task<BuyTicketViewModel> GetPurchaseViewModelAsync(int movieId, string? sector, DateTime forDate);
        Task BuyTicket(string seatCoords, int movieId, DateTime forDate, string? userEmail);
        Task<bool> ExistsByIdAsync(int? id);
        Task<IEnumerable<Ticket>> GetAllAsync();
    }
}
