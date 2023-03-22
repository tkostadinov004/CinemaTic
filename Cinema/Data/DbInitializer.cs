using Cinema.Data;
using Cinema.Models;
using DbSeeder.Data.DTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

        public void Run()
        {
            //AddGenres();
            //AddActors();
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
    }
}
