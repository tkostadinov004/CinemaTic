using AutoMapper;
using CinemaTic.Core.Contracts;
using CinemaTic.Core.Utilities;
using CinemaTic.Data;
using CinemaTic.Data.Enums;
using CinemaTic.Data.Models;
using CinemaTic.ViewModels.Cinemas;
using CinemaTic.ViewModels.Movies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text.RegularExpressions;

namespace CinemaTic.Core.Services
{
    public class CinemasService : ICinemasService
    {
        private readonly CinemaDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ISectorsService _sectorsService;
        private readonly ILogService _logger;
        private readonly IImageService _imageService;

        public CinemasService(CinemaDbContext context, UserManager<ApplicationUser> userManager, IMapper mapper, ISectorsService sectorsService, ILogService logger, IImageService imageService)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
            _sectorsService = sectorsService;
            _logger = logger;
            _imageService = imageService;
        }

        public async Task CreateCinemaAsync(CreateCinemaViewModel viewModel, string userEmail)
        {
            string imageUrl = await _imageService.UploadPhotoAsync("Cinemas", viewModel.Image);

            var cinema = new Data.Models.Cinema
            {
                AccentColor = viewModel.AccentColor,
                BackgroundColor = viewModel.BackgroundColor,
                BoardColor = viewModel.BoardColor,
                ButtonBackgroundColor = viewModel.ButtonBackgroundColor,
                ButtonTextColor = viewModel.ButtonTextColor,
                Description = viewModel.Description,
                FoundedOn = viewModel.FoundedOn,
                ImageUrl = imageUrl ?? "default.jpg",
                Owner = await _userManager.FindByEmailAsync(userEmail),
                Name = viewModel.Name,
                SeatCols = int.Parse(viewModel.SeatCols),
                SeatRows = int.Parse(viewModel.SeatRows),
                TextColor = viewModel.TextColor
            };
            cinema.Sectors = await _sectorsService.DefineSectorsAsync(cinema.SeatRows, cinema.SeatCols);

            _context.Cinemas.Add(cinema);
            await _context.SaveChangesAsync();
            await _logger.LogActionAsync(UserActionType.Create, LogMessages.AddEntityMessage, "cinema", viewModel.Name, $"({viewModel.SeatRows} rows and {viewModel.SeatCols} columns)");
        }

        public async Task DeleteByIdAsync(int? id)
        {
            var customerCinemas = _context.CustomersCinemas.Where(i => i.CinemaId == id);
            _context.CustomersCinemas.RemoveRange(customerCinemas);
            await _context.SaveChangesAsync();

            var tickets = _context.Tickets.Where(i => i.CinemaId == id);
            _context.Tickets.RemoveRange(tickets);
            await _context.SaveChangesAsync();

            var sectors = _context.Sectors.Where(i => i.CinemaId == id);
            _context.Sectors.RemoveRange(sectors);
            await _context.SaveChangesAsync();

            var cinema = await _context.Cinemas.FindAsync(id);
            _context.Cinemas.Remove(cinema);

            await _imageService.DeleteImageAsync("Cinemas", cinema.ImageUrl);
            await _context.SaveChangesAsync();
            await _logger.LogActionAsync(UserActionType.Delete, LogMessages.DeleteEntityMessage, "cinema", cinema.Name, $"({cinema.SeatRows} rows and {cinema.SeatCols} columns)");
        }

        public async Task EditCinemaAsync(EditCinemaViewModel viewModel)
        {
            var cinema = await _context.Cinemas.FirstOrDefaultAsync(i => i.Id == viewModel.Id);

            if (viewModel.SeatRows != cinema.SeatRows || viewModel.SeatCols != cinema.SeatCols)
            {
                await _sectorsService.DeleteSectorsAsync(cinema.Id);
                cinema.Sectors = await _sectorsService.DefineSectorsAsync(viewModel.SeatRows, viewModel.SeatCols);
            }

            cinema.Name = viewModel.Name;
            cinema.Description = viewModel.Description;
            cinema.FoundedOn = viewModel.FoundedOn;
            cinema.SeatRows = viewModel.SeatRows;
            cinema.SeatCols = viewModel.SeatCols;
            cinema.TextColor = viewModel.TextColor;
            cinema.AccentColor = viewModel.AccentColor;
            cinema.ButtonTextColor = viewModel.ButtonTextColor;
            cinema.ButtonBackgroundColor = viewModel.ButtonBackgroundColor;
            cinema.BackgroundColor = viewModel.BackgroundColor;
            cinema.BoardColor = viewModel.BoardColor;

            if (viewModel.Image != null)
            {
                await _imageService.DeleteImageAsync("Cinemas", cinema.ImageUrl);
                cinema.ImageUrl = await _imageService.UploadPhotoAsync("Cinemas", viewModel.Image);
            }

            _context.Cinemas.Update(cinema);
            await _context.SaveChangesAsync();
            await _logger.LogActionAsync(UserActionType.Update, LogMessages.EditEntityMessage, "cinema", cinema.Name, "");
        }

