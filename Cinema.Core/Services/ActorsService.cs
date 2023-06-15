using Cinema.Core.Contracts;
using Cinema.Core.Contracts.Common;
using Cinema.Data;
using Cinema.Data.Models;
using Cinema.Utilities;
using Cinema.ViewModels;
using Cinema.ViewModels.Contracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                ImageUrl = photoName,
                Nationality = viewModel.Nationality,
                Rating = viewModel.Rating
            };
            _context.Add(actor);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
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
            actor.FirstName = viewModel.FirstName;
            actor.LastName = viewModel.LastName;
            actor.Birthdate = viewModel.Birthdate;
            actor.Rating = viewModel.Rating;

            if (country != null)
            {
                actor.Nationality = country;
            }
            if (actorVM.Image != null)
            {
                actor.ImageUrl = photoName;
            }
            _context.Update(actor);
            await _context.SaveChangesAsync();
        }

        public Task<IEnumerable<Actor>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Actor> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
