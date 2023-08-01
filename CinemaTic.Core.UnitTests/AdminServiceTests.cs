﻿using CinemaTic.Core.Contracts;
using CinemaTic.Core.Services;
using CinemaTic.Data;
using CinemaTic.Data.Enums;
using CinemaTic.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTic.Core.UnitTests
{
    [TestFixture]
    public class AdminServiceTests
    {
        private CinemaDbContext _context;
        private Mock<UserManager<ApplicationUser>> _userManagerMock;
        private Mock<IImageService> _imageServiceMock;
        private AdminService _adminService;
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
            _imageServiceMock = new Mock<IImageService>();

            _adminService = new AdminService(_userManagerMock.Object, _context, _imageServiceMock.Object);
            var users = new List<ApplicationUser>()
            {
                 new ApplicationUser( "156fc675-02de-4250-9edb-869c85e13e61", 0, "8f06bfbc-e1e3-4f95-9bc1-30add0031c34", new DateTime(2023, 7, 29, 12, 59, 18, 234, DateTimeKind.Local).AddTicks(7086), "owner1@owner.com", true, "James", "Johnson", false, null, "OWNER1@OWNER.COM", "OWNER1@OWNER.COM", null, null, false, "profilePicURL", "1969a695-01c0-49be-8d42-482cb1c327bc", false, "owner1@owner.com" ),
                    new ApplicationUser( "191d6e78-88fb-40ce-b85c-d8bcf4a1ae4c", 0, "1c85f3a1-2adc-4bc0-8e52-72f91f9c11e6", new DateTime(2023, 7, 29, 12, 59, 18, 234, DateTimeKind.Local).AddTicks(7401), "customer2@customer.com", true, "Owais", "Flynn", false, null, "CUSTOMER2@CUSTOMER.COM", "CUSTOMER2@CUSTOMER.COM", null, null, false, "profilePicURL", "23628fe3-34d3-436b-b12b-7386bda03b50", false, "customer2@customer.com" ),
                    new ApplicationUser( "1c850a33-6e0a-4c03-bb2d-c5a388042364", 0, "7ee1ee23-0839-40da-908d-b87ef4d668df", new DateTime(2023, 7, 29, 12, 59, 18, 234, DateTimeKind.Local).AddTicks(7518), "customer6@customer.com", true, "Peggy", "Pope", false, null, "CUSTOMER6@CUSTOMER.COM", "CUSTOMER6@CUSTOMER.COM", null, null, false, "profilePicURL", "d3103320-f4b9-4654-ba99-654bc1cdf6c4", false, "customer6@customer.com" ),
                    new ApplicationUser( "2055e8c8-5a8e-49c3-9f0a-a987700af2ee", 0, "a294bebb-1a63-4316-8f84-629edf1d64e0", new DateTime(2023, 7, 29, 12, 59, 18, 234, DateTimeKind.Local).AddTicks(7180), "owner2@owner.com", true, "Mary", "Lou", false, null, "OWNER2@OWNER.COM", "OWNER2@OWNER.COM", null, null, false, "profilePicURL", "0dcc8f9a-b2bd-40a5-8f68-ad4c06ccd772", false, "owner2@owner.com" ),
                    new ApplicationUser( "218dcf68-aa10-4c63-994f-50853fb19296", 0, "76e4b52a-578d-47d2-8a5f-c0f9fc0b5c3e", new DateTime(2023, 7, 29, 12, 59, 18, 234, DateTimeKind.Local).AddTicks(7212), "owner3@owner.com", true, "Margarita", "Costner", false, null, "OWNER3@OWNER.COM", "OWNER3@OWNER.COM", null, null, false, "profilePicURL", "ea79ff42-c2cd-49bd-8d71-1cc1c1f27dab", false, "owner3@owner.com" ),
                    new ApplicationUser( "2a8f5f5c-e539-4868-837b-9a19852a904e", 0, "5012f086-228e-4796-9b77-9f767892e80c", new DateTime(2023, 7, 29, 12, 59, 18, 234, DateTimeKind.Local).AddTicks(7385), "customer1@customer.com", true, "Barney", "Hobbs", false, null, "CUSTOMER1@CUSTOMER.COM", "CUSTOMER1@CUSTOMER.COM", null, null, false, "profilePicURL", "41af5f84-823a-4d6e-8dc3-6917d36c1983", false, "customer1@customer.com" ),
                    new ApplicationUser( "2f09c66b-0830-4fcc-8a0f-f29b0990c669", 0, "159b0533-e321-4cca-a2de-243612f00487", new DateTime(2023, 7, 29, 12, 59, 18, 234, DateTimeKind.Local).AddTicks(7553), "customer8@customer.com", true, "Frazer", "Hensley", false, null, "CUSTOMER8@CUSTOMER.COM", "CUSTOMER8@CUSTOMER.COM", null, null, false, "profilePicURL", "11d3f4f3-b612-4683-90db-2a0c77867a06", false, "customer8@customer.com" ),
                    new ApplicationUser( "4634669c-c5ad-41e6-8b41-f1524c9654ad", 0, "2485e1c3-2020-40bc-b989-f73593f7ed05", new DateTime(2023, 7, 29, 12, 59, 18, 234, DateTimeKind.Local).AddTicks(7419), "customer3@customer.com", true, "Kiran", "Delacruz", false, null, "CUSTOMER3@CUSTOMER.COM", "CUSTOMER3@CUSTOMER.COM", null, null, false, "profilePicURL", "18e0ad1c-2ce4-494c-8a08-4eb3e491aae1", false, "customer3@customer.com" ),
                    new ApplicationUser( "5556c45e-395d-402b-b765-750666b092fc", 0, "b65be569-e85a-4a74-a7bc-425b7635069f", new DateTime(2023, 7, 29, 12, 59, 18, 234, DateTimeKind.Local).AddTicks(7264), "owner6@owner.com", true, "Javier", "Gonzalez", false, null, "OWNER6@OWNER.COM", "OWNER6@OWNER.COM", null, null, false, "profilePicURL", "35a42130-bf0d-4f5a-85a4-032f27fb34fe", false, "owner6@owner.com" ),
                    new ApplicationUser( "60223fcf-5fa4-434f-a4ba-9389a4f571a0", 0, "4f65a024-e0d8-424b-95be-4c4966e0ed73", new DateTime(2023, 7, 29, 12, 59, 18, 234, DateTimeKind.Local).AddTicks(7299), "owner8@owner.com", true, "John", "Chang", false, null, "OWNER8@OWNER.COM", "OWNER8@OWNER.COM", null, null, false, "profilePicURL", "5caf143b-da62-492e-9c4c-3a631cdd77b9", false, "owner8@owner.com" ),
                    new ApplicationUser( "610ab053-2c5a-451b-9634-03b59ea4a473", 0, "169c576b-71f5-4ef3-8fcb-3479eca00a65", new DateTime(2023, 7, 29, 12, 59, 18, 234, DateTimeKind.Local).AddTicks(7347), "owner9@owner.com", true, "Lily", "May", false, null, "OWNER9@OWNER.COM", "OWNER9@OWNER.COM", null, null, false, "profilePicURL", "cf814eb1-d2f0-47ac-8dbd-54482814dc77", false, "owner9@owner.com" ),
                    new ApplicationUser( "64ca1994-bfd0-4d26-8ec4-4d1bc82bd95c", 0, "b26c078d-4fca-474d-9a24-a10a61a69c88", new DateTime(2023, 7, 29, 12, 59, 18, 234, DateTimeKind.Local).AddTicks(7283), "owner7@owner.com", true, "Marco", "Cruz", false, null, "OWNER7@OWNER.COM", "OWNER7@OWNER.COM", null, null, false, "profilePicURL", "2f09438a-adca-4bed-8a26-4ac7ae834dd3", false, "owner7@owner.com" ),
                    new ApplicationUser( "8980e4ca-2628-490d-840a-9c9414ab9f33", 0, "d135f42d-5dae-407f-a9d4-c4ec642f61cc", new DateTime(2023, 7, 29, 12, 59, 18, 234, DateTimeKind.Local).AddTicks(7364), "owner10@owner.com", true, "Jack", "Marston", false, null, "OWNER10@OWNER.COM", "OWNER10@OWNER.COM", null, null, false, "profilePicURL", "69434a7f-f6f5-4f2f-9aec-4594d2e1bd27", false, "owner10@owner.com" ),
                    new ApplicationUser( "96256cfb-df20-4a1f-8898-f06f634a17d7", 0, "4effdcbd-6484-43ad-bab0-83c882123a07", new DateTime(2023, 7, 29, 12, 59, 18, 234, DateTimeKind.Local).AddTicks(7587), "customer10@customer.com", true, "Bryony", "Becker", false, null, "CUSTOMER10@CUSTOMER.COM", "CUSTOMER10@CUSTOMER.COM", null, null, false, "profilePicURL", "84717c40-eafd-40e7-b0a1-96e7e0937a09", false, "customer10@customer.com" ),
                    new ApplicationUser( "a9b6dc74-38a5-4794-a703-59204f461adb", 0, "a0a0780d-c13c-4218-8b33-03da54eec896", new DateTime(2023, 7, 29, 12, 59, 18, 234, DateTimeKind.Local).AddTicks(7228), "owner4@owner.com", true, "Gabriel", "Peric", false, null, "OWNER4@OWNER.COM", "OWNER4@OWNER.COM", null, null, false, "profilePicURL", "d795d540-219a-4312-bbae-4d3b0558c2d5", false, "owner4@owner.com" ),
                    new ApplicationUser( "bfa19e5f-4529-4276-bde8-8e6d3de2c423", 0, "13ea44e7-8dae-4bb7-976e-64a952c9004c", new DateTime(2023, 7, 29, 12, 59, 18, 234, DateTimeKind.Local).AddTicks(7435), "customer4@customer.com", true, "Ava-Rose", "Chapman", false, null, "CUSTOMER4@CUSTOMER.COM", "CUSTOMER4@CUSTOMER.COM", null, null, false, "profilePicURL", "eede314b-fa5b-4b39-927c-59c9959e9990", false, "customer4@customer.com" ),
                    new ApplicationUser( "c21bf410-3e22-4720-b01a-f2d91191a222", 0, "87fe1bfb-3589-4d5e-a44e-c8adcb6d5214", new DateTime(2023, 7, 29, 12, 59, 18, 234, DateTimeKind.Local).AddTicks(7453), "customer5@customer.com", true, "Sienna", "Barnett", false, null, "CUSTOMER5@CUSTOMER.COM", "CUSTOMER5@CUSTOMER.COM", null, null, false, "profilePicURL", "0f9d29a6-6105-4649-a166-0762aee21984", false, "customer5@customer.com" ),
                    new ApplicationUser( "e7d88cb7-a424-4795-8965-17273642b773", 0, "8e7b2c00-3070-485c-8e23-06551a9cff1f", new DateTime(2023, 7, 29, 12, 59, 18, 234, DateTimeKind.Local).AddTicks(7571), "customer9@customer.com", true, "Sadia", "Francis", false, null, "CUSTOMER9@CUSTOMER.COM", "CUSTOMER9@CUSTOMER.COM", null, null, false, "profilePicURL", "7ca95f02-1224-4ae7-a823-0e9243192c8b", false, "customer9@customer.com" ),
                    new ApplicationUser( "ea811464-0c63-4c45-ac26-d4eb5bff334f", 0, "9bfe5e83-4173-4624-ba5d-f7cd07563c4f", new DateTime(2023, 7, 29, 12, 59, 18, 234, DateTimeKind.Local).AddTicks(7246), "owner5@owner.com", true, "Percy", "Jackson", false, null, "OWNER5@OWNER.COM", "OWNER5@OWNER.COM", null, null, false, "profilePicURL", "4b0d4e53-c70f-4118-8c78-b63d6adb1548", false, "owner5@owner.com" ),
                    new ApplicationUser( "f338b628-feaf-4a03-95ad-defb7aec5c83", 0, "096f10f9-3070-45c1-824d-1e96bc810fdc", new DateTime(2023, 7, 29, 12, 59, 18, 234, DateTimeKind.Local).AddTicks(7537), "customer7@customer.com", true, "Madiha", "Brock", false, null, "CUSTOMER7@CUSTOMER.COM", "CUSTOMER7@CUSTOMER.COM", null, null, false, "profilePicURL", "d6172bcd-0893-437a-b705-43122add4781", false, "customer7@customer.com" ),
                    new ApplicationUser( "c0102fde-8991-4e2a-bfca-cdda51f13a66", 0, "3a2fa07d-ff91-407f-a2d7-fa01172115d2", new DateTime(2023, 7, 29, 12, 59, 18, 234, DateTimeKind.Local).AddTicks(7537), "admin@admin.com", true, "Admin", "Admin", false, null, "ADMIN@ADMIN.COM", "ADMIN@ADMIN.COM", null, null, false, "profilePicURL", "d6172bcd-0893-437a-b705-43122add4781", false, "admin@admin.com" ),
            };
            var roles = new List<IdentityRole>()
            {
                new IdentityRole
                {
                    Id = "4f2e306d-f18e-407f-add5-b06709864898",
                    Name = "Owner",
                    NormalizedName = "OWNER",
                    ConcurrencyStamp = "986d6ab3-c3ef-4c63-9f83-c5e9542da90d"
                },
                new IdentityRole
                {
                    Id = "ddcf49d7-098f-4dfb-a859-01efa93f73be",
                    Name = "Customer",
                    NormalizedName = "CUSTOMER",
                    ConcurrencyStamp = "b1c2e206-4e9c-4b8d-9342-00c75ef366eb"
                },
                new IdentityRole
                {
                    Id = "f3c309d2-8913-4392-bef9-04e264ed443a",
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR",
                    ConcurrencyStamp = "f821d92f-1d22-41cb-9e38-3fed772c8834"
                }
            };
            var userRoles = new List<IdentityUserRole<string>>()
            {
                new IdentityUserRole<string>{UserId = "156fc675-02de-4250-9edb-869c85e13e61", RoleId = "4f2e306d-f18e-407f-add5-b06709864898" },
                new IdentityUserRole<string>{UserId = "2055e8c8-5a8e-49c3-9f0a-a987700af2ee", RoleId = "4f2e306d-f18e-407f-add5-b06709864898" },
                new IdentityUserRole<string>{UserId = "218dcf68-aa10-4c63-994f-50853fb19296", RoleId = "4f2e306d-f18e-407f-add5-b06709864898" },
                new IdentityUserRole<string>{UserId = "5556c45e-395d-402b-b765-750666b092fc", RoleId = "4f2e306d-f18e-407f-add5-b06709864898" },
                new IdentityUserRole<string>{UserId = "60223fcf-5fa4-434f-a4ba-9389a4f571a0", RoleId = "4f2e306d-f18e-407f-add5-b06709864898" },
                new IdentityUserRole<string>{UserId = "610ab053-2c5a-451b-9634-03b59ea4a473", RoleId = "4f2e306d-f18e-407f-add5-b06709864898" },
                new IdentityUserRole<string>{UserId = "64ca1994-bfd0-4d26-8ec4-4d1bc82bd95c", RoleId = "4f2e306d-f18e-407f-add5-b06709864898" },
                new IdentityUserRole<string>{UserId = "8980e4ca-2628-490d-840a-9c9414ab9f33", RoleId = "4f2e306d-f18e-407f-add5-b06709864898" },
                new IdentityUserRole<string>{UserId = "a9b6dc74-38a5-4794-a703-59204f461adb", RoleId = "4f2e306d-f18e-407f-add5-b06709864898" },
                new IdentityUserRole<string>{UserId = "ea811464-0c63-4c45-ac26-d4eb5bff334f", RoleId = "4f2e306d-f18e-407f-add5-b06709864898" },
                new IdentityUserRole<string>{UserId = "191d6e78-88fb-40ce-b85c-d8bcf4a1ae4c", RoleId = "ddcf49d7-098f-4dfb-a859-01efa93f73be" },
                new IdentityUserRole<string>{UserId = "1c850a33-6e0a-4c03-bb2d-c5a388042364", RoleId = "ddcf49d7-098f-4dfb-a859-01efa93f73be" },
                new IdentityUserRole<string>{UserId = "2a8f5f5c-e539-4868-837b-9a19852a904e", RoleId = "ddcf49d7-098f-4dfb-a859-01efa93f73be" },
                new IdentityUserRole<string>{UserId = "2f09c66b-0830-4fcc-8a0f-f29b0990c669", RoleId = "ddcf49d7-098f-4dfb-a859-01efa93f73be" },
                new IdentityUserRole<string>{UserId = "4634669c-c5ad-41e6-8b41-f1524c9654ad", RoleId = "ddcf49d7-098f-4dfb-a859-01efa93f73be" },
                new IdentityUserRole<string>{UserId = "96256cfb-df20-4a1f-8898-f06f634a17d7", RoleId = "ddcf49d7-098f-4dfb-a859-01efa93f73be" },
                new IdentityUserRole<string>{UserId = "bfa19e5f-4529-4276-bde8-8e6d3de2c423", RoleId = "ddcf49d7-098f-4dfb-a859-01efa93f73be" },
                new IdentityUserRole<string>{UserId = "c21bf410-3e22-4720-b01a-f2d91191a222", RoleId = "ddcf49d7-098f-4dfb-a859-01efa93f73be" },
                new IdentityUserRole<string>{UserId = "e7d88cb7-a424-4795-8965-17273642b773", RoleId = "ddcf49d7-098f-4dfb-a859-01efa93f73be" },
                new IdentityUserRole<string>{UserId = "f338b628-feaf-4a03-95ad-defb7aec5c83", RoleId = "ddcf49d7-098f-4dfb-a859-01efa93f73be" },
                new IdentityUserRole<string>{UserId = "c0102fde-8991-4e2a-bfca-cdda51f13a66", RoleId = "f3c309d2-8913-4392-bef9-04e264ed443a" },
            };
            var cinemas = new List<Data.Models.Cinema>()
            {
                 new Data.Models.Cinema( 1, "#FF0000", 0, "#000000", "#FFFFFF", "#FFFFFF", "#000000", "Experience the magic of movies under the stars at Starlight Cinemas! Our open-air theaters offer a unique and enchanting movie-going experience that combines the joy of cinema with the beauty of the night sky. Founded on January 15, 1985, Starlight Cinemas has been a cherished destination for families, couples, and friends seeking to create unforgettable memories. With 50 rows and 30 columns, our theaters accommodate a large audience while maintaining an intimate atmosphere. Grab your popcorn and recline in our comfortable seats as you immerse yourself in a world of cinematic wonder and twinkling constellations.", new DateTime(1985, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "StarlightCinemas.jpg", "Testlight Cinemas", "156fc675-02de-4250-9edb-869c85e13e61", 30, 50, "#000000" ),
                    new Data.Models.Cinema( 2, "#FF0000", 0, "#000000", "#FFFFFF", "#FFFFFF", "#000000", "Welcome to Cineplex Delights, the perfect destination for delightful movies the whole family can enjoy! Established on March 5, 1999, our mission has been to bring joy, laughter, and heartwarming stories to our audiences. With 40 rows and 40 columns, our theaters provide a spacious yet cozy environment for a truly immersive cinematic experience. From the latest blockbusters to timeless classics, we curate a diverse selection of films to cater to every taste. Our friendly staff, comfortable seating, and delicious concessions ensure that your time at Cineplex Delights will be nothing short of delightful!", new DateTime(1999, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "CineplexDelights.jpg", "Cineplex Delights", "156fc675-02de-4250-9edb-869c85e13e61", 40, 40, "#000000" ),
                    new Data.Models.Cinema( 3, "#FF0000", 0, "#000000", "#FFFFFF", "#FFFFFF", "#000000", "Step into the world of cinematic splendor at Golden Screen Theatres, where every movie shines like gold. Established on December 2, 1977, we take pride in our long-standing tradition of delivering exceptional film experiences to our patrons. With 60 rows and 45 columns, our theaters boast a grand ambiance, befitting the finest movie presentations. Our state-of-the-art facilities, crystal-clear audio, and high-definition visuals enhance your enjoyment of every film. Whether you're a film connoisseur or a casual movie-goer, Golden Screen Theatres promises to elevate your love for the silver screen to dazzling new heights.", new DateTime(1977, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "GoldenScreenTheatres.jpg", "Test Screen Theatres", "156fc675-02de-4250-9edb-869c85e13e61", 45, 60, "#000000" ),
                    new Data.Models.Cinema( 4, "#FF0000", 0, "#000000", "#FFFFFF", "#FFFFFF", "#000000", "Relive the golden age of cinema at Retro Rewind Cinema! Founded on October 20, 2005, our theater is a tribute to the timeless classics that continue to captivate audiences across generations. Featuring 30 rows and 60 columns, our spacious yet cozy auditoriums ensure an intimate connection with the films of yesteryears. From black-and-white masterpieces to beloved blockbusters, every screening is an immersive journey into cinematic history. Our vintage decor and classic movie posters transport you back in time as you savor the magic of the silver screen. Step into a bygone era at Retro Rewind Cinema!", new DateTime(2005, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "RetroRewindCinema.jpg", "Retro Rewind Cinema", "156fc675-02de-4250-9edb-869c85e13e61", 60, 30, "#000000" ),
            };

            _context.Users.AddRange(users);
            _context.Roles.AddRange(roles);
            _context.UserRoles.AddRange(userRoles);
            _context.Cinemas.AddRange(cinemas);
            _context.SaveChanges();
        }
        [Test]
        public async Task GetsAllUsersCorrectly()
        {
            var users = await _adminService.GetAllUsersAsync();

            Assert.That(users.Count(), Is.EqualTo(_context.Users.Count()));
        }
        [Test]
        public async Task DeletesUserCorrectly()
        {
            string id = "156fc675-02de-4250-9edb-869c85e13e61";
            var user = await _context.Users.FirstOrDefaultAsync(i => i.Id == id);

            _userManagerMock
               .Setup(userManager => userManager.FindByIdAsync(id)).ReturnsAsync(user);
            _userManagerMock
              .Setup(userManager => userManager.DeleteAsync(user)).ReturnsAsync(IdentityResult.Success);

            Assert.That(await _adminService.DeleteAccountAsync(id), Is.True);
        }
        [Test]
        public async Task DoesNotThrowOnDeleteWithWrongId()
        {
            string id = null;

            Assert.DoesNotThrowAsync(async () => await _adminService.DeleteAccountAsync(id));
        }
        [Test]
        public async Task DemotesUserCorrectly()
        {
            string id = "156fc675-02de-4250-9edb-869c85e13e61";
            var user = await _context.Users.FirstOrDefaultAsync(i => i.Id == id);

            _userManagerMock
               .Setup(userManager => userManager.FindByIdAsync(id)).ReturnsAsync(user);
            _userManagerMock
               .Setup(userManager => userManager.IsInRoleAsync(user, "Owner")).ReturnsAsync(true);

            Assert.That(await _adminService.DemoteUserAsync(id), Is.True);
        }
        [Test]
        public async Task FindsByIdCorrectly()
        {
            string id = "156fc675-02de-4250-9edb-869c85e13e61";
            var user = await _context.Users.FirstOrDefaultAsync(i => i.Id == id);

            _userManagerMock
               .Setup(userManager => userManager.FindByIdAsync(id)).ReturnsAsync(user);
            Assert.That(await _adminService.GetUserByIdAsync(id), Is.EqualTo(user));
        }
        [Test]
        public async Task PromotesUserCorrectly()
        {
            string id = "156fc675-02de-4250-9edb-869c85e13e61";
            var user = await _context.Users.FirstOrDefaultAsync(i => i.Id == id);

            _userManagerMock
               .Setup(userManager => userManager.FindByIdAsync(id)).ReturnsAsync(user);
            _userManagerMock
               .Setup(userManager => userManager.IsInRoleAsync(user, "Customer")).ReturnsAsync(true);

            Assert.That(await _adminService.PromoteUserAsync(id), Is.True);
        }
        [Test]
        public async Task GetsAllCinemasCorrectly()
        {
            var cinemas = await _adminService.GetAllCinemasAsync();
            Assert.That(cinemas.Count(), Is.EqualTo(_context.Cinemas.Count()));
        }
        [Test]
        public async Task SearchesAndFiltersCinemasCorrectly()
        {
            var cinemas = await _adminService.SearchAndFilterCinemasAsync("test", ApprovalStatus.Approved.ToString(), "name-sort-desc", 1);

            Assert.Multiple(() =>
            {
                Assert.That(cinemas.Count(), Is.EqualTo(2));
                Assert.That(cinemas.ElementAt(0).Name, Is.EqualTo("Testlight Cinemas"));
            });
        }
        [Test]
        public async Task UserExistsCorrectly()
        {
            string id = "156fc675-02de-4250-9edb-869c85e13e61";
            var exists = await _adminService.UserExistsAsync(id);

            Assert.That(exists, Is.True);
        }
        [Test]
        public async Task CinemaExistsCorrectly()
        {
            int id = 1;
            var exists = await _adminService.CinemaExistsAsync(id);

            Assert.That(exists, Is.True);
        }
        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
        }
    }
}
