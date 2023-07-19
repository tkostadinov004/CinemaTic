using Cinema.Core.Contracts;
using Cinema.Data;
using Cinema.Data.Models;
using Cinema.Utilities;
using Cinema.ViewModels.Sectors;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Core.Services
{
    public class SectorsService : ISectorsService
    {
        private readonly CinemaDbContext _context;

        public SectorsService(CinemaDbContext context)
        {
            _context = context;
        }

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

        public async Task DeleteSectorsAsync(int cinemaId)
        {
            _context.Sectors.RemoveRange(await _context.Sectors.Where(i => i.CinemaId == cinemaId).ToListAsync());
            await _context.SaveChangesAsync();
        }

        public async Task<SectorGridViewModel> GetCinemaSectorsGridAsync(string cinemaId, string movieId)
        {
            var cinema = await _context.Cinemas.FirstOrDefaultAsync(i => i.Id == int.Parse(cinemaId));
            var movie = await _context.Movies.FirstOrDefaultAsync(i => i.Id == int.Parse(movieId));

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
                    StartingCol = i.StartCol
                }).ToArray()
            };
        }

        public async Task<List<List<SectorSeatViewModel>>> GetSeatsForSectorAsync(string sectorId)
        {
            var seats = new List<List<SectorSeatViewModel>>();
            var sector = await _context.Sectors.FirstOrDefaultAsync(i => i.Id == int.Parse(sectorId));
            var ticketsForOccupiedSeats = await _context.Tickets
                .Where(i => i.SectorId == sector.Id).ToListAsync();

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

        public async Task<SectorDetailsViewModel> GetSectorByIdAsync(string sectorId, string movieId)
        {
            var sector = await _context.Sectors.FirstOrDefaultAsync(i => i.Id == int.Parse(sectorId));
            var movie = await _context.Movies.FirstOrDefaultAsync(i => i.Id == int.Parse(movieId));
            return new SectorDetailsViewModel
            {
                Id = sector.Id,
                ForMovieId = movie.Id,
                Name = sector.SectorName,
                StartingRow = sector.StartRow,
                StartingCol = sector.StartCol,
                CinemaId = sector.CinemaId,
                Seats = await this.GetSeatsForSectorAsync(sectorId)
            };
        }
    }
}
