using Cinema.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Data
{
    public class CinemaContext : DbContext
    {
        public CinemaContext(DbContextOptions<CinemaContext> options) : base(options)
        {
        }

        public DbSet<Actor> Actors { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<ActorMovie> ActorsMovies { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ActorMovie>().HasKey(i => new { i.ActorId, i.MovieId });

            builder.Entity<Actor>().Property(i => i.Rating).HasColumnType("decimal").HasPrecision(1);
            builder.Entity<Movie>().Property(i => i.UserRating).HasColumnType("decimal").HasPrecision(1);
            builder.Entity<Movie>().Property(i => i.Price).HasColumnType("decimal").HasPrecision(1);
            builder.Entity<Seat>().Property(i => i.Price).HasColumnType("decimal").HasPrecision(1);
            builder.Entity<Ticket>().Property(i => i.Price).HasColumnType("decimal").HasPrecision(1);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }
    }
}
