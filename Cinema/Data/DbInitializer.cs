using Cinema.Models;
using DbSeeder.Data.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Data
{
    public class DbInitializer
    {
        private readonly ApplicationDbContext _context;
        private const string Directory = "../DbSeeder.Data/Datasets";
        public DbInitializer(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Run(IServiceProvider serviceProvider, bool shouldDeleteDB)
        {
            if (shouldDeleteDB == true)
            {
                _context.Database.EnsureDeleted();
                _context.Database.EnsureCreated();

                AddGenres();
                AddActors();
                AddMovies();
                AddActorMovies();
                AddRoles(serviceProvider);
                AddUsers(serviceProvider);
            }
        }
        public void AddRoles(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                Task<IdentityResult> roleResult;

                //Check that there is an Administrator role and create if not
                Task<bool> hasAdminRole = roleManager.RoleExistsAsync("Administrator");
                hasAdminRole.Wait();

                if (!hasAdminRole.Result)
                {
                    roleResult = roleManager.CreateAsync(new IdentityRole("Administrator"));
                    roleResult.Wait();
                }

                Task<bool> hasOwnerRole = roleManager.RoleExistsAsync("Owner");
                hasOwnerRole.Wait();

                if (!hasOwnerRole.Result)
                {
                    roleResult = roleManager.CreateAsync(new IdentityRole("Owner"));
                    roleResult.Wait();
                }

                Task<bool> hasVisitorRole = roleManager.RoleExistsAsync("Visitor");
                hasVisitorRole.Wait();

                if (!hasVisitorRole.Result)
                {
                    roleResult = roleManager.CreateAsync(new IdentityRole("Visitor"));
                    roleResult.Wait();
                }
            }
        }
        public void AddUsers(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                string adminEmail = "admin@admin.com";

                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                //Check if the admin user exists and create it if not
                //Add to the Administrator role

                Task<ApplicationUser> adminUser = userManager.FindByEmailAsync(adminEmail);
                adminUser.Wait();

                Task<IdentityResult> userResult = null;
                if (adminUser.Result == null)
                {
                    ApplicationUser administrator = new ApplicationUser();
                    administrator.Email = adminEmail;
                    administrator.UserName = adminEmail;
                    administrator.FirstName = "Admin";
                    administrator.LastName = "Admin";
                    administrator.EmailConfirmed = true;

                    userResult = userManager.CreateAsync(administrator, "adminPass123*");
                    userResult.Wait();
                    if (userResult.Result.Succeeded)
                    {
                        Task<IdentityResult> newUserRole = userManager.AddToRoleAsync(administrator, "Administrator");
                        newUserRole.Wait();
                    }
                }

                Task<ApplicationUser> visitorUser1 = userManager.FindByEmailAsync("visitor@visitor.bg");
                visitorUser1.Wait();

                Task<IdentityResult> visitorUser1Result = null;
                if (visitorUser1.Result == null)
                {
                    ApplicationUser visitor = new ApplicationUser();
                    visitor.Email = "visitor@visitor.bg";
                    visitor.UserName = "visitor@visitor.bg";
                    visitor.FirstName = "Петър";
                    visitor.LastName = "Йорданов";
                    visitor.EmailConfirmed = true;

                    visitorUser1Result = userManager.CreateAsync(visitor, "visitorPass123*");
                    visitorUser1Result.Wait();
                    if (visitorUser1Result.Result.Succeeded)
                    {
                        Task<IdentityResult> newUserRole = userManager.AddToRoleAsync(visitor, "Visitor");
                        newUserRole.Wait();
                    }
                }

                Task<ApplicationUser> visitorUser2 = userManager.FindByEmailAsync("visitor2@visitor.bg");
                visitorUser2.Wait();

                Task<IdentityResult> visitorUser2Result = null;
                if (visitorUser2.Result == null)
                {
                    ApplicationUser visitor = new ApplicationUser();
                    visitor.Email = "visitor2@visitor.bg";
                    visitor.UserName = "visitor2@visitor.bg";
                    visitor.FirstName = "Иван";
                    visitor.LastName = "Петков";
                    visitor.EmailConfirmed = true;

                    visitorUser2Result = userManager.CreateAsync(visitor, "visitorPass123*");
                    visitorUser2Result.Wait();
                    if (visitorUser2Result.Result.Succeeded)
                    {
                        Task<IdentityResult> newUserRole = userManager.AddToRoleAsync(visitor, "Visitor");
                        newUserRole.Wait();
                    }
                }

                Task<ApplicationUser> visitorUser3 = userManager.FindByEmailAsync("visitor3@visitor.bg");
                visitorUser3.Wait();

                Task<IdentityResult> visitorUser3Result = null;
                if (visitorUser3.Result == null)
                {
                    ApplicationUser visitor = new ApplicationUser();
                    visitor.Email = "visitor3@visitor.bg";
                    visitor.UserName = "visitor3@visitor.bg";
                    visitor.FirstName = "Йордан";
                    visitor.LastName = "Костов";
                    visitor.EmailConfirmed = true;

                    visitorUser3Result = userManager.CreateAsync(visitor, "visitorPass123*");
                    visitorUser3Result.Wait();
                    if (visitorUser3Result.Result.Succeeded)
                    {
                        Task<IdentityResult> newUserRole = userManager.AddToRoleAsync(visitor, "Visitor");
                        newUserRole.Wait();
                    }
                }

                Task<ApplicationUser> ownerUser1 = userManager.FindByEmailAsync("owner@owner.bg");
                ownerUser1.Wait();

                Task<IdentityResult> ownerUser1Result = null;
                if (ownerUser1.Result == null)
                {
                    ApplicationUser owner = new ApplicationUser();
                    owner.Email = "owner@owner.bg";
                    owner.UserName = "owner@owner.bg";
                    owner.FirstName = "Костадин";
                    owner.LastName = "Миленов";
                    owner.EmailConfirmed = true;

                    ownerUser1Result = userManager.CreateAsync(owner, "ownerPass123*");
                    ownerUser1Result.Wait();
                    if (ownerUser1Result.Result.Succeeded)
                    {
                        Task<IdentityResult> newUserRole = userManager.AddToRoleAsync(owner, "Owner");
                        newUserRole.Wait();
                    }
                }

                Task<ApplicationUser> ownerUser2 = userManager.FindByEmailAsync("owner1@owner.bg");
                ownerUser2.Wait();

                Task<IdentityResult> ownerUser2Result = null;
                if (ownerUser2.Result == null)
                {
                    ApplicationUser owner = new ApplicationUser();
                    owner.Email = "owner1@owner.bg";
                    owner.UserName = "owner1@owner.bg";
                    owner.FirstName = "Стоян";
                    owner.LastName = "Грозев";
                    owner.EmailConfirmed = true;

                    ownerUser2Result = userManager.CreateAsync(owner, "ownerPass123*");
                    ownerUser2Result.Wait();
                    if (ownerUser2Result.Result.Succeeded)
                    {
                        Task<IdentityResult> newUserRole = userManager.AddToRoleAsync(owner, "Owner");
                        newUserRole.Wait();
                    }
                }
            }
        }
        public void AddGenres()
        {
            string json = File.ReadAllText($"{Directory}/genres.json", Encoding.UTF8);

            var genres = JsonConvert.DeserializeObject<AddGenreDTO[]>(json);
            foreach (var genre in genres)
            {
                _context.Genres.Add(new Genre
                {
                    EnglishName = genre.EnglishName,
                    BulgarianName = genre.BulgarianName
                });
            }
            _context.SaveChanges();
        }
        public void AddActors()
        {
            string json = File.ReadAllText($"{Directory}/actors.json", Encoding.UTF8);

            var actors = JsonConvert.DeserializeObject<AddActorDTO[]>(json);
            foreach (var actor in actors)
            {
                _context.Actors.Add(new Actor
                {
                    FirstName = actor.FirstName,
                    LastName = actor.LastName,
                    BulgarianFullName = actor.BulgarianFullName,
                    Birthdate = DateTime.ParseExact(actor.Birthdate, "dd.MM.yyyy", CultureInfo.InvariantCulture),
                    Nationality = actor.Nationality,
                    Rating = decimal.Parse(actor.IMDBRating),
                    ImageUrl = $"{actor.FirstName}-{actor.LastName}.jpg"
                });
            }
            _context.SaveChanges();
        }
        public void AddMovies()
        {
            string json = File.ReadAllText($"{Directory}/movies.json", Encoding.UTF8);

            var movies = JsonConvert.DeserializeObject<AddMovieDTO[]>(json);
            foreach (var movie in movies)
            {
                _context.Movies.Add(new Movie
                {
                    EnglishTitle = movie.EnglishTitle,
                    BulgarianTitle = movie.BulgarianTitle,
                    GenreId = _context.Genres.FirstOrDefault(i => i.EnglishName == movie.Genre).Id,
                    Description = movie.Description,
                    Date = DateTime.ParseExact(movie.Date, "dd.MM.yyyy", CultureInfo.InvariantCulture),
                    Price = decimal.Parse(movie.Price),
                    RunningTime = int.Parse(movie.RunningTime),
                    TrailerUrl = movie.TrailerUrl,
                    UserRating = 0,
                    RatingCount = 0,
                    ImageUrl = $"{string.Join("-", movie.EnglishTitle.Replace(":", "").Split())}.jpg"
                });
            }
            _context.SaveChanges();
        }
        public void AddActorMovies()
        {
            string json = File.ReadAllText($"{Directory}/actormovies.json", Encoding.UTF8);
            var actorMovies = JsonConvert.DeserializeObject<AddActorMovieDTO[]>(json);

            foreach (var actorMovie in actorMovies)
            {
                var am = new ActorMovie
                {
                    ActorId = _context.Actors.ToList().FirstOrDefault(i => $"{i.FirstName} {i.LastName}" == actorMovie.Actor).Id,
                    MovieId = _context.Movies.ToList().FirstOrDefault(i => i.EnglishTitle == actorMovie.Movie).Id,
                };
                _context.ActorsMovies.Add(new ActorMovie
                {
                    ActorId = _context.Actors.ToList().FirstOrDefault(i => $"{i.FirstName} {i.LastName}" == actorMovie.Actor).Id,
                    MovieId = _context.Movies.ToList().FirstOrDefault(i => i.EnglishTitle == actorMovie.Movie).Id,
                });
            }
            _context.SaveChanges();
        }
    }
}
