using Cinema.Data.Models;
using Cinema.Data.Seeder.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        private readonly CinemaDbContext _context;
        private const string Directory = "../Cinema.Data.Seeder/Datasets";
        public DbInitializer(CinemaDbContext context)
        {
            _context = context;
        }

        public void Run(IServiceProvider serviceProvider, bool shouldDeleteDB)
        {
            if (shouldDeleteDB)
            {
                _context.Database.EnsureDeleted();
                _context.Database.Migrate();
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

                Task<bool> hasCustomerRole = roleManager.RoleExistsAsync("Customer");
                hasCustomerRole.Wait();

                if (!hasCustomerRole.Result)
                {
                    roleResult = roleManager.CreateAsync(new IdentityRole("Customer"));
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

                Task<ApplicationUser> customerUser1 = userManager.FindByEmailAsync("customer@customer.com");
                customerUser1.Wait();

                Task<IdentityResult> customerUser1Result = null;
                if (customerUser1.Result == null)
                {
                    ApplicationUser customer = new ApplicationUser();
                    customer.Email = "customer@customer.com";
                    customer.UserName = "customer@customer.com";
                    customer.FirstName = "George";
                    customer.LastName = "Jameson";
                    customer.EmailConfirmed = true;

                    customerUser1Result = userManager.CreateAsync(customer, "customerPass123*");
                    customerUser1Result.Wait();
                    if (customerUser1Result.Result.Succeeded)
                    {
                        Task<IdentityResult> newUserRole = userManager.AddToRoleAsync(customer, "Customer");
                        newUserRole.Wait();
                    }
                }

                Task<ApplicationUser> customerUser2 = userManager.FindByEmailAsync("customer2@customer.com");
                customerUser2.Wait();

                Task<IdentityResult> customerUser2Result = null;
                if (customerUser2.Result == null)
                {
                    ApplicationUser customer = new ApplicationUser();
                    customer.Email = "customer2@customer.com";
                    customer.UserName = "customer2@customer.com";
                    customer.FirstName = "Ivan";
                    customer.LastName = "Moody";
                    customer.EmailConfirmed = true;

                    customerUser2Result = userManager.CreateAsync(customer, "customerPass123*");
                    customerUser2Result.Wait();
                    if (customerUser2Result.Result.Succeeded)
                    {
                        Task<IdentityResult> newUserRole = userManager.AddToRoleAsync(customer, "Customer");
                        newUserRole.Wait();
                    }
                }

                Task<ApplicationUser> customerUser3 = userManager.FindByEmailAsync("customer3@customer.com");
                customerUser3.Wait();

                Task<IdentityResult> customerUser3Result = null;
                if (customerUser3.Result == null)
                {
                    ApplicationUser customer = new ApplicationUser();
                    customer.Email = "customer3@customer.com";
                    customer.UserName = "customer3@customer.com";
                    customer.FirstName = "Jack";
                    customer.LastName = "Carson";
                    customer.EmailConfirmed = true;

                    customerUser3Result = userManager.CreateAsync(customer, "customerPass123*");
                    customerUser3Result.Wait();
                    if (customerUser3Result.Result.Succeeded)
                    {
                        Task<IdentityResult> newUserRole = userManager.AddToRoleAsync(customer, "Customer");
                        newUserRole.Wait();
                    }
                }

                Task<ApplicationUser> ownerUser1 = userManager.FindByEmailAsync("owner@owner.com");
                ownerUser1.Wait();

                Task<IdentityResult> ownerUser1Result = null;
                if (ownerUser1.Result == null)
                {
                    ApplicationUser owner = new ApplicationUser();
                    owner.Email = "owner@owner.com";
                    owner.UserName = "owner@owner.com";
                    owner.FirstName = "Carl";
                    owner.LastName = "Milhouse";
                    owner.EmailConfirmed = true;

                    ownerUser1Result = userManager.CreateAsync(owner, "ownerPass123*");
                    ownerUser1Result.Wait();
                    if (ownerUser1Result.Result.Succeeded)
                    {
                        Task<IdentityResult> newUserRole = userManager.AddToRoleAsync(owner, "Owner");
                        newUserRole.Wait();
                    }
                }

                Task<ApplicationUser> ownerUser2 = userManager.FindByEmailAsync("owner1@owner.com");
                ownerUser2.Wait();

                Task<IdentityResult> ownerUser2Result = null;
                if (ownerUser2.Result == null)
                {
                    ApplicationUser owner = new ApplicationUser();
                    owner.Email = "owner1@owner.com";
                    owner.UserName = "owner1@owner.com";
                    owner.FirstName = "Jim";
                    owner.LastName = "Warren";
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
                    Name = genre.Name,
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
                    Title = movie.Title,
                    GenreId = _context.Genres.FirstOrDefault(i => i.Name == movie.Genre).Id,
                    Description = movie.Description,
                    RunningTime = int.Parse(movie.RunningTime),
                    TrailerUrl = movie.TrailerUrl,
                    UserRating = 0,
                    RatingCount = 0,
                    ImageUrl = $"{string.Join("-", movie.Title.Replace(":", "").Split())}.jpg"
                });
            }
            _context.SaveChanges();
        }
        public void AddActorMovies()
        {
            //string json = File.ReadAllText($"{Directory}/actormovies.json", Encoding.UTF8);
            //var actorMovies = JsonConvert.DeserializeObject<AddActorMovieDTO[]>(json);

            //foreach (var actorMovie in actorMovies)
            //{
            //    var am = new ActorMovie
            //    {
            //        ActorId = _context.Actors.ToList().FirstOrDefault(i => $"{i.FirstName} {i.LastName}" == actorMovie.Actor).Id,
            //        MovieId = _context.Movies.ToList().FirstOrDefault(i => i.Title == actorMovie.Movie).Id,
            //    };
            //    _context.ActorsMovies.Add(new ActorMovie
            //    {
            //        ActorId = _context.Actors.ToList().FirstOrDefault(i => $"{i.FirstName} {i.LastName}" == actorMovie.Actor).Id,
            //        MovieId = _context.Movies.ToList().FirstOrDefault(i => i.Title == actorMovie.Movie).Id,
            //    });
            //}
            //_context.SaveChanges();
        }
    }
}
