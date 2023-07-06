using Cinema.Core.Contracts;
using Cinema.Core.Utilities;
using Cinema.Data;
using Cinema.Data.Models;
using Cinema.Utilities;
using Cinema.ViewModels.Cinemas;
using Cinema.ViewModels.Contracts;
using Cinema.ViewModels.Movies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Core.Services
{
    public class OwnersService : IOwnersService
    {
        private readonly CinemaDbContext _context;
        private IWebHostEnvironment _webHostEnvironment;
        private UserManager<ApplicationUser> _userManager;

        public OwnersService(CinemaDbContext context, IWebHostEnvironment webHostEnvironment, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
        }

        public async Task AddAsync(IViewModel item, string userEmail)
        {
            var viewModel = item as AddCinemaViewModel;
            string imageUrl = GlobalMethods.UploadPhoto("Cinema", viewModel.Image, _webHostEnvironment);
            var cinema = new Data.Models.Cinema
            {
                Name = viewModel.Name,
                Description = viewModel.Description,
                SeatRows = int.Parse(viewModel.SeatRows),
                SeatCols = int.Parse(viewModel.SeatCols),
                FoundedOn = viewModel.FoundedOn,
                ImageUrl = imageUrl,
                Owner = await _userManager.FindByEmailAsync(userEmail)
            };
            _context.Cinemas.Add(cinema);
            await _context.SaveChangesAsync();
        }

        public async Task AddMovieToCinemas(MovieDatesDTO data)
        {
            var cinemas = _context.Cinemas.Where(i => data.Cinemas.Select(d => d.CinemaId).Contains(i.Id));

            var movie = await _context.Movies.FirstOrDefaultAsync(i => i.Id == data.MovieId);
            foreach (var item in cinemas)
            {
                var currentCinemaInfo = data.Cinemas.FirstOrDefault(i => i.CinemaId == item.Id);
                movie.Cinemas.Add(new CinemaMovie
                {
                    CinemaId = item.Id,
                    FromDate = currentCinemaInfo.FromDate,
                    ToDate = currentCinemaInfo.ToDate,
                    MoviePrice = currentCinemaInfo.Price
                });
            }
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int? id)
        {
            var cinema = await _context.Cinemas.FindAsync(id);
            _context.Cinemas.Remove(cinema);
            await GlobalMethods.DeleteImage("Cinemas", cinema.ImageUrl, _context, _webHostEnvironment);
            await _context.SaveChangesAsync();
        }

        public async Task EditCinema(IViewModel item)
        {
            var viewModel = item as EditCinemaViewModel;
            var cinema = await _context.Cinemas.FirstOrDefaultAsync(i => i.Id == viewModel.Id);

            cinema.Name = viewModel.Name;
            cinema.Description = viewModel.Description;
            cinema.FoundedOn = viewModel.FoundedOn;
            cinema.SeatRows = viewModel.SeatRows;
            cinema.SeatCols = viewModel.SeatCols;

            if (viewModel.Image != null)
            {
                await GlobalMethods.DeleteImage("Cinemas", cinema.ImageUrl, _context, _webHostEnvironment);
                cinema.ImageUrl = GlobalMethods.UploadPhoto("Cinemas", viewModel.Image, _webHostEnvironment);
            }
            _context.Cinemas.Update(cinema);
            await _context.SaveChangesAsync();
        }

        public Task<bool> ExistsByIdAsync(int? id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CinemaListViewModel>> GetAllByUserAsync(string userEmail)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(userEmail);
            var cinemas = await _context.Cinemas.Include(i => i.Movies).Where(i => i.OwnerId == user.Id).ToListAsync();

            return cinemas.Select(i => new CinemaListViewModel
            {
                Id = i.Id,
                Name = i.Name,
                Status = (i.IsApproved ? "Approved" : "Pending approval"),
                IsApproved = i.IsApproved,
                FoundedOn = i.FoundedOn.ToString(Constants.DateTimeFormat),
                ImageUrl = i.ImageUrl,
                MoviesCount = i.Movies.Count
            }).ToList();
        }

        public async Task<CinemaDetailsViewModel> GetByIdAsync(int? id)
        {
            var cinema = await _context.Cinemas
                .Include(i => i.Owner)
                .Include(i => i.Movies)
                .ThenInclude(i => i.Movie)
                .ThenInclude(i => i.Genre)
                .Include(i => i.Movies)
                .ThenInclude(i => i.Movie)
                .ThenInclude(i => i.AddedBy)
                .FirstOrDefaultAsync(i => i.Id == id);
            return new CinemaDetailsViewModel
            {
                Id = cinema.Id,
                Name = cinema.Name,
                Description = cinema.Description,
                FoundedOn = cinema.FoundedOn.ToString(Constants.DateTimeFormat),
                ImageUrl = cinema.ImageUrl,
                SeatRows = cinema.SeatRows,
                SeatCols = cinema.SeatCols,
                IsApproved = cinema.IsApproved
            };
        }

        public async Task<EditCinemaViewModel> GetEditViewModelByIdAsync(int cinemaId)
        {
            var cinema = (await _context.Cinemas.FirstOrDefaultAsync(i => i.Id == cinemaId));
            return new EditCinemaViewModel
            {
                Id = cinema.Id,
                SeatCols = cinema.SeatCols,
                SeatRows = cinema.SeatRows,
                Description = cinema.Description,
                FoundedOn = cinema.FoundedOn,
                Image = null,
                Name = cinema.Name
            };
        }

        public async Task<OwnerStatisticsViewModel> GetStatisticsAsync(string userEmail)
        {
            var cinemas = await _context.Cinemas.ToListAsync();
            var tickets = await _context.Tickets.ToListAsync();
            var currentUser = await _userManager.FindByEmailAsync(userEmail);

            decimal personalIncome = cinemas.Where(i => i.OwnerId == currentUser.Id)
                .Select(i => i.Tickets.Select(k => k.Price).Sum()).Sum();
            decimal totalIncome = cinemas
                .Select(i => i.Tickets.Select(k => k.Price).Sum()).Sum();

            var cinemasVisitors = cinemas.ToDictionary(cinema => cinema.Name, value => value.Visitors.Count);
            var cinemasTotalRevenues = cinemas.ToDictionary(cinema => cinema.Name, value => value.Tickets.Sum(i => i.Price));
            return new OwnerStatisticsViewModel
            {
                PersonalIncome = personalIncome,
                TotalIncome = totalIncome,
                CinemasVisitors = cinemasVisitors,
                CinemasTotalRevenues = cinemasTotalRevenues
            };
        }

        public async Task<DeleteCinemaViewModel> PrepareDeleteViewModelAsync(int id)
        {
            var cinema = await this.GetByIdAsync(id);
            return new DeleteCinemaViewModel
            {
                Id = cinema.Id,
                Name = cinema.Name,
                ImageUrl = cinema.ImageUrl
            };
        }

        public async Task<IEnumerable<CinemaListViewModel>> SearchAndFilterCinemasAsync(string searchText, string userEmail, string filterValue)
        {
            var cinemas = await this.GetAllByUserAsync(userEmail);
            if (string.IsNullOrEmpty(searchText) == false)
            {
                cinemas = cinemas.Where(i => i.Name.StartsWith(searchText));
            }
            if(string.IsNullOrEmpty(filterValue) == false)
            {
                switch (filterValue)
                {
                    case "Approved":
                        cinemas = cinemas.Where(i => i.Status == "Approved");
                        break;
                    case "Pending approval":
                        cinemas = cinemas.Where(i => i.Status == "Pending approval");
                        break;
                }
            }
            return cinemas;
        }

        public async Task<IEnumerable<MovieInfoCardViewModel>> SearchMoviesByCinema(string searchText, string cinemaId)
        {
            var cinema = await _context.Cinemas
                .Include(i => i.Movies)
                .ThenInclude(i => i.Movie)
                .ThenInclude(i => i.Genre)
                .Include(i => i.Movies).ThenInclude(i => i.Movie).ThenInclude(i => i.AddedBy)
                .FirstOrDefaultAsync(i => i.Id == int.Parse(cinemaId));
            var movies = cinema.Movies.Select(i => new MovieInfoCardViewModel
            {
                Id = i.Movie.Id,
                Name = i.Movie.Title,
                ImageUrl = i.Movie.ImageUrl,
                AverageRating = i.Movie.UserRating.Value,
                Genre = i.Movie.Genre.Name,
                RatingCount = i.Movie.RatingCount,
                AddedBy = $"{i.Movie.AddedBy.FirstName} {i.Movie.AddedBy.LastName}"
            });
            if (string.IsNullOrEmpty(searchText) == false)
            {
                movies = movies.Where(i => i.Name.StartsWith(searchText));
            }
            return movies;
        }
    }
}
