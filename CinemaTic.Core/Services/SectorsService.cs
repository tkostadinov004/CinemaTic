using CinemaTic.Core.Contracts;
using CinemaTic.Data;
using CinemaTic.Data.Models;
using CinemaTic.Core.Utilities;
using CinemaTic.ViewModels.Sectors;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient.Server;
using CinemaTic.Data.Configurations;

namespace CinemaTic.Core.Services
{
    public class SectorsService : ISectorsService
    {
        private readonly CinemaDbContext _context;

        public SectorsService(CinemaDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// <para>Generates the sectors of a given <see cref="Cinema"/>.</para>
        /// </summary>
        /// <returns>A <see cref="List{T}"/> of <see cref="Sector"/></returns>
        public async Task<List<Sector>> DefineSectorsAsync(int rows, int cols)
        {
            char startLetter = 'A';

            List<Sector> sectors = new List<Sector>();
            for (int i = 1; i <= rows; i += Constants.SectorRows)
            {
                for (int j = 1; j <= cols; j += Constants.SectorCols)
                {
                    int endRow = i + Constants.SectorRows - 1;
                    int endCol = j + Constants.SectorCols - 1;

                    sectors.Add(new Sector
                    {
                        SectorName = startLetter.ToString(),
                        StartRow = i,
                        StartCol = j,
                        EndRow = endRow > rows ? rows : endRow,
                        EndCol = endCol > cols ? cols : endCol
                    });
                    startLetter++;
                }
            }
            return sectors;
        }
        /// <summary>
        /// <para>Deletes the sectors of a given <see cref="Cinema"/>.</para>
        /// </summary>
        public async Task DeleteSectorsAsync(int? cinemaId)
        {
            _context.Sectors.RemoveRange(await _context.Sectors.Where(i => i.CinemaId == cinemaId).ToListAsync());
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// <para>Checks whether a <see cref="Sector"/> exists in the database.</para>
        /// </summary>
        /// <returns><see cref="bool"/></returns>
        public async Task<bool> ExistsByIdAsync(int? id)
        {
            return await _context.Sectors.AnyAsync(i => i.Id == id);
        }
        /// <summary>
        /// <para>Gets the view model, used for displaying the sector grid.</para>
        /// </summary>
        /// <returns>A <see cref="SectorGridViewModel"/> object</returns>
        public async Task<SectorGridViewModel> GetCinemaSectorsGridAsync(int? cinemaId, int? movieId, DateTime forDate)
        {
            var cinema = await _context.Cinemas.FirstOrDefaultAsync(i => i.Id == cinemaId);
            var movie = await _context.Movies.FirstOrDefaultAsync(i => i.Id == movieId);
            if (cinema != null && movie != null)
            {
                var sectors = await _context.Sectors.Where(i => i.CinemaId == cinema.Id).ToListAsync();
                return new SectorGridViewModel
                {
                    CinemaId = cinema.Id,
                    ForMovieId = movie.Id,
                    Rows = cinema.SeatRows,
                    Cols = cinema.SeatCols,
                    Sectors = sectors.Select(i => new SectorDetailsViewModel
                    {
                        Id = i.Id,
                        Name = i.SectorName,
                        CinemaId = i.CinemaId,
                        StartingRow = i.StartRow,
                        ForDateTime = forDate == default ? DateTime.Today : forDate,
                        StartingCol = i.StartCol,
                    }).ToArray(),
                    ForDateTime = forDate == default ? DateTime.Today : forDate,
                };
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// <para>Gets the seats of a given <see cref="Sector"/></para>
        /// </summary>
        /// <returns>A <see cref="List{List{T}}"/> of <see cref="SectorSeatViewModel"/></returns>
        public async Task<List<List<SectorSeatViewModel>>> GetSeatsForSectorAsync(int? sectorId, DateTime forDateTime)
        {
            var seats = new List<List<SectorSeatViewModel>>();
            var sector = await _context.Sectors.FirstOrDefaultAsync(i => i.Id == sectorId);
            var ticketsForOccupiedSeats = await _context.Tickets
                .Where(i => i.SectorId == sector.Id && i.ForDate == forDateTime).ToListAsync();

            for (int i = sector.StartRow; i <= sector.EndRow; i++)
            {
                List<SectorSeatViewModel> currentRow = new List<SectorSeatViewModel>();
                for (int j = sector.StartCol; j <= sector.EndCol; j++)
                {
                    currentRow.Add(new SectorSeatViewModel
                    {
                        Row = i,
                        Col = j,
                        IsOccupied = ticketsForOccupiedSeats.Any(s => s.SerialNumber == $"R{i}C{j}"),
                        IsChecked = false
                    });
                }
                seats.Add(currentRow);
            }
            return seats;
        }
        /// <summary>
        /// <para>Gets a <see cref="Sector"/> by given id.</para>
        /// </summary>
        /// <returns>A <see cref="SectorDetailsViewModel"/> object</returns>
        public async Task<SectorDetailsViewModel> GetSectorByIdAsync(int? sectorId, int? movieId, DateTime forDateTime)
        {
            var sector = await _context.Sectors.FirstOrDefaultAsync(i => i.Id == sectorId);
            var movie = await _context.Movies.FirstOrDefaultAsync(i => i.Id == movieId);
            if(sector != null && movie != null)
            {
                return new SectorDetailsViewModel
                {
                    Id = sector.Id,
                    ForMovieId = movie.Id,
                    Name = sector.SectorName,
                    StartingRow = sector.StartRow,
                    StartingCol = sector.StartCol,
                    CinemaId = sector.CinemaId,
                    ForDateTime = forDateTime == default ? DateTime.Today : forDateTime,
                    Seats = await this.GetSeatsForSectorAsync(sectorId, forDateTime)
                };
            }
            return null;
        }
    }
}
