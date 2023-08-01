using CinemaTic.Core.Contracts;
using CinemaTic.Core.Services;
using CinemaTic.Data;
using CinemaTic.Data.Models;
using CinemaTic.ViewModels.Actors;
using CinemaTic.ViewModels.Movies;
using Microsoft.AspNetCore.Http;
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

namespace CinemaTic.Core.UnitTests
{
    [TestFixture]
    public class MoviesServiceTests
    {
        private CinemaDbContext _context;
        private IMoviesService _moviesService;
        private Mock<UserManager<ApplicationUser>> _userManagerMock;
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
            _moviesService = new MoviesService(_context, _userManagerMock.Object, new Mock<ILogService>().Object, new Mock<IImageService>().Object);

            _context.Movies.Add(new Movie
            {
                Id = 1,
                Description = "A sample description",
                RunningTime = 120,
                Title = "Test movie",
                ImageUrl = "imageurl",
                TrailerUrl = "testurl",
                UserRating = 0,
                GenreId = new Genre { Id = 1, Name = "Test" }.Id,
                Genre = new Genre { Id = 1, Name = "Test" },
                AddedById = "156fc675-02de-4250-9edb-869c85e13e61",
                AddedBy = new ApplicationUser("156fc675-02de-4250-9edb-869c85e13e61", 0, "8f06bfbc-e1e3-4f95-9bc1-30add0031c34", new DateTime(2023, 7, 29, 12, 59, 18, 234, DateTimeKind.Local).AddTicks(7086), "owner1@owner.com", true, "James", "Johnson", false, null, "OWNER1@OWNER.COM", "OWNER1@OWNER.COM", null, null, false, "profilePicURL", "1969a695-01c0-49be-8d42-482cb1c327bc", false, "owner1@owner.com"),
                Director = "Director Test",
                Actors = new List<ActorMovie>(),
                Cinemas = new List<Cinema.Data.Models.CinemaMovie>(),
            });
            _context.SaveChanges();
        }
        [Test]
        public async Task CreatesMovieCorrectly()
        {
            var bytes = Encoding.UTF8.GetBytes("An image");
            IFormFile picture = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "actor.jpg");
            var viewModel = new CreateMovieViewModel
            {
                Title = "TestMovie",
                Description = "A description of a movie",
                Director = "Test Director",
                GenreId = new Genre { Id = 1, Name = "Test" }.Id,
                TrailerUrl = "asdadadasd",
                RunningTime = 150,
                Image = picture,
                ActorsDropdown = new List<ActorDropdownViewModel> { }
            };
            var user = await _context.Users.FirstOrDefaultAsync(i => i.Id == "156fc675-02de-4250-9edb-869c85e13e61");
            _userManagerMock.Setup(userManager => userManager.FindByEmailAsync(user.Email)).ReturnsAsync(user);
            await _moviesService.CreateMovieAsync(viewModel, "owner1@owner.com");

