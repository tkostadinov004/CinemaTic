using CinemaTic.Core.Contracts;
using CinemaTic.Core.Services;
using CinemaTic.Data;
using CinemaTic.ViewModels.Actors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTic.Core.UnitTests
{
    [TestFixture]
    public class ActorsServiceTests
    {
        private CinemaDbContext _context;
        private IActorsService _actorsService;
        private Mock<ILogService> logMock;
        private Mock<IImageService> imageMock;
        [SetUp]
        public void TestInitialize()
        {
            var options = new DbContextOptionsBuilder<CinemaDbContext>()
                .UseInMemoryDatabase(databaseName: "CinemaTicInMemory")
                 .Options;
            this._context = new CinemaDbContext(options);
            logMock = new Mock<ILogService>();
            imageMock = new Mock<IImageService>();
            imageMock.Setup(i => i.UploadPhotoAsync("Actors", It.IsAny<IFormFile>())).ReturnsAsync(It.IsNotNull<string>());
            imageMock.Setup(i => i.UploadPhotoAsync("Actors", null)).Throws<ArgumentNullException>();
            imageMock.Setup(i => i.DeleteImageAsync("Actors", It.IsAny<string>())).ReturnsAsync(true);
            imageMock.Setup(i => i.DeleteImageAsync("Actors", null)).ReturnsAsync(false);
            imageMock.Setup(i => i.DeleteImageAsync("Actors", "asdasd")).ReturnsAsync(false);

            _actorsService = new ActorsService(_context, logMock.Object, imageMock.Object);

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
            _context.Actors.Add(new Data.Models.Actor
            {
                Id = 3,
                FullName = "Ivan Peterson",
                Nationality = "Sweden",
                Birthdate = DateTime.Now,
                ImageUrl = "Image3.jpg",
                Rating = 2.0m
            });
            _context.Actors.Add(new Data.Models.Actor
            {
                Id = 4,
                FullName = "Ivan Petrovic",
                Nationality = "Serbia",
                Birthdate = DateTime.ParseExact("02/09/1982", "MM/dd/yyyy", null),
                ImageUrl = "Image3.jpg",
                Rating = 7.0m
            });
            _context.SaveChanges();
        }

        [Test]
        public async Task Adds_Actor_Correctly()
        {
            var bytes = Encoding.UTF8.GetBytes("An image");
            IFormFile picture = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "actor.jpg");
            CreateActorViewModel vm = new CreateActorViewModel
            {
                FullName = "John Jameson",
                Birthdate = DateTime.ParseExact("07/12/1982", "MM/dd/yyyy", null),
                Nationality = "United States",
                Rating = "5",
                Image = picture
            };

            await _actorsService.CreateActorAsync(vm);

            Assert.That(_context.Actors.Any(i => i.FullName == "John Jameson"), Is.True);
        }
        [Test]
        public async Task Throws_ExceptionOnNullImage()
        {
            CreateActorViewModel vm = new CreateActorViewModel
            {
                FullName = "John Jameson",
                Birthdate = DateTime.ParseExact("07/12/1982", "MM/dd/yyyy", null),
                Nationality = "United States",
                Rating = "5",
                Image = null
            };
            
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _actorsService.CreateActorAsync(vm));
        }
        [Test]
        public async Task DeletesByIdCorrectly()
        {
            await _actorsService.DeleteByIdAsync(1);

            Assert.That(_context.Actors.FirstOrDefault(i => i.Id == 1), Is.Null);
        }
        [Test]
        public async Task EditsActorCorrectly()
        {
            var original = _context.Actors.AsNoTracking().FirstOrDefault(i => i.Id == 1);
            var viewModel = new EditActorViewModel
            {
                Id = 1,
                FullName = "NewName",
                Nationality = "United Kingdom",
                Rating = 10m,
                Birthdate = DateTime.ParseExact("06/16/2001", "MM/dd/yyyy", null)
            };
            await _actorsService.EditActorAsync(viewModel);
            var edited = _context.Actors.AsNoTracking().FirstOrDefault(i => i.Id == 1);
            Assert.Multiple(() =>
            {
                Assert.That(edited.FullName, Is.EqualTo(viewModel.FullName));
                Assert.That(edited.Nationality, Is.EqualTo(viewModel.Nationality));
                Assert.That(edited.Rating, Is.EqualTo(viewModel.Rating));
                Assert.That(edited.Birthdate, Is.EqualTo(viewModel.Birthdate));
            });
        }
        [Test]
        public async Task ChecksIfPresent()
        {
            bool exists = await _actorsService.ExistsByIdAsync(1);

            Assert.True(exists);
        }
        [Test]
        public async Task ReturnsFalseIfNotPresent()
        {
            bool exists = await _actorsService.ExistsByIdAsync(-1);

            Assert.That(exists, Is.False);
        }
        [Test]
        public async Task GetsByIdCorrectly()
        {
            var correctId = 1;
            var incorrectId = -1;

            var correct = await _actorsService.GetByIdAsync(correctId);
            var incorrect = await _actorsService.GetByIdAsync(incorrectId);
            Assert.Multiple(() =>
            {
                Assert.That(correct, Is.Not.Null);
                Assert.That(incorrect, Is.Null);
            });
        }
        [Test]
        public async Task GetsViewModelsCorrectly()
        {
            var correctId = 1;
            var incorrectId = -1;

            var editViewModel = await _actorsService.GetEditViewModelByIdAsync(correctId);
            var deleteViewModel = await _actorsService.GetDeleteViewModelByIdAsync(correctId);
            var detailsViewModel = await _actorsService.GetDetailsViewModelByIdAsync(correctId);
            var addViewModel = await _actorsService.GetCreateViewModelAsync(); 
            
            var editIncorrectViewModel = await _actorsService.GetEditViewModelByIdAsync(incorrectId);
            var deleteIncorrectViewModel = await _actorsService.GetDeleteViewModelByIdAsync(incorrectId);
            var detailsIncorrecViewModel = await _actorsService.GetDetailsViewModelByIdAsync(incorrectId);

            Assert.Multiple(() =>
            {
                Assert.That(editViewModel, Is.Not.Null);
                Assert.That(editViewModel.Countries, Is.Not.Null);
                Assert.That(editViewModel.Countries.Count(), Is.GreaterThan(0));

                Assert.That(deleteViewModel, Is.Not.Null);
                Assert.That(detailsViewModel, Is.Not.Null);

                Assert.That(addViewModel, Is.Not.Null);
                Assert.That(addViewModel.Countries, Is.Not.Null);
                Assert.That(addViewModel.Countries.Count(), Is.GreaterThan(0));

                Assert.That(editIncorrectViewModel, Is.Null);
                Assert.That(deleteIncorrectViewModel, Is.Null);
                Assert.That(detailsIncorrecViewModel, Is.Null);
            });
        }
        [Test]
        public async Task SearchesCorrectly()
        {
            var actors = await _actorsService.QueryActorsAsync("iv", "", "nationality-sort", 1);
            Assert.Multiple(() =>
            {
                Assert.That(actors.Count(), Is.EqualTo(2));
                Assert.That(actors.FirstOrDefault().FullName, Is.EqualTo("Ivan Petrovic"));
            });
        }
        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
        }
    }
}
