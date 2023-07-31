using Cinema.Core.Contracts;
using Cinema.Core.Services;
using Cinema.Data;
using Cinema.Data.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Core.UnitTests
{
    [TestFixture]
    public class SectorsServiceTests
    {
        private CinemaDbContext _context;
        private ISectorsService _sectorsService;
        [SetUp]
        public void TestInitialize()
        {
            var options = new DbContextOptionsBuilder<CinemaDbContext>()
               .UseInMemoryDatabase(databaseName: "CinemaTicInMemory")
                .Options;
            this._context = new CinemaDbContext(options);
            _sectorsService = new SectorsService(_context);
            _context.Cinemas.Add(new Data.Models.Cinema
            {
                Name = "TestCinema",
                Description = "A description",
                SeatRows = 50,
                SeatCols = 30,
                FoundedOn = DateTime.Now,
                AccentColor = "#000000",
                BackgroundColor = "#000000",
                BoardColor = "#000000",
                ButtonBackgroundColor = "#000000",
                ButtonTextColor = "#000000",
                ImageUrl = "test.jpg",
                TextColor = "#000000",
                Id = 1,
                OwnerId = "156fc675-02de-4250-9edb-869c85e13e61",
                Sectors = _sectorsService.DefineSectorsAsync(50, 30).Result
            });
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
                Cinemas = new List<Cinema.Data.Models.CinemaMovie>() { new CinemaMovie() {CinemaId = 1, MovieId = 1 } },
            });
            _context.Sectors.Add(new Data.Models.Sector
            {
                Id = 15,
                CinemaId = 1,
                StartCol = 1,
                EndCol = 15,
                StartRow =1,
                EndRow = 10,
                SectorName = "A"
            });
            _context.SaveChanges();
        }
        [Test]
        public async Task GetsSectorsCorrectly()
        {
            int rows = 50;
            int cols = 30;

            var sectors = await _sectorsService.DefineSectorsAsync(rows, cols);
            Assert.That(sectors.Count, Is.EqualTo(10));
        }
        [Test]
        public async Task DeletesSectorsCorrectly()
        {
            int cinemaId = 1;
            await _sectorsService.DeleteSectorsAsync(cinemaId);

            Assert.That(_context.Cinemas.Include(i => i.Sectors).FirstOrDefault(i => i.Id == cinemaId).Sectors.Count, Is.EqualTo(0));
        }
        [Test]
        public async Task ExistsByIdCorrectly()
        {
            var correctId = 15;
            var incorrectId = -1;

            bool correctExists = await _sectorsService.ExistsByIdAsync(correctId);
            bool incorrectExists = await _sectorsService.ExistsByIdAsync(incorrectId);
            Assert.Multiple(() =>
            {
                Assert.That(correctExists, Is.True);
                Assert.That(incorrectExists, Is.False);
            });
        }
        [Test]
        public async Task GetsSectorGridCorrectly()
        {
            var layout = await _sectorsService.GetCinemaSectorsGridAsync(1, 1, default);
            Assert.Multiple(async () =>
            {
                Assert.That(await _sectorsService.GetCinemaSectorsGridAsync(-1, 0, default), Is.Null);
                Assert.That(layout, Is.Not.Null);
                Assert.That(layout.ForDateTime, Is.EqualTo(DateTime.Today));
                Assert.That(layout.Sectors.Count(), Is.EqualTo(_context.Sectors.Where(i => i.CinemaId == 1).Count()));
            });
        }
        [Test]
        public async Task GetsByIdCorrectly()
        {
            int correctId = 1;
            int incorrectId = -1;
            Assert.Multiple(async () =>
            {
                Assert.That(await _sectorsService.GetSectorByIdAsync(correctId, 1, DateTime.Today), Is.Not.Null);
                Assert.That(await _sectorsService.GetSectorByIdAsync(incorrectId, 1, DateTime.Today), Is.Null);
            });
        }
        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
        }
    }
}
