using Cinema.Core.Contracts;
using Cinema.Data;
using Cinema.Data.Models;
using Cinema.Utilities;
using Cinema.ViewModels.Actors;
using Cinema.ViewModels.Contracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Core.Services
{
    public class ActorsService : IActorsService
    {
        private readonly CinemaDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ActorsService(CinemaDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task CreateAsync(IViewModel? item, string country)
        {
            CreateActorViewModel? viewModel = item as CreateActorViewModel;

            viewModel.Nationality = country;
            string photoName = GlobalMethods.UploadPhoto("Actors", viewModel.Image, _webHostEnvironment);
            Actor actor = new Actor
            {
                Birthdate = viewModel.Birthdate,
                FullName = viewModel.FullName,
                ImageUrl = photoName,
                Nationality = viewModel.Nationality,
                Rating = viewModel.Rating
            };
            _context.Add(actor);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int? id)
        {
            var actor = await _context.Actors.FindAsync(id);
            _context.Actors.Remove(actor);
            await GlobalMethods.DeleteImage("Actors", actor.ImageUrl, _context, _webHostEnvironment);
            await _context.SaveChangesAsync();
        }

        public async Task EditByIdAsync(IViewModel item, int id, string? country)
        {
            EditActorViewModel? viewModel = item as EditActorViewModel;

            var actor = await _context.Actors.FirstOrDefaultAsync(i => i.Id == id);
            string photoName = GlobalMethods.UploadPhoto("Actors", viewModel.Image, _webHostEnvironment);
            actor.FullName = viewModel.FullName;
            actor.Birthdate = viewModel.Birthdate;
            actor.Rating = viewModel.Rating;

            if (country != null)
            {
                actor.Nationality = country;
            }
            if (viewModel.Image != null)
            {
                actor.ImageUrl = photoName;
            }
            _context.Update(actor);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsByIdAsync(int? id)
        {
            return await _context.Actors.AnyAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Actor>> GetAllAsync()
        {
            return await _context.Actors.Include(i => i.Movies).ToListAsync();
        }

        public async Task<Actor> GetByIdAsync(int? id)
        {
            var actor = await _context.Actors
                .Include(i => i.Movies)
                .FirstOrDefaultAsync(m => m.Id == id);

            return actor;
        }
    }
}
