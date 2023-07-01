using Cinema.Core.Contracts;
using Cinema.Data;
using Cinema.Data.Models;
using Cinema.Utilities;
using Cinema.ViewModels.Cinemas;
using Cinema.ViewModels.Contracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;

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

        public Task DeleteByIdAsync(int? id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsByIdAsync(int? id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Data.Models.Cinema>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Data.Models.Cinema> GetByIdAsync(int? id)
        {
            throw new NotImplementedException();
        }
    }
}
