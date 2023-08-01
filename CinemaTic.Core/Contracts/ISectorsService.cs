using CinemaTic.Data.Models;
using CinemaTic.ViewModels.Sectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTic.Core.Contracts
{
    public interface ISectorsService
    {
        Task<List<Sector>> DefineSectorsAsync(int rows, int cols);
        Task DeleteSectorsAsync(int? cinemaId);
        Task<bool> ExistsByIdAsync(int? id);
        Task<SectorGridViewModel> GetCinemaSectorsGridAsync(int? cinemaId, int? movieId, DateTime forDate);
        Task<SectorDetailsViewModel> GetSectorByIdAsync(int? sectorId, int? movieId, DateTime forDate);
        Task<List<List<SectorSeatViewModel>>> GetSeatsForSectorAsync(int? sectorId, DateTime forDate);
    }
}
