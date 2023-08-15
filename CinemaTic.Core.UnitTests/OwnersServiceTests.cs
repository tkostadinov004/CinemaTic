using CinemaTic.Data.Models;
using CinemaTic.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaTic.Core.Contracts;
using CinemaTic.Core.Services;
using AutoMapper;
using CinemaTic.ViewModels.Cinemas;
using Microsoft.AspNetCore.Http;
using CinemaTic.ViewModels.Actors;
using CinemaTic.ViewModels.Movies;

namespace CinemaTic.Core.UnitTests
{
    [TestFixture]
    public class OwnersServiceTests
    {
        private CinemaDbContext _context;
        private Mock<UserManager<ApplicationUser>> _userManagerMock;
        private ICinemasService _ownersService;
        private Mock<IImageService> _imageServiceMock;
        [SetUp]
        public void TestInitialize()
        {

            var options = new DbContextOptionsBuilder<CinemaDbContext>()
                .UseInMemoryDatabase(databaseName: "CinemaTicInMemory")
                 .Options;
            this._context = new CinemaDbContext(options);
            _userManagerMock = new Mock<UserManager<ApplicationUser>>(
   new Mock<IUserStore<ApplicationUser>>().Object,
   new Mock<IOptions<IdentityOptions>>().Object,
   new Mock<IPasswordHasher<ApplicationUser>>().Object,
   new IUserValidator<ApplicationUser>[0],
   new IPasswordValidator<ApplicationUser>[0],
   new Mock<ILookupNormalizer>().Object,
   new Mock<IdentityErrorDescriber>().Object,
   new Mock<IServiceProvider>().Object,
   new Mock<ILogger<UserManager<ApplicationUser>>>().Object);
            _imageServiceMock = new Mock<IImageService>();
            _ownersService = new CinemasService(_context, _userManagerMock.Object, new Mock<IMapper>().Object, new Mock<ISectorsService>().Object, new Mock<ILogService>().Object, _imageServiceMock.Object);
            _context.Users.Add(new ApplicationUser("156fc675-02de-4250-9edb-869c85e13e61", 0, "8f06bfbc-e1e3-4f95-9bc1-30add0031c34", new DateTime(2023, 7, 29, 12, 59, 18, 234, DateTimeKind.Local).AddTicks(7086), "owner1@owner.com", true, "James", "Johnson", false, null, "OWNER1@OWNER.COM", "OWNER1@OWNER.COM", null, null, false, "profilePicURL", "1969a695-01c0-49be-8d42-482cb1c327bc", false, "owner1@owner.com"));
            _context.Cinemas.Add(new Data.Models.Cinema
            {
                Name = "TestCinema",
                Description = "A description",
                SeatRows = 10,
                SeatCols = 10,
                FoundedOn = DateTime.Now,
                AccentColor = "#000000",
                BackgroundColor = "#000000",
                BoardColor = "#000000",
                ButtonBackgroundColor = "#000000",
                ButtonTextColor = "#000000",
                ImageUrl = "test.jpg",
                TextColor = "#000000",
                Id = 1,
                OwnerId = "156fc675-02de-4250-9edb-869c85e13e61"                
            });
            _context.SaveChanges();
        }
        [Test]
        public async Task AddsCinemaCorrectly()
        {
            var bytes = Encoding.UTF8.GetBytes("An image");
            IFormFile picture = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "Cinema.jpg");
            var viewModel = new CreateCinemaViewModel
            {
                Name = "TestCinema",
                Description = "A description",
                SeatRows = "10",
                SeatCols = "10",
                FoundedOn = DateTime.Now,
                AccentColor = "#000000",
                BackgroundColor = "#000000",
                BoardColor = "#000000",
                ButtonBackgroundColor = "#000000",
                ButtonTextColor = "#000000",
                Image = picture,
                TextColor = "#000000"
            };

            var user = await _context.Users.FirstOrDefaultAsync(i => i.Id == "156fc675-02de-4250-9edb-869c85e13e61");
            _userManagerMock.Setup(userManager => userManager.FindByEmailAsync(user.Email)).ReturnsAsync(user);
            await _ownersService.CreateCinemaAsync(viewModel, "owner1@owner.com");

