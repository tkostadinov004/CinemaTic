using Cinema.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Data
{
    public class CinemaDbContext : IdentityDbContext<ApplicationUser>
    {
        public CinemaDbContext(DbContextOptions<CinemaDbContext> options)
            : base(options)
        {
        }
        public DbSet<Data.Models.Cinema> Cinemas { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<ActorMovie> ActorsMovies { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<UserMovie> UsersMovies { get; set; }
        public DbSet<CinemaMovie> CinemasMovies { get; set; }
        public DbSet<VisitorCinema> VisitorsCinemas { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ActorMovie>().HasKey(i => new { i.ActorId, i.MovieId });
            modelBuilder.Entity<UserMovie>().HasKey(i => new { i.UserId, i.MovieId });
            modelBuilder.Entity<CinemaMovie>().HasKey(i => new { i.CinemaId, i.MovieId });
            modelBuilder.Entity<VisitorCinema>().HasKey(i => new { i.VisitorId, i.CinemaId });

            modelBuilder.Entity<Data.Models.Cinema>().HasOne(i => i.Owner).WithMany(o => o.CinemasOwned);
            modelBuilder.Entity<VisitorCinema>().HasOne(i => i.Cinema).WithMany(i => i.Visitors).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
