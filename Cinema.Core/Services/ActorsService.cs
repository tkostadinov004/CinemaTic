using Cinema.Core.Contracts;
using Cinema.Data;
using Cinema.Data.Enums;
using Cinema.Data.Models;
using Cinema.Core.Utilities;
using Cinema.ViewModels.Actors;
using Cinema.ViewModels.Movies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Core.Services
{
    public class ActorsService : IActorsService
    {
        private readonly CinemaDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogService _logger;
        public ActorsService(CinemaDbContext context, IWebHostEnvironment webHostEnvironment, ILogService logger)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }
        public async Task AddActorAsync(CreateActorViewModel? viewModel)
        {
            string photoName = GlobalMethods.UploadPhoto("Actors", viewModel.Image, _webHostEnvironment);
            Actor actor = new Actor
            {
                Birthdate = viewModel.Birthdate.Value,
                FullName = viewModel.FullName,
                ImageUrl = photoName,
                Nationality = viewModel.Nationality,
                Rating = decimal.Parse(viewModel.Rating)
            };
            _context.Add(actor);
            await _context.SaveChangesAsync();
            await _logger.LogActionAsync(UserActionType.Create, LogMessages.AddEntityMessage, "actor", viewModel.FullName, $"({viewModel.Nationality})");
        }

        public async Task DeleteByIdAsync(int? id)
        {
            var actor = await _context.Actors.FindAsync(id);
            _context.Actors.Remove(actor);
            await GlobalMethods.DeleteImage("Actors", actor.ImageUrl, _context, _webHostEnvironment);
            await _context.SaveChangesAsync();
            await _logger.LogActionAsync(UserActionType.Delete, LogMessages.DeleteEntityMessage, "actor", actor.FullName, $"({actor.Nationality})");
        }

        public async Task EditActorAsync(EditActorViewModel viewModel)
        {
            var actor = await _context.Actors.FirstOrDefaultAsync(i => i.Id == viewModel.Id);
            string photoName = GlobalMethods.UploadPhoto("Actors", viewModel.Image, _webHostEnvironment);
            actor.FullName = viewModel.FullName;
            actor.Birthdate = viewModel.Birthdate;
            actor.Rating = viewModel.Rating;
            actor.Nationality = viewModel.Nationality;

            if (viewModel.Image != null)
            {
                actor.ImageUrl = photoName;
            }
            _context.Update(actor);
            await _context.SaveChangesAsync();
            await _logger.LogActionAsync(UserActionType.Update, LogMessages.EditEntityMessage, "actor", actor.FullName, $"({actor.Nationality})");
        }

        public async Task<bool> ExistsByIdAsync(int? id)
        {
            return await _context.Actors.AnyAsync(e => e.Id == id);
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
                Rating = actor.Rating,
                Countries = GlobalMethods.GetCountries()
            };
        }

        public async Task<DeleteActorViewModel> PrepareDeleteViewModelAsync(int id)
        {
            var actor = await _context.Actors.FirstOrDefaultAsync(i => i.Id == id);
            return new DeleteActorViewModel
            {
                Id = actor.Id,
                FullName = actor.FullName,
                ImageUrl = actor.ImageUrl
            };
        }

        public async Task<CreateActorViewModel> PrepareForAddingAsync()
        {
            return new CreateActorViewModel
            {
                Countries = GlobalMethods.GetCountries(),
                Birthdate = null
            };
        }

        public async Task<IEnumerable<ActorListViewModel>> SearchAndFilterActorsAsync(string searchText, string filterValue, string sortBy)
        {
            var actors = _context.Actors.Include(i => i.Movies).AsQueryable();
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

        public async Task<IEnumerable<MovieInfoCardViewModel>> SearchMoviesByActor(string searchText, int actorId)
        {
            var actor = await _context.Actors.Include(i => i.Movies).ThenInclude(i => i.Genre)
                .FirstOrDefaultAsync(i => i.Id == actorId);

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
