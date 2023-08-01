using CinemaTic.Core.Contracts;
using CinemaTic.Core.Utilities;
using CinemaTic.Data;
using CinemaTic.Data.Enums;
using CinemaTic.Data.Models;
using CinemaTic.Extensions.ModelBinders;
using CinemaTic.ViewModels.Genres;
using CinemaTic.ViewModels.Movies;
using Microsoft.EntityFrameworkCore;

namespace CinemaTic.Core.Services
{
    public class GenresService : IGenresService
    {
        private readonly CinemaDbContext _context;
        private readonly ILogService _logger;
        public GenresService(CinemaDbContext context, ILogService logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task CreateAsync(CreateGenreViewModel viewModel)
        {
            Genre genre = new Genre
            {
                Name = viewModel.Name
            };
            _context.Add(genre);
            await _context.SaveChangesAsync();
            await _logger.LogActionAsync(UserActionType.Create, LogMessages.AddEntityMessage, "genre", genre.Name, "");
        }

        public async Task DeleteByIdAsync(int? id)
        {
            var genre = await _context.Genres.FindAsync(id);

            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();
            await _logger.LogActionAsync(UserActionType.Delete, LogMessages.DeleteEntityMessage, "genre", genre.Name, "");
        }

        public async Task EditByIdAsync(EditGenreViewModel viewModel)
        {
            var genre = await _context.Genres.FirstOrDefaultAsync(i => i.Id == viewModel.Id);
            if (genre != null)
            {
                genre.Name = viewModel.Name;
                _context.Update(genre);
                await _context.SaveChangesAsync();
                await _logger.LogActionAsync(UserActionType.Update, LogMessages.EditEntityMessage, "genre", genre.Name, "");
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

        public async Task<EditGenreViewModel> GetEditViewModelByIdAsync(int? genreId)
        {
            var genre = await _context.Genres.FirstOrDefaultAsync(i => i.Id == genreId);
            if (genre != null)
            {
                return new EditGenreViewModel
                {
                    Id = genre.Id,
                    Name = genre.Name
                };
            }
            return null;
        }

        public async Task<DeleteGenreViewModel> GetDeleteViewModelByIdAsync(int? id)
        {
            var genre = await _context.Genres.FirstOrDefaultAsync(i => i.Id == id);
            if (genre != null)
            {
                return new DeleteGenreViewModel
                {
                    Id = genre.Id,
                    Name = genre.Name
                };
            }
            return null;
        }

        public async Task<GenreDetailsViewModel> GetDetailsViewModelByIdAsync(int? id)
        {
            var genre = await _context.Genres.Include(i => i.Movies).FirstOrDefaultAsync(i => i.Id == id);
            if (genre != null)
            {
                return new GenreDetailsViewModel
                {
                    Id = genre.Id,
                    Name = genre.Name,
                    MoviesCount = genre.Movies.Count
                };
            }
            return null;
        }

        public async Task<IEnumerable<MovieInfoCardViewModel>> SearchAndSortMoviesByGenre(int? genreId, string searchText, string sortBy, int? pageNumber)
        {
            var genre = await _context.Genres.Include(i => i.Movies)
                .FirstOrDefaultAsync(i => i.Id == genreId);

            var movies = genre.Movies.AsQueryable();
            if (string.IsNullOrEmpty(searchText) == false)
            {
                movies = movies.Where(i => i.Title.ToLower().StartsWith(searchText.ToLower()));
            }
            if (string.IsNullOrEmpty(sortBy) == false)
            {
                var sortParameter = sortBy.Split('-')[0];
                var sortDirection = sortBy.Split('-')[^1];

                switch (sortParameter)
                {
                    case "name":
                        movies = movies.OrderBy(i => i.Title);
                        if (sortDirection == "desc")
                        {
                            movies = movies.OrderByDescending(i => i.Title);
                        }
                        break;
                    case "rating":
                        movies = movies.OrderBy(i => i.UserRating);
                        if (sortDirection == "desc")
                        {
                            movies = movies.OrderByDescending(i => i.UserRating);
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
            return await PaginatedList<MovieInfoCardViewModel>.CreateAsync(movies.Select(i => new MovieInfoCardViewModel
            {
                Id = i.Id,
                Name = i.Title,
                ImageUrl = i.ImageUrl,
                AverageRating = i.UserRating.Value,
                RatingCount = i.RatingCount
            }), pageNumber ?? 1, 5);
        }
        public async Task<IEnumerable<GenreListViewModel>> SortGenresAsync(string sortBy)
        {
            var genres = _context.Genres.Include(i => i.Movies).OrderBy(i => i.Name).AsEnumerable();

            if (string.IsNullOrEmpty(sortBy) == false)
            {
                var sortParameter = sortBy.Split('-')[0];
                var sortDirection = sortBy.Split('-')[^1];

                switch (sortParameter)
                {
                    case "name":
                        genres = genres.OrderBy(i => i.Name);
                        if (sortDirection == "desc")
                        {
                            genres = genres.OrderByDescending(i => i.Name);
                        }
                        break;
                    case "moviescount":
                        genres = genres.OrderBy(i => i.Movies.Count);
                        if (sortDirection == "desc")
                        {
                            genres = genres.OrderByDescending(i => i.Movies.Count);
                        }
                        break;
                }
            }
            return genres.Select(i => new GenreListViewModel
            {
                Id = i.Id,
                Name = i.Name,
                MoviesCount = i.Movies.Count
            });
        }
    }
}
