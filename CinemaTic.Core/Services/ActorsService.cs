using CinemaTic.Core.Contracts;
using CinemaTic.Data;
using CinemaTic.Data.Enums;
using CinemaTic.Data.Models;
using CinemaTic.Core.Utilities;
using CinemaTic.ViewModels.Actors;
using CinemaTic.ViewModels.Movies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using CinemaTic.Extensions;

namespace CinemaTic.Core.Services
{
    public class ActorsService : IActorsService
    {
        private readonly CinemaDbContext _context;
        private readonly ILogService _logger;
        private readonly IImageService _imageService;
        public ActorsService(CinemaDbContext context, ILogService logger, IImageService imageService)
        {
            _context = context;
            _logger = logger;
            _imageService = imageService;
        }
        /// <summary>
        /// Adds an <see cref="Actor"/> to the database.
        /// </summary>
        public async Task CreateActorAsync(CreateActorViewModel? viewModel)
        {
            if (viewModel != null)
            {
                string photoName = await _imageService.UploadPhotoAsync("Actors", viewModel.Image);
                decimal rating = decimal.Parse(viewModel.Rating);
                Actor actor = new Actor
                {
                    Birthdate = viewModel.Birthdate.Value,
                    FullName = viewModel.FullName,
                    ImageUrl = photoName,
                    Nationality = viewModel.Nationality,
                    Rating = rating
                };
                _context.Add(actor);
                await _context.SaveChangesAsync();
                await _logger.LogActionAsync(UserActionType.Create, LogMessages.AddEntityMessage, "actor", viewModel.FullName, $"({viewModel.Nationality})");
            }
        }
        /// <summary>
        /// Deletes an <see cref="Actor"/> from the database by given id.
        /// </summary>
        public async Task DeleteByIdAsync(int? id)
        {
            var actor = await _context.Actors.FindAsync(id);
            if (actor != null)
            {
                _context.Actors.Remove(actor);
                await _imageService.DeleteImageAsync("Actors", actor.ImageUrl);
                await _context.SaveChangesAsync();
                await _logger.LogActionAsync(UserActionType.Delete, LogMessages.DeleteEntityMessage, "actor", actor.FullName, $"({actor.Nationality})");
            }
        }
        /// <summary>
        /// Edits an <see cref="Actor"/>.
        /// </summary>
        public async Task EditActorAsync(EditActorViewModel viewModel)
        {
            var actor = await _context.Actors.FirstOrDefaultAsync(i => i.Id == viewModel.Id);
            if (actor != null)
            {
                actor.FullName = viewModel.FullName;
                actor.Birthdate = viewModel.Birthdate;
                actor.Rating = viewModel.Rating;
                actor.Nationality = viewModel.Nationality;

                if (viewModel.Image != null)
                {
                    string photoName = await _imageService.UploadPhotoAsync("Actors", viewModel.Image);
                    actor.ImageUrl = photoName;
                }
                _context.Update(actor);
                await _context.SaveChangesAsync();
                await _logger.LogActionAsync(UserActionType.Update, LogMessages.EditEntityMessage, "actor", actor.FullName, $"({actor.Nationality})");
            }
        }
        /// <summary>
        /// Checks whether an <see cref="Actor"/> is present in the databaseл
        /// </summary>
        public async Task<bool> ExistsByIdAsync(int? id)
        {
            return await _context.Actors.AnyAsync(i => i.Id == id);
        }
        /// <summary>
        /// Gets an <see cref="Actor"/> by their id.
        /// </summary>
        /// <returns>An <see cref="Actor"/> object.</returns>
        public async Task<Actor> GetByIdAsync(int? id)
        {
            return await _context.Actors
                .Include(i => i.Movies)
                .FirstOrDefaultAsync(i => i.Id == id);
        }
        /// <summary>
        /// Gets the details view model of an <see cref="Actor"/>.
        /// </summary>
        /// <returns>An <see cref="ActorDetailsViewModel"/> object</returns>
        public async Task<ActorDetailsViewModel> GetDetailsViewModelByIdAsync(int? id)
        {
            var actor = await _context.Actors
                .Include(i => i.Movies)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (actor == null)
            {
                return null;
            }

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

        public async Task<EditActorViewModel> GetEditViewModelByIdAsync(int? actorId)
        {
            var actor = await _context.Actors.FirstOrDefaultAsync(i => i.Id == actorId);
            if (actor == null)
            {
                return null;
            }
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

        public async Task<DeleteActorViewModel> GetDeleteViewModelByIdAsync(int? id)
        {
            var actor = await _context.Actors.FirstOrDefaultAsync(i => i.Id == id);
            if (actor == null)
            {
                return null;
            }
            return new DeleteActorViewModel
            {
                Id = actor.Id,
                FullName = actor.FullName,
                ImageUrl = actor.ImageUrl
            };
        }
        /// <summary>
        /// Gets a list of countries (used for selecting an <see cref="Actor"/>'s nationality)
        /// </summary>
        /// <returns>A <see cref="CreateActorViewModel"/> object.</returns>
        public async Task<CreateActorViewModel> GetCreateViewModelAsync()
        {
            return new CreateActorViewModel
            {
                Countries = GlobalMethods.GetCountries(),
                Birthdate = null
            };
        }
        /// <summary>
        /// <para>Gets a <see cref="PaginatedList{T}"/> of actors.</para>
        /// <para>The method supports searching by name and sorting (by name, birthdate, nationality, rating, and count of movies).</para>
        /// </summary>
        /// <returns>A <see cref="PaginatedList{T}"/> of <see cref="ActorListViewModel"/></returns>
        public async Task<PaginatedList<ActorListViewModel>> QueryActorsAsync(string searchText, string sortBy, int? pageNumber)
        {
            var actors = _context.Actors.Include(i => i.Movies).AsQueryable();
            if (string.IsNullOrEmpty(searchText) == false)
            {
                actors = actors.Where(i => i.FullName.ToLower().StartsWith(searchText.ToLower()));
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
            return await PaginatedList<ActorListViewModel>.CreateAsync(actors.Select(i => new ActorListViewModel
            {
                Id = i.Id,
                Birthdate = i.Birthdate.ToString(Constants.DateTimeFormat),
                FullName = i.FullName,
                ImageUrl = i.ImageUrl,
                MoviesCount = i.Movies.Count.ToString(),
                Nationality = i.Nationality,
                Rating = i.Rating.ToString()
            }), pageNumber ?? 1, Constants.ActorsPerPage);
        }
        /// <summary>
        /// <para>Gets a <see cref="PaginatedList{T}"/>  of the movies, in which an actor is starring.</para>
        /// <para>The method supports searching by name and sorting (by name, genre, average user rating, and the amount of user ratings).</para>
        /// </summary>
        /// <returns>A <see cref="PaginatedList{T}"/> of <see cref="MovieInfoCardViewModel"/></returns>
        public async Task<PaginatedList<MovieInfoCardViewModel>> QueryMoviesByActorAsync(int? actorId, string searchText, string sortBy, int? pageNumber)
        {
            var actor = await _context.Actors.Include(i => i.Movies).ThenInclude(i => i.Movie).ThenInclude(i => i.Genre)
                .FirstOrDefaultAsync(i => i.Id == actorId);

            var movies = actor.Movies.Select(i => new MovieInfoCardViewModel
            {
                Id = i.Movie.Id,
                Name = i.Movie.Title,
                ImageUrl = i.Movie.ImageUrl,
                AverageRating = i.Movie.UserRating.Value,
                Genre = i.Movie.Genre != null ? i.Movie.Genre.Name : "No genre",
                RatingCount = i.Movie.RatingCount
            });
            if (string.IsNullOrEmpty(searchText) == false)
            {
                movies = movies.Where(i => i.Name.ToLower().StartsWith(searchText.ToLower()));
            }
            if (string.IsNullOrEmpty(sortBy) == false)
            {
                var sortParameter = sortBy.Split('-')[0];
                var sortDirection = sortBy.Split('-')[^1];

                switch (sortParameter)
                {
                    case "name":
                        movies = movies.OrderBy(i => i.Name);
                        if (sortDirection == "desc")
                        {
                            movies = movies.OrderByDescending(i => i.Name);
                        }
                        break;
                    case "genre":
                        movies = movies.OrderBy(i => i.Genre);
                        if (sortDirection == "desc")
                        {
                            movies = movies.OrderByDescending(i => i.Genre);
                        }
                        break;
                    case "rating":
                        movies = movies.OrderBy(i => i.AverageRating);
                        if (sortDirection == "desc")
                        {
                            movies = movies.OrderByDescending(i => i.AverageRating);
                        }
                        break;
                    case "ratingcount":
                        movies = movies.OrderBy(i => i.RatingCount);
                        if (sortDirection == "desc")
                        {
                            movies = movies.OrderByDescending(i => i.RatingCount);
                        }
                        break;
                }
            }
            else
            {
                movies = movies.OrderBy(i => i.Name);
            }
            return await PaginatedList<MovieInfoCardViewModel>.CreateAsync(movies, pageNumber ?? 1, Constants.ActorMoviesPerPage);
        }
    }
}