        public async Task<bool> ExistsByIdAsync(int? id)
        {
            return await _context.Cinemas.AnyAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<CinemaListViewModel>> GetAllByUserAsync(string userEmail)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(userEmail);
            var cinemas = await _context.Cinemas.Include(i => i.Movies).Where(i => i.OwnerId == user.Id).ToListAsync();

            return cinemas.Select(i => new CinemaListViewModel
            {
                Id = i.Id,
                Name = i.Name,
                Status = (i.ApprovalStatus == ApprovalStatus.Approved ? "Approved" : (i.ApprovalStatus == ApprovalStatus.PendingApproval ? "Pending approval" : "Denied approval")),
                FoundedOn = i.FoundedOn,
                ImageUrl = i.ImageUrl,
                StatusCode = i.ApprovalStatus,
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
            if (cinema != null)
            {
                return new CinemaDetailsViewModel
                {
                    Id = cinema.Id,
                    Name = cinema.Name,
                    Description = cinema.Description,
                    FoundedOn = cinema.FoundedOn.ToString(Constants.DateTimeFormat),
                    ImageUrl = cinema.ImageUrl,
                    SeatRows = cinema.SeatRows,
                    SeatCols = cinema.SeatCols,
                    ApprovalStatus = cinema.ApprovalStatus,
                    Status = (cinema.ApprovalStatus == ApprovalStatus.Approved ? "Approved" : (cinema.ApprovalStatus == ApprovalStatus.PendingApproval ? "Pending approval" : "Denied approval"))
                };
            }
            return null;
        }

        public async Task<EditCinemaViewModel> GetEditViewModelByIdAsync(int? cinemaId)
        {
            var cinema = await _context.Cinemas.FirstOrDefaultAsync(i => i.Id == cinemaId);
            if (cinema != null)
            {
                return new EditCinemaViewModel
                {
                    Id = cinema.Id,
                    SeatCols = cinema.SeatCols,
                    SeatRows = cinema.SeatRows,
                    Description = cinema.Description,
                    FoundedOn = cinema.FoundedOn,
                    Image = null,
                    Name = cinema.Name,
                    AccentColor = cinema.AccentColor,
                    BackgroundColor = cinema.BackgroundColor,
                    BoardColor = cinema.BoardColor,
                    TextColor = cinema.TextColor,
                    ButtonBackgroundColor = cinema.ButtonBackgroundColor,
                    ButtonTextColor = cinema.ButtonTextColor
                };
            }
            return null;
        }

        public async Task<DeleteCinemaViewModel> PrepareDeleteViewModelAsync(int? id)
        {
            var cinema = await _context.Cinemas.FirstOrDefaultAsync(i => i.Id == id);
            if (cinema != null)
            {
                return new DeleteCinemaViewModel
                {
                    Id = cinema.Id,
                    Name = cinema.Name,
                    ImageUrl = cinema.ImageUrl
                };
            }
            return null;
        }

        public async Task<IEnumerable<CinemaListViewModel>> QueryCinemasAsync(string searchText, string filterValue, string sortBy, string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);

            var cinemas = _context.Cinemas.Include(i => i.Movies).Where(i => i.OwnerId == user.Id);
            if (string.IsNullOrEmpty(searchText) == false)
            {
                cinemas = cinemas.Where(i => i.Name.ToLower().StartsWith(searchText.ToLower()));
            }
            if (string.IsNullOrEmpty(filterValue) == false && Regex.IsMatch(filterValue, @"^[0-9]*$"))
            {
                int enumValue = int.Parse(filterValue);
                cinemas = cinemas.Where(i => i.ApprovalStatus == Enum.Parse<ApprovalStatus>(filterValue));
            }
            if (string.IsNullOrEmpty(sortBy) == false)
            {
                var sortParameter = sortBy.Split('-')[0];
                var sortDirection = sortBy.Split('-')[^1];

                switch (sortParameter)
                {
                    case "name":
                        cinemas = cinemas.OrderBy(i => i.Name);
                        if (sortDirection == "desc")
                        {
                            cinemas = cinemas.OrderByDescending(i => i.Name);
                        }
                        break;
                    case "status":
                        cinemas = cinemas.OrderBy(i => i.ApprovalStatus);
                        if (sortDirection == "desc")
                        {
                            cinemas = cinemas.OrderByDescending(i => i.ApprovalStatus);
                        }
                        break;
                    case "addedon":
                        cinemas = cinemas.OrderBy(i => i.FoundedOn);
                        if (sortDirection == "desc")
                        {
                            cinemas = cinemas.OrderByDescending(i => i.FoundedOn);
                        }
                        break;
                    case "count":
                        cinemas = cinemas.OrderBy(i => i.Movies.Count);
                        if (sortDirection == "desc")
                        {
                            cinemas = cinemas.OrderByDescending(i => i.Movies.Count);
                        }
                        break;
                }
            }
            else
            {
                cinemas = cinemas.OrderBy(i => i.Name);
            }
            return cinemas.Select(i => new CinemaListViewModel
            {
                Id = i.Id,
                Name = i.Name,
                Status = (i.ApprovalStatus == ApprovalStatus.Approved ? "Approved" : (i.ApprovalStatus == ApprovalStatus.PendingApproval ? "Pending approval" : "Denied approval")),
                FoundedOn = i.FoundedOn,
                ImageUrl = i.ImageUrl,
                StatusCode = i.ApprovalStatus,
                MoviesCount = i.Movies.Count
            }).ToList();
        }

        public async Task<IEnumerable<MovieInfoCardViewModel>> QueryMoviesByCinemaAsync(int? cinemaId, string searchText, string sortBy)
        {
            var cinema = await _context.Cinemas
                .Include(i => i.Movies)
                .ThenInclude(i => i.Movie)
                .ThenInclude(i => i.Genre)
                .Include(i => i.Movies).ThenInclude(i => i.Movie).ThenInclude(i => i.AddedBy)
                .FirstOrDefaultAsync(i => i.Id == cinemaId);
            var movies = cinema.Movies.Select(i => new MovieInfoCardViewModel
            {
                Id = i.Movie.Id,
                Name = i.Movie.Title,
                ImageUrl = i.Movie.ImageUrl,
                AverageRating = i.Movie.UserRating.Value,
                Genre = i.Movie.Genre != null ? i.Movie.Genre.Name : "No genre",
                RatingCount = i.Movie.RatingCount,
                AddedBy = $"{i.Movie.AddedBy.FirstName} {i.Movie.AddedBy.LastName}"
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
            return movies;
        }

        public async Task<CinemaPagePreviewViewModel> GetPreviewViewModelAsync(string userEmail, int? cinemaId)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            var cinema = await _context.Cinemas.FirstOrDefaultAsync(i => i.Id == cinemaId);
            var dates = Enumerable.Range(0, 7).Select(i => DateTime.Now.AddDays(i))
                .ToDictionary(key =>
                {
                    return key.Date != DateTime.Now.Date ? key.DayOfWeek.ToString().Substring(0, 3) : "Today";
                }, value => value.ToString(Constants.DateTimeFormat));
            if (user != null && cinema != null)
            {
                return new CinemaPagePreviewViewModel
                {
                    CinemaLogoUrl = cinema.ImageUrl,
                    ProfilePictureUrl = user.ProfilePictureUrl,
                    Dates = dates,
                    AccentColor = cinema.AccentColor,
                    BackgroundColor = cinema.BackgroundColor,
                    BoardColor = cinema.BoardColor,
                    ButtonBackgroundColor = cinema.ButtonBackgroundColor,
                    ButtonTextColor = cinema.ButtonTextColor,
                    TextColor = cinema.TextColor
                };
            }
            return null;
        }

        public async Task<IEnumerable<CinemaContainingMovieViewModel>> GetCinemasContainingMovieAsync(int? movieId, string userEmail, string sortBy)
        {
            var movie = await _context.Movies.Include(i => i.Cinemas).ThenInclude(i => i.Cinema).FirstOrDefaultAsync(i => i.Id == movieId);
            var user = await _userManager.FindByEmailAsync(userEmail);

            if (movie != null && user != null)
            {
                var cinemas = movie.Cinemas.Where(i => i.Cinema.OwnerId == user.Id);
                if (string.IsNullOrEmpty(sortBy) == false)
                {
                    var sortParameter = sortBy.Split('-')[0];
                    var sortDirection = sortBy.Split('-')[^1];

                    switch (sortParameter)
                    {
                        case "name":
                            cinemas = cinemas.OrderBy(i => i.Cinema.Name);
                            if (sortDirection == "desc")
                            {
                                cinemas = cinemas.OrderByDescending(i => i.Cinema.Name);
                            }
                            break;
                        case "fromDate":
                            cinemas = cinemas.OrderBy(i => i.FromDate);
                            if (sortDirection == "desc")
                            {
                                cinemas = cinemas.OrderByDescending(i => i.FromDate);
                            }
                            break;
                        case "toDate":
                            cinemas = cinemas.OrderBy(i => i.ToDate);
                            if (sortDirection == "desc")
                            {
                                cinemas = cinemas.OrderByDescending(i => i.ToDate);
                            }
                            break;
                        case "price":
                            cinemas = cinemas.OrderBy(i => i.TicketPrice);
                            if (sortDirection == "desc")
                            {
                                cinemas = cinemas.OrderByDescending(i => i.TicketPrice);
                            }
                            break;
                    }
                }
                else
                {
                    cinemas = cinemas.OrderBy(i => i.Cinema.Name);
                }
                return cinemas.Select(i => new CinemaContainingMovieViewModel
                {
                    Id = i.Cinema.Id,
                    MovieId = i.MovieId,
                    Name = i.Cinema.Name,
                    FromDate = i.FromDate.ToString(Constants.DateTimeFormat),
                    ToDate = i.ToDate.ToString(Constants.DateTimeFormat),
                    TicketPrice = i.TicketPrice,
                    CinemaLogoUrl = i.Cinema.ImageUrl
                }).ToList();
            }
            return null;
        }
    }
}