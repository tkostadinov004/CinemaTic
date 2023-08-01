using CinemaTic.Core.Contracts;
using CinemaTic.Core.Services;
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
using Microsoft.AspNetCore.Http;

namespace CinemaTic.Core.UnitTests
{
    [TestFixture]
    public class UsersServiceTests
    {
        private CinemaDbContext _context;
        private Mock<UserManager<ApplicationUser>> _userManagerMock;
        private IUsersService _usersService;
        [SetUp]
        public void TestInitialize()
        {
            var options = new DbContextOptionsBuilder<CinemaDbContext>()
              .UseInMemoryDatabase(databaseName: "CinemaTicInMemory")
               .Options;
            this._context = new CinemaDbContext(options);
            var store = new Mock<IUserStore<ApplicationUser>>();
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
            _userManagerMock
                .Setup(userManager => userManager.CreateAsync(It.IsAny<ApplicationUser>()))
                .Returns(Task.FromResult(IdentityResult.Success));
            _userManagerMock
                .Setup(userManager => userManager.AddToRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()));

            var signInManagerMock = new Mock<SignInManager<ApplicationUser>>(_userManagerMock.Object,
   Mock.Of<IHttpContextAccessor>(),
    Mock.Of<IUserClaimsPrincipalFactory<ApplicationUser>>(), null, null, null, null);
            _usersService = new UsersService(_context, _userManagerMock.Object, signInManagerMock.Object, new Mock<ILogService>().Object, new Mock<IImageService>().Object);
            _context.Users.Add(new ApplicationUser("156fc675-02de-4250-9edb-869c85e13e61", 0, "8f06bfbc-e1e3-4f95-9bc1-30add0031c34", new DateTime(2023, 7, 29, 12, 59, 18, 234, DateTimeKind.Local).AddTicks(7086), "owner1@owner.com", true, "James", "Johnson", false, null, "OWNER1@OWNER.COM", "OWNER1@OWNER.COM", null, null, false, "profilePicURL", "1969a695-01c0-49be-8d42-482cb1c327bc", false, "owner1@owner.com"));
            _context.SaveChanges();
        }
        [Test]
        public async Task GetsByEmailCorrectly()
        {
            var user = await _context.Users.FirstOrDefaultAsync(i => i.Id == "156fc675-02de-4250-9edb-869c85e13e61");
            _userManagerMock.Setup(userManager => userManager.FindByEmailAsync(user.Email)).ReturnsAsync(user);

            Assert.That(await _usersService.GetUserByEmailAsync("owner1@owner.com"), Is.EqualTo(user));
        }
        [Test]
        public async Task GetsSidebarViewModelCorrectly()
        {
            var correctEmail = "owner1@owner.com";
            var incorrectEmail = "";

            var user = await _context.Users.FirstOrDefaultAsync(i => i.Id == "156fc675-02de-4250-9edb-869c85e13e61");
            _userManagerMock.Setup(userManager => userManager.FindByEmailAsync(user.Email)).ReturnsAsync(user);
            Assert.Multiple(async () =>
            {
                Assert.That(await _usersService.GetSidebarViewModelByEmailAsync(correctEmail), Is.Not.Null);
                Assert.That(await _usersService.GetSidebarViewModelByEmailAsync(incorrectEmail), Is.Null);
            });
        }
        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
        }
    }
}
