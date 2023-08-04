using CinemaTic.Data.Configurations;
using CinemaTic.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CinemaTic.Data
{
    public class CinemaDbContext : IdentityDbContext<ApplicationUser>
    {
        public CinemaDbContext(DbContextOptions<CinemaDbContext> options)
            : base(options)
        {
        }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Sector> Sectors { get; set; }
        public DbSet<UserMovie> UsersMovies { get; set; }
        public DbSet<CustomerCinema> CustomersCinemas { get; set; }
        public DbSet<CinemaMovie> CinemasMovies { get; set; }
        public DbSet<ActionLog> ActionLogs { get; set; }
        public DbSet<ActorMovie> ActorsMovies { get; set; }
        public DbSet<CinemaMovieTime> CinemasMoviesTimes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserMovie>().HasKey(i => new { i.CustomerId, i.MovieId });
            modelBuilder.Entity<CustomerCinema>().HasKey(i => new { i.CustomerId, i.CinemaId });
            modelBuilder.Entity<CinemaMovie>().HasKey(i => new { i.CinemaId, i.MovieId });
            modelBuilder.Entity<ActorMovie>().HasKey(i => new { i.ActorId, i.MovieId });

            modelBuilder.Entity<Data.Models.Cinema>().HasOne(i => i.Owner).WithMany(o => o.CinemasOwned);
            modelBuilder.Entity<CustomerCinema>().HasOne(i => i.Cinema).WithMany(i => i.Customers).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Ticket>().HasOne(i => i.Cinema).WithMany(i => i.Tickets).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Movie>().HasOne(i => i.AddedBy).WithMany(a => a.MoviesAdded).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Movie>().HasOne(i => i.Genre).WithMany(g => g.Movies).IsRequired(false).OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<ActionLog>().HasOne(i => i.User).WithMany(a => a.UserActions);

            modelBuilder.Entity<Ticket>().HasOne(i => i.Sector).WithMany(s => s.Tickets).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
