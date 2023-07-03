using Cinema.Core.Contracts.Common;
using Cinema.Data.Models;
using Cinema.ViewModels.Cinemas;
using Cinema.ViewModels.Contracts;

namespace Cinema.Core.Contracts
{
    public interface IOwnersService
    {
        Task AddAsync(IViewModel item, string userEmail);
        Task<IEnumerable<CinemaListViewModel>> GetAllByUserAsync(string userEmail);
    }
}
