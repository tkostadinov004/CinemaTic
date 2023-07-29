using Cinema.Data.Enums;
using Cinema.Data.Models;
using Cinema.Data.Seeder.DTOs;
using Cinema.Data.Seeder.DTOs.New;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace Cinema.Data
{
    public class DbInitializer
    {
        private readonly CinemaDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private const string Directory = "../Cinema.Data.Seeder/Datasets";
        public DbInitializer(CinemaDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public void Run(IServiceProvider serviceProvider, bool shouldDeleteDB)
        {
            if (shouldDeleteDB)
            {
                //_context.Database.EnsureDeleted();
                //_context.Database.Migrate();
                //AddGenres();
                //AddActors(serviceProvider);
                //AddMovies(serviceProvider);
                //AddActorMovies();
                //AddRoles(serviceProvider);
                //AddUsers(serviceProvider);
                // AddCinemasMovies();
                //AddCinemasMoviesTimes();
                // AddCustomersCinemas();
                //AddTickets(serviceProvider);
                //AddUserRatings(serviceProvider);
              //  AddCinemas(serviceProvider);
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
        public void AddCinemasMovies()
        {
            var cinemas = _context.Cinemas.ToList();
            var movies = _context.Movies.ToList();
            var dates = Enumerable.Range(0, 1 + DateTime.Now.AddMonths(1).Subtract(DateTime.Now).Days)
          .Select(offset => DateTime.Now.AddDays(offset))
          .ToArray();

            var collection = new List<CinemaMovie>();
            for (int i = 0; i < 100; i++)
            {
                int cinemaIndex = new Random().Next(cinemas.Count());
                int movieIndex = new Random().Next(movies.Count());
                int fromDateIndex = new Random().Next(dates.Count());

                DateTime fromDate = dates[fromDateIndex];
                if (!collection.Any(i => i.CinemaId == cinemas[cinemaIndex].Id && i.MovieId == movies[movieIndex].Id))
                {
                    collection.Add(new CinemaMovie
                    {
                        CinemaId = cinemas[cinemaIndex].Id,
                        MovieId = movies[movieIndex].Id,
                        FromDate = fromDate,
                        ToDate = dates.Where(i => i >= fromDate).ToList()[new Random().Next(dates.Where(i => i >= fromDate).Count())],
                        TicketPrice = new Random().Next(5, 26)
                    });
                }
                else
                {
                    i--;
                }
            }
            _context.CinemasMovies.AddRange(collection);
            _context.SaveChanges();
        }
        public void AddCustomersCinemas()
        {
            var customers = _context.Users.ToList().Where(i => _userManager.IsInRoleAsync(i, "Customer").Result).ToList();
            var cinemas = _context.Cinemas.ToList();

            var collection = new List<CustomerCinema>();
            for (int i = 0; i < 100; i++)
            {
                int cinemaIndex = new Random().Next(cinemas.Count());
                int customerIndex = new Random().Next(customers.Count());
                if (!collection.Any(i => i.CinemaId == cinemas[cinemaIndex].Id && i.CustomerId == customers[customerIndex].Id))
                {
                    collection.Add(new CustomerCinema
                    {
                        CinemaId = cinemas[cinemaIndex].Id,
                        CustomerId = customers[customerIndex].Id
                    });
                }
                else
                {

                }
            }
            _context.CustomersCinemas.AddRange(collection);
            _context.SaveChanges();
        }
        public void AddCinemasMoviesTimes()
        {
            var cinemasMovies = _context.CinemasMovies.ToList();

            var collection = new List<CinemaMovieTime>();
            for (int i = 0; i < 200; i++)
            {
                int cinemaMovieIndex = new Random().Next(cinemasMovies.Count());
                var cinemaMovie = cinemasMovies[cinemaMovieIndex];
                var dates = Enumerable.Range(0, 1 + cinemaMovie.ToDate.Subtract(cinemaMovie.FromDate).Days)
.Select(offset => cinemaMovie.FromDate.AddDays(offset))
.ToArray();
                var randomDate = dates[new Random().Next(dates.Length)];
                collection.Add(new CinemaMovieTime
                {
                    CinemaId = cinemaMovie.CinemaId,
                    MovieId = cinemaMovie.MovieId,
                    ForDateTime = new DateTime(randomDate.Year, randomDate.Month, randomDate.Day, new Random().Next(8, 24), new int[] { 0, 10, 15, 20, 30, 40, 45, 50 }[new Random().Next(8)], 0)
                });
            }
            _context.CinemasMoviesTimes.AddRange(collection);
            _context.SaveChanges();
        }
        public void AddActors(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                string json = File.ReadAllText($"{Directory}/New/actors.json", Encoding.UTF8);
                var whe = scope.ServiceProvider.GetRequiredService<IHostingEnvironment>();
                var actors = JsonConvert.DeserializeObject<Seeder.DTOs.New.AddActorDTO[]>(json);
                foreach (var actor in actors)
                {
                    string name = string.Join(" ", actor.Name.Split().Select(i =>
                    {
                        return char.ToUpper(i[0]) + i[1..];
                    }));
                    string imageUrl = $"{string.Join("", actor.Name.Replace(":", "").Split())}.jpg";
                    using (var client = new HttpClient())
                    {
                        using (var s = client.GetStreamAsync(actor.Image))
                        {
                            using (var fs = new FileStream(Path.Combine(whe.WebRootPath, "client-images", "Actors", imageUrl), FileMode.OpenOrCreate))
                            {
                                s.Result.CopyTo(fs);
                            }
                        }
                    }
                    _context.Actors.Add(new Actor
                    {
                        FullName = name,
                        Birthdate = DateTime.ParseExact(actor.Birthday, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                        Nationality = new RegionInfo(actor.Nationality).EnglishName,
                        Rating = new Random().Next(1, 11),
                        ImageUrl = imageUrl
                    });
                }
                _context.SaveChanges();
            }
        }
        public void AddMovies(IServiceProvider serviceProvider)
        {
            //using (var scope = serviceProvider.CreateScope())
            //{
            //    string json = File.ReadAllText($"{Directory}/New/moviesActors.json", Encoding.UTF8);
            //    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            //    var whe = scope.ServiceProvider.GetRequiredService<IHostingEnvironment>();
            //    var movies = JsonConvert.DeserializeObject<Seeder.DTOs.New.AddMovieDTO[]>(json);
            //    foreach (var movie in movies)
            //    {
            //        var genre = movie.Genre.Split(", ")[0];
            //        string imageUrl = $"{string.Join("", movie.Title.Replace(":", "").Split())}.jpg";
            //        using (var client = new HttpClient())
            //        {
            //            using (var s = client.GetStreamAsync(movie.Poster))
            //            {
            //                using (var fs = new FileStream(Path.Combine(whe.WebRootPath, "client-images", "Movies", imageUrl), FileMode.OpenOrCreate))
            //                {
            //                    s.Result.CopyTo(fs);
            //                }
            //            }
            //        }
            //        var actors = movie.Actors.Split(", ");
            //        _context.Movies.Add(new Movie
            //        {
            //            Title = movie.Title,
            //            Genre = _context.Genres.FirstOrDefault(i => i.Name == genre) ?? new Genre { Name = genre },
            //            Description = movie.Plot,
            //            Actors = actors.Where(i => _context.Actors.FirstOrDefault(a => a.FullName == i) != null).Select(actor =>
            //            {
            //                var found = _context.Actors.FirstOrDefault(i => i.FullName == actor);
            //                return new Actor
            //                {
            //                    FullName = found.FullName,
            //                    Birthdate = found.Birthdate,
            //                    ImageUrl = found.ImageUrl,
            //                    Nationality = found.Nationality,
            //                    Rating = found.Rating
            //                };
            //            }).ToList(),
            //            RunningTime = int.Parse(movie.Runtime.Split()[0]),
            //            TrailerUrl = "",
            //            Director = movie.Director,
            //            AddedById = userManager.FindByEmailAsync("owner@owner.com").Result.Id,
            //            UserRating = 0,
            //            RatingCount = 0,
            //            ImageUrl = imageUrl
            //        });
            //        _context.SaveChanges();
            //    }
            //}
        }
        public void AddCinemas(IServiceProvider serviceProvider)
        {
            List<Sector> DefineSectorsAsync(int rows, int cols)
            {
                char startLetter = 'A';

                List<Sector> sectors = new List<Sector>();
                for (int i = 1; i <= rows; i += 15)
                {
                    for (int j = 1; j <= cols; j += 15)
                    {
                        int endRow = i + 15 - 1;
                        int endCol = j + 15 - 1;

                        sectors.Add(new Sector
                        {
                            SectorName = startLetter.ToString(),
                            StartRow = i,
                            StartCol = j,
                            EndRow = endRow > rows ? rows : endRow,
                            EndCol = endCol > cols ? cols : endCol
                        });
                        startLetter++;
                    }
                }
                return sectors;
            }
            string[] logos = new string[]
           {
              //  "https://www.freeiconspng.com/thumbs/logo-design/blank-bird-logo-design-idea-png-15.png",
                //"https://static.vecteezy.com/system/resources/thumbnails/011/653/653/small/eco-friendly-smart-city-logo-design-blue-fullcolor-png.png",
               // "https://img.freepik.com/free-icon/diamond_318-195446.jpg",
                //"https://www.freeiconspng.com/thumbs/logo-design/rainbow-logo-design-transparent-0.png",
                //"https://www.freeiconspng.com/thumbs/logo-design/blank-logo-design-for-brand-13.png",
                //"https://www.freeiconspng.com/thumbs/logo-design/logo-design-blank-circle-blue-and-orange-png-2.png",
                "https://cdn.freebiesupply.com/logos/large/2x/jest-logo-png-transparent.png"
           };
            using (var scope = serviceProvider.CreateScope())
            {
                string json = File.ReadAllText($"{Directory}/New/cinemas.json", Encoding.UTF8);
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var whe = scope.ServiceProvider.GetRequiredService<IHostingEnvironment>();

                var cinemas = JsonConvert.DeserializeObject<Seeder.DTOs.New.AddCinemaDTO[]>(json);
                foreach (var cinema in cinemas)
                {
                    string imageUrl = $"{string.Join("", cinema.Name.Replace(":", "").Split())}.jpg";
                    using (var client = new HttpClient())
                    {
                        using (var s = client.GetStreamAsync(logos[new Random().Next(logos.Length)]))
                        {
                            using (var fs = new FileStream(Path.Combine(whe.WebRootPath, "client-images", "Cinemas", imageUrl), FileMode.OpenOrCreate))
                            {
                                s.Result.CopyTo(fs);
                            }
                        }
                    }
                    var owners = _context.Users.ToList().Where(i => _userManager.IsInRoleAsync(i, "Owner").Result).ToList();
                    _context.Cinemas.Add(new Models.Cinema
                    {
                        Name = cinema.Name,
                        Description = cinema.Description,
                        SeatRows = int.Parse(cinema.Rows),
                        SeatCols = int.Parse(cinema.Columns),
                        FoundedOn = DateTime.ParseExact(cinema.FoundingDate, "MM d, yyyy", CultureInfo.InvariantCulture),
                        Owner = owners[new Random().Next(owners.Count)],
                        ApprovalStatus = ApprovalStatus.Approved,
                        Sectors = DefineSectorsAsync(int.Parse(cinema.Rows), int.Parse(cinema.Columns)),
                        ImageUrl = imageUrl,
                        BackgroundColor = "#000000",
                        ButtonBackgroundColor = "#FFFFFF",
                        ButtonTextColor = "#000000",
                        TextColor = "#FFFFFF",
                        AccentColor = "#FF0000",
                        BoardColor = "#FFFFFF",
                    });
                    _context.SaveChanges();
                }
            }
        }
        public void AddActorMovies()
        {
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
        public void AddTickets(IServiceProvider serviceProvider)
        {
            var tickets = new List<Ticket>();
            var cinemasMovies = _context.CinemasMovies.ToList();
            var sectors = _context.Sectors.ToList();

            using (var scope = serviceProvider.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                var customers = _context.Users.ToList().Where(i => _userManager.IsInRoleAsync(i, "Customer").Result).ToList();
                for (int i = 0; i < 200; i++)
                {
                    int cinemaMovieIndex = new Random().Next(cinemasMovies.Count());
                    int sectorsIndex = new Random().Next(sectors.Count());
                    int customerIndex = new Random().Next(customers.Count());

                    var dates = _context.CinemasMoviesTimes.ToList().Where(i => i.CinemaId == cinemasMovies[cinemaMovieIndex].CinemaId && i.MovieId == cinemasMovies[cinemaMovieIndex].MovieId).Select(i => i.ForDateTime).ToList();
                    int datesIndex = new Random().Next(dates.Count());
                    int row = new Random().Next(sectors[sectorsIndex].StartRow, sectors[sectorsIndex].EndRow + 1);
                    int col = new Random().Next(sectors[sectorsIndex].StartCol, sectors[sectorsIndex].EndCol + 1);
                    string serialNumber = $"R{row}C{col}";
                    if (!tickets.Any(i => i.MovieId == cinemasMovies[cinemaMovieIndex].MovieId && i.CinemaId == cinemasMovies[cinemaMovieIndex].CinemaId && i.ForDate == dates[datesIndex] && i.SectorId == sectors[sectorsIndex].Id && i.CustomerId == customers[customerIndex].Id && i.SerialNumber == serialNumber))
                    {
                        tickets.Add(new Ticket
                        {
                            SerialNumber = serialNumber,
                            MovieId = cinemasMovies[cinemaMovieIndex].MovieId,
                            CinemaId = cinemasMovies[cinemaMovieIndex].CinemaId,
                            CustomerId = customers[customerIndex].Id,
                            SectorId = sectors[sectorsIndex].Id,
                            Price = cinemasMovies[cinemaMovieIndex].TicketPrice,
                            ForDate = dates[datesIndex]
                        });
                    }     
                }
                _context.Tickets.AddRange(tickets);
                _context.SaveChanges();
            }
        }
        public void AddUserRatings(IServiceProvider serviceProvider)
        {
            var ratings = new List<UserMovie>();
            var cinemasMovies = _context.CinemasMovies.ToList();
            var customersCinemas = _context.CustomersCinemas.ToList();

            using (var scope = serviceProvider.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                var customers = _context.Users.ToList().Where(i => _userManager.IsInRoleAsync(i, "Customer").Result).ToList();
                for (int i = 0; i < 100; i++)
                {
                    int cinemaMovieIndex = new Random().Next(cinemasMovies.Count);
                    int customerCinemaIndex = new Random().Next(customersCinemas.Count);

                    var cinemaMovie = cinemasMovies[cinemaMovieIndex];
                    var customerCinema = customersCinemas[customerCinemaIndex];
                    if(customerCinema.CinemaId == cinemaMovie.CinemaId)
                    {
                        if(!ratings.Any(i => i.CustomerId == customerCinema.CustomerId && i.MovieId == cinemaMovie.MovieId))
                        {
                            ratings.Add(new UserMovie
                            {
                                CustomerId = customerCinema.CustomerId,
                                MovieId = cinemaMovie.MovieId,
                                Rating = new Random().Next(1, 11)
                            });
                        }
                    }
                }
                _context.UsersMovies.AddRange(ratings);
                _context.SaveChanges();
            }
        }

    }
}
