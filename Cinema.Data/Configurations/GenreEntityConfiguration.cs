using Cinema.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace Cinema.Data.Configurations
{
    public class GenreEntityConfiguration : IEntityTypeConfiguration<Genre>
    {
        private const string Directory = "../Cinema.Data.Seeder/Datasets";
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.HasData(this.GetGenres());
        }
        private IEnumerable<Genre> GetGenres()
        {
            var genres = new List<Genre>()
            {
                new Genre( 1, "Drama" ),
                    new Genre( 2, "Crime" ),
                    new Genre( 3, "Adventure" ),
                    new Genre( 4, "Action" ),
                    new Genre( 5, "Biography" ),
                    new Genre( 6, "Western" ),
                    new Genre( 7, "Mystery" ),
                    new Genre( 8, "Horror" ),
                    new Genre( 9, "Comedy" ),
                    new Genre( 10, "Animation" ),
                    new Genre( 11, "Film-Noir" )
            };
            return genres;
        }
    }
}