            Assert.That(_context.Movies.Count(), Is.GreaterThan(0));
        }
        [Test]
        public async Task DeletesMovieCorrectly()
        {
            int id = (await _context.Movies.FirstOrDefaultAsync(i => i.Id == 1)).Id;
            await _moviesService.DeleteByIdAsync(id);

            Assert.That(_context.Movies.FirstOrDefault(i => i.Id == id), Is.Null);
        }
        [Test]
        public async Task EditsMovieCorrectly()
        {
            var original = _context.Movies.AsNoTracking().FirstOrDefault(i => i.Id == 1);
            var viewModel = new EditMovieViewModel
            {
                Id = 1,
                Title = "A movie",
                GenreId = new Genre { Id = 1, Name = "Test" }.Id,
                Description = "Desc",
                RunningTime = "123",
                TrailerUrl = "trailer",
                ActorsDropdown = new List<ActorDropdownViewModel> { }
            };
            await _moviesService.EditMovieAsync(viewModel);
            var edited = _context.Movies.AsNoTracking().FirstOrDefault(i => i.Id == 1);
            Assert.Multiple(() =>
            {
                Assert.That(edited.Title, Is.EqualTo(viewModel.Title));
                Assert.That(edited.Description, Is.EqualTo(viewModel.Description));
                Assert.That(edited.RunningTime, Is.EqualTo(decimal.Parse(viewModel.RunningTime)));
                Assert.That(edited.TrailerUrl, Is.EqualTo(viewModel.TrailerUrl));
            });
        }
        [Test]
        public async Task ExistsByIdCorrectly()
        {
            var correctId = 1;
            var incorrectId = -1;

            bool correctExists = await _moviesService.ExistsByIdAsync(correctId);
            bool incorrectExists = await _moviesService.ExistsByIdAsync(incorrectId);
            Assert.Multiple(() =>
            {
                Assert.That(correctExists, Is.True);
                Assert.That(incorrectExists, Is.False);
            });
        }
        [Test]
        public async Task GetsAllCorrectly()
        {
            var movies = await _moviesService.GetAllAsync();
            Assert.That(movies.Count(), Is.EqualTo(_context.Movies.Count()));
        }
        [Test]
        public async Task GetsByIdCorrectly()
        {
            var id = 1;
            var movie = await _moviesService.GetByIdAsync(id);
            
            Assert.NotNull(movie);
        }
        [Test]
        public async Task GetsActorDropdownCorrectly()
        {
            _context.Actors.Add(new Data.Models.Actor
            {
                Id = 1,
                FullName = "Test",
                Nationality = "Bulgaria",
                Birthdate = DateTime.Now,
                ImageUrl = "Image.jpg",
                Rating = 5.0m
            });
            _context.Actors.Add(new Data.Models.Actor
            {
                Id = 2,
                FullName = "George Jameson",
                Nationality = "United States",
                Birthdate = DateTime.Now,
                ImageUrl = "Image2.jpg",
                Rating = 6.0m
            });
            _context.SaveChanges();

            var dropdown = await _moviesService.GetActorsDropdownAsync();
            Assert.That(dropdown.Count(), Is.EqualTo(_context.Actors.Count()));
        }
        [Test]
        public async Task DoesNotThrowExceptionOnNullIds()
        {
            Assert.Multiple(async () =>
            {
                Assert.That(await _moviesService.GetDetailsViewModelAsync(null, ""), Is.Null);
            });
        }
        [Test]
        public async Task GetsGenresDropdownCorrectly()
        {
            _context.Genres.AddRange(new List<Genre>()
            {
                new Genre(){Id = 2, Name = "Adventure"}
            });
            await _context.SaveChangesAsync();

            var dropdown = await _moviesService.GetGenresDropdownAsync();
            Assert.That(dropdown.Count(), Is.EqualTo(_context.Genres.Count()));
        }
        [Test]
        public async Task GetsCreateViewModelCorrectly()
        {
            Assert.That(await _moviesService.GetCreateViewModelAsync(), Is.Not.Null);
        }
        [Test]
        public async Task GetsFilterViewModelCorrectly()
        {
            Assert.That(await _moviesService.GetFilterViewModelAsync(), Is.Not.Null);
        }
        [Test]
        public async Task GetsEditViewModelCorrectly()
        {
            int correctId = 1;
            int incorrectId = -1;
            Assert.Multiple(async () =>
            {
                Assert.That(await _moviesService.GetEditViewModelAsync(correctId), Is.Not.Null);
                Assert.That(await _moviesService.GetEditViewModelAsync(incorrectId), Is.Null);
            });
        }
        [Test]
        public async Task GetsDeleteViewModelCorrectly()
        {
            int correctId = 1;
            int incorrectId = -1;
            Assert.Multiple(async () =>
            {
                Assert.That(await _moviesService.GetDeleteViewModelAsync(correctId), Is.Not.Null);
                Assert.That(await _moviesService.GetDeleteViewModelAsync(incorrectId), Is.Null);
            });
        }
        [Test]
        public async Task SearchesAndFiltersMoviesCorrectly()
        {
            _context.Movies.Add(new Movie
            {
                Id = 2,
                Description = "A sample description",
                RunningTime = 123,
                Title = "Test movie 1",
                ImageUrl = "imageurl",
                TrailerUrl = "testurl",
                UserRating = 0,
                GenreId = 1,
                AddedById = "156fc675-02de-4250-9edb-869c85e13e61",
                Director = "Director Test2",
                Actors = new List<ActorMovie>(),
                Cinemas = new List<Cinema.Data.Models.CinemaMovie>(),
            });
            _context.Movies.Add(new Movie
            {
                Id = 3,
                Description = "A sample description",
                RunningTime = 123,
                Title = "A movie 1",
                ImageUrl = "imageurl",
                TrailerUrl = "testurl",
                UserRating = 0,
                GenreId = 1,
                AddedById = "156fc675-02de-4250-9edb-869c85e13e61",
                Director = "Director Test2",
                Actors = new List<ActorMovie>(),
                Cinemas = new List<Cinema.Data.Models.CinemaMovie>(),
            });
            _context.SaveChanges();

            var movies = await _moviesService.SearchAndFilterMoviesAsync("test", "", "name-sort-desc", null);
            Assert.That(movies.FirstOrDefault().Id, Is.EqualTo(2));
        }
        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
        }
    }
}
