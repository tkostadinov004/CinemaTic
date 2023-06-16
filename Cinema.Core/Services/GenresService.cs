using Cinema.Core.Contracts;
using Cinema.Data;
using Cinema.Data.Models;
using Cinema.ViewModels.Contracts;
using Cinema.ViewModels.Genres;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Core.Services
{
    public class GenresService : IGenresService
    {
        private readonly CinemaDbContext _context;
        public GenresService(CinemaDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(IViewModel item)
        {
            CreateGenreViewModel viewModel = item as CreateGenreViewModel;

            Genre genre = new Genre
            {
                Name = viewModel.Name
            };
            _context.Add(genre);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int? id)
        {
            var genre = await _context.Genres.FindAsync(id);

            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();
        }

        public async Task EditByIdAsync(IViewModel item, int id)
        {
            EditGenreViewModel viewModel = item as EditGenreViewModel;

            var genre = await _context.Genres.FirstOrDefaultAsync(i => i.Id == id);
            if (genre != null)
            {
                genre.Name = viewModel.Name;
                _context.Update(genre);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsByIdAsync(int? id)
        {
            return await _context.Genres.AnyAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Genre>> GetAllAsync()
        {
            return await _context.Genres.Include(i => i.Movies).ToListAsync();
        }

        public async Task<Genre> GetByIdAsync(int? id)
        {
            return await _context.Genres.Include(i => i.Movies).FirstOrDefaultAsync(i => i.Id == id);
        }
    }
}