            Assert.That(_context.Cinemas.Count(), Is.GreaterThan(0));
        }
        [Test]
        public async Task DeletesCinemaCorrectly()
        {
            int id = 1;
            await _ownersService.DeleteByIdAsync(id);

            Assert.That(_context.Cinemas.FirstOrDefault(i => i.Id == id), Is.Null);
        }
        [Test]
        public async Task EditsCinemaCorrectly()
        {
            var original = _context.Cinemas.AsNoTracking().FirstOrDefault(i => i.Id == 1);
            var viewModel = new EditCinemaViewModel
            {
                Id = 1,
                Name = "TestCinema1",
                Description = "A description1",
                SeatRows = 15,
                SeatCols = 15,
                FoundedOn = DateTime.Now,
                AccentColor = "#ffffff",
                BackgroundColor = "#ffffff",
                BoardColor = "#ffffff",
                ButtonBackgroundColor = "#ffffff",
                ButtonTextColor = "#ffffff",
                TextColor = "#ffffff"
            };
            await _ownersService.EditCinemaAsync(viewModel);
            var edited = _context.Cinemas.AsNoTracking().FirstOrDefault(i => i.Id == 1);
            Assert.Multiple(() =>
            {
                Assert.That(edited.Name, Is.EqualTo(viewModel.Name));
                Assert.That(edited.Description, Is.EqualTo(viewModel.Description));
                Assert.That(edited.SeatRows, Is.EqualTo(viewModel.SeatRows));
                Assert.That(edited.SeatCols, Is.EqualTo(viewModel.SeatCols));
                Assert.That(edited.FoundedOn, Is.EqualTo(viewModel.FoundedOn));
                Assert.That(edited.AccentColor, Is.EqualTo(viewModel.AccentColor));
                Assert.That(edited.BackgroundColor, Is.EqualTo(viewModel.BackgroundColor));
                Assert.That(edited.BoardColor, Is.EqualTo(viewModel.BoardColor));
                Assert.That(edited.ButtonBackgroundColor, Is.EqualTo(viewModel.ButtonBackgroundColor));
                Assert.That(edited.ButtonTextColor, Is.EqualTo(viewModel.ButtonTextColor));
                Assert.That(edited.TextColor, Is.EqualTo(viewModel.TextColor));
            });
        }
        [Test]
        public async Task ExistsByIdCorrectly()
        {
            var correctId = 1;
            var incorrectId = -1;

            bool correctExists = await _ownersService.ExistsByIdAsync(correctId);
            bool incorrectExists = await _ownersService.ExistsByIdAsync(incorrectId);
            Assert.Multiple(() =>
            {
                Assert.That(correctExists, Is.True);
                Assert.That(incorrectExists, Is.False);
            });
        }
        [Test]
        public async Task GetsAllCinemasByUserAsync()
        {
            var user = await _context.Users.FirstOrDefaultAsync(i => i.Id == "156fc675-02de-4250-9edb-869c85e13e61");
            _userManagerMock.Setup(userManager => userManager.FindByEmailAsync(user.Email)).ReturnsAsync(user);

            var cinemas = await _ownersService.GetAllByUserAsync(user.Email);

            Assert.That(cinemas.Count(), Is.EqualTo(_context.Cinemas.Where(i => i.OwnerId == user.Id).Count()));
        }
        [Test]
        public async Task GetsByIdCorrectly()
        {
            int correctId = 1;
            int incorrectId = -1;
            Assert.Multiple(async () =>
            {
                Assert.That(await _ownersService.GetByIdAsync(correctId), Is.Not.Null);
                Assert.That(await _ownersService.GetByIdAsync(incorrectId), Is.Null);
            });
        }
        [Test]
        public async Task GetsEditViewModelCorrectly()
        {
            int correctId = 1;
            int incorrectId = -1;
            Assert.Multiple(async () =>
            {
                Assert.That(await _ownersService.GetEditViewModelByIdAsync(correctId), Is.Not.Null);
                Assert.That(await _ownersService.GetEditViewModelByIdAsync(incorrectId), Is.Null);
            });
        }
        [Test]
        public async Task GetsDeleteViewModelCorrectly()
        {
            int correctId = 1;
            int incorrectId = -1;
            Assert.Multiple(async () =>
            {
                Assert.That(await _ownersService.PrepareDeleteViewModelAsync(correctId), Is.Not.Null);
                Assert.That(await _ownersService.PrepareDeleteViewModelAsync(incorrectId), Is.Null);
            });
        }
        [Test]
        public async Task SearchesAndFiltersCinemasCorrectly()
        {
            _context.Cinemas.Add(new Data.Models.Cinema
            {
                Name = "CinKTest",
                Description = "A description",
                SeatRows = 10,
                SeatCols = 10,
                FoundedOn = DateTime.Now,
                AccentColor = "#000000",
                BackgroundColor = "#000000",
                BoardColor = "#000000",
                ButtonBackgroundColor = "#000000",
                ButtonTextColor = "#000000",
                ImageUrl = "test.jpg",
                TextColor = "#000000",
                Id = 2,
                OwnerId = "156fc675-02de-4250-9edb-869c85e13e61"
            });
            _context.Cinemas.Add(new Data.Models.Cinema
            {
                Name = "CinemaTest",
                Description = "A description",
                SeatRows = 10,
                SeatCols = 10,
                FoundedOn = DateTime.Now,
                AccentColor = "#000000",
                BackgroundColor = "#000000",
                BoardColor = "#000000",
                ButtonBackgroundColor = "#000000",
                ButtonTextColor = "#000000",
                ImageUrl = "test.jpg",
                TextColor = "#000000",
                Id = 3,
                OwnerId = "156fc675-02de-4250-9edb-869c85e13e61"
            });
            _context.SaveChanges();

            var user = await _context.Users.FirstOrDefaultAsync(i => i.Id == "156fc675-02de-4250-9edb-869c85e13e61");
            _userManagerMock.Setup(userManager => userManager.FindByEmailAsync(user.Email)).ReturnsAsync(user);

            var cinemas = await _ownersService.QueryCinemasAsync("cin", "", "name-sort-desc", user.Email);
            Assert.That(cinemas.FirstOrDefault().Id, Is.EqualTo(2));
        }
        [Test]
        public async Task GetsPreviewViewModelCorrectly()
        {
            var user = await _context.Users.FirstOrDefaultAsync(i => i.Id == "156fc675-02de-4250-9edb-869c85e13e61");
            _userManagerMock.Setup(userManager => userManager.FindByEmailAsync(user.Email)).ReturnsAsync(user);

            Assert.Multiple(async () =>
            {
                Assert.That(await _ownersService.GetPreviewViewModelAsync(user.Email, 1), Is.Not.Null);
                Assert.That(await _ownersService.GetPreviewViewModelAsync(user.Email, -1), Is.Null);
                Assert.That(await _ownersService.GetPreviewViewModelAsync("", -1), Is.Null);
            });
        }
        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
        }
    }
}
