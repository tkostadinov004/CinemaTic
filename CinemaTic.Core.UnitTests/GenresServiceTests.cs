using CinemaTic.Core.Contracts;
using CinemaTic.Core.Services;
using CinemaTic.Data;
using CinemaTic.Data.Models;
using CinemaTic.ViewModels.Actors;
using CinemaTic.ViewModels.Genres;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTic.Core.UnitTests
{
    [TestFixture]
    public class GenresServiceTests
    {
        private CinemaDbContext _context;
        private IGenresService _genresService;
        [SetUp]
        public void TestInitialize()
        {
            var options = new DbContextOptionsBuilder<CinemaDbContext>()
                .UseInMemoryDatabase(databaseName: "CinemaTicInMemory")
                 .Options;
            this._context = new CinemaDbContext(options);

            _genresService = new GenresService(_context, new Mock<ILogService>().Object);
            var genre = new Genre
            {
                Id = 1,
                Name = "Adventure"
            };
            _context.Genres.Add(genre);
            _context.SaveChangesAsync();
        }
        [Test]
        public async Task AddGenreCorrectly()
        {
            var genre = new CreateGenreViewModel
            {
                Name = "Action"
            };
            await _genresService.CreateAsync(genre);

            Assert.That(_context.Genres.Any(i => i.Name == "Action"), Is.True);
        }
        [Test]
        public async Task DeletesGenreCorrectly()
        {
            await _genresService.DeleteByIdAsync(1);

            Assert.That(await _context.Genres.FirstOrDefaultAsync(i => i.Id == 1), Is.Null);
        }
        [Test]
        public async Task EditsGenreCorrectly()
        {
            var original = _context.Genres.AsNoTracking().FirstOrDefault(i => i.Id == 1);
            var viewModel = new EditGenreViewModel
            {
                Id = 1,
                Name = "Western"
            };
            await _genresService.EditAsync(viewModel);
            var edited = _context.Genres.AsNoTracking().FirstOrDefault(i => i.Id == 1);

            Assert.That(edited.Name, Is.Not.EqualTo(original.Name));
        }
        [Test]
        public async Task ChecksIfPresent()
        {
            bool exists = await _genresService.ExistsByIdAsync(1);

            Assert.True(exists);
        }
        [Test]
        public async Task GetsAllGenresCorrectly()
        {
            var genres = await _genresService.GetAllAsync();

            Assert.That(genres.Count(), Is.EqualTo(_context.Genres.Count()));
        }
        [Test]
        public async Task GetsByIdCorrectly()
        {
            var genre = await _genresService.GetByIdAsync(1);

            Assert.That(genre, Is.Not.Null);
        }
        [Test]
        public async Task Not_ThrowsExceptionOnInvalidId()
        {
            var genre = await _genresService.GetByIdAsync(null);

            Assert.That(genre, Is.Null);
            Assert.DoesNotThrowAsync(() => _genresService.GetByIdAsync(null));
        }
        [Test]
        public async Task GetsEditViewModelCorrectly()
        {
            int correctId = 1;
            int incorrectId = -1;
            Assert.Multiple(async () =>
            {
                Assert.That(await _genresService.GetEditViewModelByIdAsync(correctId), Is.Not.Null);
                Assert.That(await _genresService.GetEditViewModelByIdAsync(incorrectId), Is.Null);
            });
        }
        [Test]
        public async Task GetsDeleteViewModelCorrectly()
        {
            int correctId = 1;
            int incorrectId = -1;
            Assert.Multiple(async () =>
            {
                Assert.That(await _genresService.GetDeleteViewModelByIdAsync(correctId), Is.Not.Null);
                Assert.That(await _genresService.GetDeleteViewModelByIdAsync(incorrectId), Is.Null);
            });
        }
        [Test]
        public async Task GetsDetailsViewModelCorrectly()
        {
            int correctId = 1;
            int incorrectId = -1;
            Assert.Multiple(async () =>
            {
                Assert.That(await _genresService.GetDetailsViewModelByIdAsync(correctId), Is.Not.Null);
                Assert.That(await _genresService.GetDetailsViewModelByIdAsync(incorrectId), Is.Null);
            });
        }
        [Test]
        public async Task SortsGenresCorrectly()
        {
            _context.Genres.AddRange(new List<Genre>() {
            new Genre
            {
                Id = 2,
                Name = "Western"
            },
            new Genre
            {
                Id = 3,
                Name="Thriller"
            }
            });
            await _context.SaveChangesAsync();

            var genres = await _genresService.QueryGenresAsync("name-sort-desc", 1);
            Assert.That(genres.FirstOrDefault().Name, Is.EqualTo(_context.Genres.OrderByDescending(i => i.Name).FirstOrDefault().Name));
        }
        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
        }

    }
}
