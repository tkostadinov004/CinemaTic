﻿using Cinema.Core.Contracts;
using Cinema.Data;
using Cinema.Data.Enums;
using Cinema.Data.Models;
using Cinema.Core.Utilities;
using Cinema.ViewModels.Actors;
using Cinema.ViewModels.Cinemas;
using Cinema.ViewModels.Contracts;
using Cinema.ViewModels.Movies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

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

        public async Task EditActorAsync(IViewModel item, int id, string? country)
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

        public async Task<ActorDetailsViewModel> GetByIdAsync(int? id)
        {
            var actor = await _context.Actors
                .Include(i => i.Movies)
                .FirstOrDefaultAsync(m => m.Id == id);

            return new ActorDetailsViewModel
            {
                Id = actor.Id,
                Birthdate = actor.Birthdate.ToString(Constants.DateTimeFormat),
                FullName = actor.FullName,
                ImageUrl = actor.ImageUrl,
                MoviesCount = actor.Movies.Count.ToString(),
                Nationality = actor.Nationality,
                Rating = actor.Rating.ToString()
            };
        }

        public async Task<EditActorViewModel> GetEditViewModelByIdAsync(int actorId)
        {
            var actor = await _context.Actors.FirstOrDefaultAsync(i => i.Id == actorId);
            return new EditActorViewModel
            {
                Id = actor.Id,
                FullName = actor.FullName,
                Birthdate = actor.Birthdate,
                Nationality = actor.Nationality,
                Rating = actor.Rating
            };
        }

        public async Task<DeleteActorViewModel> PrepareDeleteViewModelAsync(int id)
        {
            var actor = await this.GetByIdAsync(id);
            return new DeleteActorViewModel
            {
                Id = actor.Id,
                FullName = actor.FullName,
                ImageUrl = actor.ImageUrl
            };
        }

        public async Task<IEnumerable<ActorListViewModel>> SearchAndFilterActorsAsync(string searchText, string filterValue, string sortBy)
        {
            var actors = _context.Actors.Include(i => i.Movies).AsEnumerable();
            if (string.IsNullOrEmpty(searchText) == false)
            {
                actors = actors.Where(i => i.FullName.ToLower().StartsWith(searchText.ToLower()));
            }
            if (string.IsNullOrEmpty(filterValue) == false && filterValue != "-Select a cinema-")
            {
                var movie = await _context.Movies.FirstOrDefaultAsync(i => i.Id == int.Parse(filterValue));
                actors = actors.Where(i => i.Movies.Contains(movie));
            }
            if (string.IsNullOrEmpty(sortBy) == false)
            {
                var sortParameter = sortBy.Split('-')[0];
                var sortDirection = sortBy.Split('-')[^1];

                switch (sortParameter)
                {
                    case "name":
                        actors = actors.OrderBy(i => i.FullName);
                        if (sortDirection == "desc")
                        {
                            actors = actors.OrderByDescending(i => i.FullName);
                        }
                        break;
                    case "birthdate":
                        actors = actors.OrderBy(i => i.Birthdate);
                        if (sortDirection == "desc")
                        {
                            actors = actors.OrderByDescending(i => i.Birthdate);
                        }
                        break;
                    case "nationality":
                        actors = actors.OrderBy(i => i.Nationality);
                        if (sortDirection == "desc")
                        {
                            actors = actors.OrderByDescending(i => i.Nationality);
                        }
                        break;
                    case "rating":
                        actors = actors.OrderBy(i => i.Rating);
                        if (sortDirection == "desc")
                        {
                            actors = actors.OrderByDescending(i => i.Rating);
                        }
                        break;
                    case "moviescount":
                        actors = actors.OrderBy(i => i.Movies.Count);
                        if (sortDirection == "desc")
                        {
                            actors = actors.OrderByDescending(i => i.Movies.Count);
                        }
                        break;
                }
            }
            return actors.Select(i => new ActorListViewModel
            {
                Id = i.Id,
                Birthdate = i.Birthdate.ToString(Constants.DateTimeFormat),
                FullName = i.FullName,
                ImageUrl = i.ImageUrl,
                MoviesCount = i.Movies.Count.ToString(),
                Nationality = i.Nationality,
                Rating = i.Rating.ToString()
            });
        }

        public async Task<IEnumerable<MovieInfoCardViewModel>> SearchMoviesByActor(string searchText, string actorId)
        {
            var actor = await _context.Actors.Include(i => i.Movies).ThenInclude(i => i.Genre)
                .FirstOrDefaultAsync(i => i.Id == int.Parse(actorId));

            var movies = actor.Movies.Select(i => new MovieInfoCardViewModel
            {
                Id = i.Id,
                Name = i.Title,
                ImageUrl = i.ImageUrl,
                AverageRating = i.UserRating.Value,
                Genre = i.Genre.Name,
                RatingCount = i.RatingCount
            });
            if (string.IsNullOrEmpty(searchText) == false)
            {
                movies = movies.Where(i => i.Name.StartsWith(searchText));
            }
            return movies;
        }
    }
}
