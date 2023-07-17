using Cinema.Core.Contracts;
using Cinema.Data;
using Cinema.Data.Models;
using Cinema.ViewModels.Actors;
using Cinema.ViewModels.Contracts;
using Cinema.ViewModels.Genres;
using Cinema.ViewModels.Movies;
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

            var genre = await _context.Genres.FirstOrDefaultAsync(i => i.Id == viewModel.Id);
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

        public async Task<EditGenreViewModel> GetEditViewModelByIdAsync(int genreId)
        {
            var genre = await _context.Genres.FirstOrDefaultAsync(i => i.Id == genreId);
            return new EditGenreViewModel
            {
                Id = genre.Id,
                Name = genre.Name
            };
        }

        public async Task<DeleteGenreViewModel> PrepareDeleteViewModelAsync(int id)
        {
            var genre = await _context.Genres.FirstOrDefaultAsync(i => i.Id == id);
            return new DeleteGenreViewModel
            {
                Id = genre.Id,
                Name = genre.Name
            };
        }

        public async Task<GenreDetailsViewModel> PrepareDetailsViewModelAsync(int? id)
        {
            var genre = await _context.Genres.Include(i => i.Movies).FirstOrDefaultAsync(i => i.Id == id);
            return new GenreDetailsViewModel
            {
                Id = genre.Id,
                Name = genre.Name,
                MoviesCount = genre.Movies.Count
            };
        }

        public async Task<IEnumerable<MovieInfoCardViewModel>> SearchAndSortMoviesByGenre(string searchText, string genreId, string sortBy)
        {
            var genre = await _context.Genres.Include(i => i.Movies)
                .FirstOrDefaultAsync(i => i.Id == int.Parse(genreId));

            var movies = genre.Movies.Select(i => new MovieInfoCardViewModel
            {
                Id = i.Id,
                Name = i.Title,
                ImageUrl = i.ImageUrl,
                AverageRating = i.UserRating.Value,
                RatingCount = i.RatingCount
            });
            if (string.IsNullOrEmpty(searchText) == false)
            {
                movies = movies.Where(i => i.Name.ToLower().StartsWith(searchText.ToLower()));
            }
            if(string.IsNullOrEmpty(sortBy) == false)
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
            return movies;
        }

        public async Task<IEnumerable<GenreListViewModel>> SortGenresAsync(string sortBy)
        {
            var genres = _context.Genres.Include(i => i.Movies).AsEnumerable();

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
