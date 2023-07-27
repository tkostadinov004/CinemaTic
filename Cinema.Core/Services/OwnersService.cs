﻿using AutoMapper;
using Cinema.Core.Contracts;
using Cinema.Core.Utilities;
using Cinema.Data;
using Cinema.Data.Enums;
using Cinema.Data.Models;
using Cinema.ViewModels.Cinemas;
using Cinema.ViewModels.Contracts;
using Cinema.ViewModels.Movies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace Cinema.Core.Services
{
    public class OwnersService : IOwnersService
    {
        private readonly CinemaDbContext _context;
        private IWebHostEnvironment _webHostEnvironment;
        private UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ISectorsService _sectorsService;
        private readonly ILogService _logger;

        public OwnersService(CinemaDbContext context, IWebHostEnvironment webHostEnvironment, UserManager<ApplicationUser> userManager, IMapper mapper, ISectorsService sectorsService, ILogService logger)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
            _mapper = mapper;
            _sectorsService = sectorsService;
            _logger = logger;
        }

        public async Task AddAsync(IViewModel item, string userEmail)
        {
            var viewModel = item as AddCinemaViewModel;
            string imageUrl = GlobalMethods.UploadPhoto("Cinemas", viewModel.Image, _webHostEnvironment);

            var cinema = _mapper.Map<Data.Models.Cinema>(viewModel);
            cinema.ImageUrl = imageUrl;
            cinema.Owner = await _userManager.FindByEmailAsync(userEmail);
            cinema.Sectors = await _sectorsService.DefineSectorsAsync(cinema.SeatRows, cinema.SeatCols);

            _context.Cinemas.Add(cinema);
            await _context.SaveChangesAsync();
            await _logger.LogActionAsync(UserActionType.Create, LogMessages.AddEntityMessage, "cinema", viewModel.Name, $"({viewModel.SeatRows} rows and {viewModel.SeatCols} columns)");
        }
        public async Task AddMovieToCinemas(MovieDetailsViewModel viewModel)
        {
            var movie = await _context.Movies.Include(i => i.Cinemas).FirstOrDefaultAsync(i => i.Id == viewModel.MovieId);
            foreach(var cinemaViewModel in viewModel.UserCinemas)
            {
                var cinema = await _context.Cinemas.FirstOrDefaultAsync(i => i.Id == cinemaViewModel.Id);
                if (cinemaViewModel.FromDate != default && cinemaViewModel.ToDate != default && cinemaViewModel.TicketPrice > 0)
                {
                    if (!movie.Cinemas.Any(i => i.CinemaId == cinema.Id))
                    {
                        movie.Cinemas.Add(new CinemaMovie
                        {
                            CinemaId = cinema.Id,
                            FromDate = cinemaViewModel.FromDate,
                            ToDate = cinemaViewModel.ToDate,
                            TicketPrice = cinemaViewModel.TicketPrice
                        });
                    }
                }
                await _logger.LogActionAsync(UserActionType.Create, LogMessages.AddMovieToCinemaMessage, movie.Title, cinema.Name);
            }
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int? id)
        {
            var cinema = await _context.Cinemas.FindAsync(id);
            _context.Cinemas.Remove(cinema);
            await GlobalMethods.DeleteImage("Cinemas", cinema.ImageUrl, _context, _webHostEnvironment);
            await _context.SaveChangesAsync();
            await _logger.LogActionAsync(UserActionType.Delete, LogMessages.DeleteEntityMessage, "cinema", cinema.Name, $"({cinema.SeatRows} rows and {cinema.SeatCols} columns)");
        }

        public async Task EditCinema(IViewModel item)
        {
            var viewModel = item as EditCinemaViewModel;
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

            if (viewModel.Image != null)
            {
                await GlobalMethods.DeleteImage("Cinemas", cinema.ImageUrl, _context, _webHostEnvironment);
                cinema.ImageUrl = GlobalMethods.UploadPhoto("Cinemas", viewModel.Image, _webHostEnvironment);
            }

            _context.Cinemas.Update(cinema);
            await _context.SaveChangesAsync();
            await _logger.LogActionAsync(UserActionType.Update, LogMessages.EditEntityMessage, "cinema", cinema.Name, "");
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
                Status = (i.ApprovalStatus == ApprovalStatus.Approved ? "Approved" : (i.ApprovalStatus == ApprovalStatus.PendingApproval ? "Pending approval" : "Denied approval")),
                FoundedOn = i.FoundedOn.ToString(Constants.DateTimeFormat),
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

            var cinemasCustomers = cinemas.ToDictionary(cinema => cinema.Name, value => value.Customers.Count);
            var cinemasTotalRevenues = cinemas.ToDictionary(cinema => cinema.Name, value => value.Tickets.Sum(i => i.Price));
            return new OwnerStatisticsViewModel
            {
                PersonalIncome = personalIncome,
                TotalIncome = totalIncome,
                CinemasCustomers = cinemasCustomers,
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

        public async Task<IEnumerable<CinemaListViewModel>> SearchAndFilterCinemasAsync(string searchText, string userEmail, string filterValue, string sortBy)
        {
            var cinemas = await this.GetAllByUserAsync(userEmail);
            if (string.IsNullOrEmpty(searchText) == false)
            {
                cinemas = cinemas.Where(i => i.Name.ToLower().StartsWith(searchText.ToLower()));
            }
            if (string.IsNullOrEmpty(filterValue) == false && Regex.IsMatch(filterValue, @"^[0-9]*$"))
            {
                int enumValue = int.Parse(filterValue);
                switch (enumValue)
                {
                    case (int)ApprovalStatus.Approved:
                        cinemas = cinemas.Where(i => i.Status == "Approved");
                        break;
                    case (int)ApprovalStatus.PendingApproval:
                        cinemas = cinemas.Where(i => i.Status == "Pending approval");
                        break;
                    case (int)ApprovalStatus.DeniedApproval:
                        cinemas = cinemas.Where(i => i.Status == "Denied approval");
                        break;
                }
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
                        cinemas = cinemas.OrderBy(i => i.StatusCode);
                        if (sortDirection == "desc")
                        {
                            cinemas = cinemas.OrderByDescending(i => i.StatusCode);
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
                        cinemas = cinemas.OrderBy(i => i.MoviesCount);
                        if (sortDirection == "desc")
                        {
                            cinemas = cinemas.OrderByDescending(i => i.MoviesCount);
                        }
                        break;
                }
            }
            else
            {
                cinemas = cinemas.OrderBy(i => i.Status);
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

        public async Task<CinemaPagePreviewViewModel> PreparePreviewViewModelAsync(string userEmail, string cinemaId)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            var cinema = await _context.Cinemas.FirstOrDefaultAsync(i => i.Id == int.Parse(cinemaId));
            var dates = Enumerable.Range(0, 7).Select(i => DateTime.Now.AddDays(i))
                .ToDictionary(key =>
                {
                    return key.Date != DateTime.Now.Date ? key.DayOfWeek.ToString().Substring(0, 3) : "Today";
                }, value => value.ToString(Constants.DateTimeFormat));
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

        public async Task<IEnumerable<CinemaListViewModel>> GetCinemasContainingMovieAsync(string movieId, string userEmail)
        {
            var movie = await _context.Movies.FirstOrDefaultAsync(i => i.Id == int.Parse(movieId));
            var user = await _userManager.FindByEmailAsync(userEmail);

            return await _context.Cinemas.Include(i => i.Movies).Where(i => i.OwnerId == user.Id && i.Movies.Any(m => m.MovieId == movie.Id)).Select(i => new CinemaListViewModel
            {
                Id = i.Id,
                Name = i.Name,
                ImageUrl = i.ImageUrl,
                FoundedOn = i.FoundedOn.ToString(Constants.DateTimeFormat),
                MoviesCount = i.Movies.Count()
            }).ToListAsync();
        }
    }
}