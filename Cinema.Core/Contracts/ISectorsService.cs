using Cinema.Data.Models;
using Cinema.ViewModels.Sectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Core.Contracts
{
    public interface ISectorsService
    {
        Task<List<Sector>> DefineSectorsAsync(int rows, int cols);
        Task DeleteSectorsAsync(int cinemaId);
        Task<SectorGridViewModel> GetCinemaSectorsGridAsync(string cinemaId, string movieId, DateTime forDate);
        Task<SectorDetailsViewModel> GetSectorByIdAsync(string sectorId, string movieId, DateTime forDate);
        Task<List<List<SectorSeatViewModel>>> GetSeatsForSectorAsync(string sectorId, DateTime forDate);
    }
}
