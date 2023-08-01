﻿using CinemaTic.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace CinemaTic.Data.Configurations
{
    public class ActorMovieEntityConfiguration : IEntityTypeConfiguration<ActorMovie>
    {
        private const string Directory = "../Cinema.Data.Seeder/Datasets";
        public void Configure(EntityTypeBuilder<ActorMovie> builder)
        {
            builder.HasData(this.GetActorMovies());
        }
        private IEnumerable<ActorMovie> GetActorMovies()
        {
            var actorsMovies = new List<ActorMovie>()
            {
                 new ActorMovie( 1, 1 ),
                    new ActorMovie( 2, 1 ),
                    new ActorMovie( 2, 22 ),
                    new ActorMovie( 3, 1 ),
                    new ActorMovie( 4, 2 ),
                    new ActorMovie( 4, 34 ),
                    new ActorMovie( 5, 2 ),
                    new ActorMovie( 5, 3 ),
                    new ActorMovie( 6, 2 ),
                    new ActorMovie( 7, 3 ),
                    new ActorMovie( 7, 15 ),
                    new ActorMovie( 7, 57 ),
                    new ActorMovie( 7, 78 ),
                    new ActorMovie( 8, 3 ),
                    new ActorMovie( 8, 34 ),
                    new ActorMovie( 9, 4 ),
                    new ActorMovie( 10, 4 ),
                    new ActorMovie( 11, 5 ),
                    new ActorMovie( 12, 5 ),
                    new ActorMovie( 13, 5 ),
                    new ActorMovie( 14, 6 ),
                    new ActorMovie( 14, 55 ),
                    new ActorMovie( 14, 66 ),
                    new ActorMovie( 15, 6 ),
                    new ActorMovie( 16, 6 ),
                    new ActorMovie( 17, 7 ),
                    new ActorMovie( 17, 24 ),
                    new ActorMovie( 18, 8 ),
                    new ActorMovie( 19, 8 ),
                    new ActorMovie( 20, 8 ),
                    new ActorMovie( 21, 9 ),
                    new ActorMovie( 21, 12 ),
                    new ActorMovie( 21, 20 ),
                    new ActorMovie( 22, 9 ),
                    new ActorMovie( 22, 20 ),
                    new ActorMovie( 24, 10 ),
                    new ActorMovie( 24, 22 ),
                    new ActorMovie( 25, 10 ),
                    new ActorMovie( 25, 33 ),
                    new ActorMovie( 26, 11 ),
                    new ActorMovie( 26, 16 ),
                    new ActorMovie( 26, 75 ),
                     new ActorMovie( 27, 11 ),
                    new ActorMovie( 27, 16 ),
                    new ActorMovie( 27, 27 ),
                    new ActorMovie( 27, 75 ),
                    new ActorMovie( 27, 100 ),
                    new ActorMovie( 28, 11 ),
                    new ActorMovie( 28, 16 ),
                    new ActorMovie( 28, 75 ),
                    new ActorMovie( 29, 12 ),
                    new ActorMovie( 30, 13 ),
                    new ActorMovie( 30, 46 ),
                    new ActorMovie( 30, 49 ),
                    new ActorMovie( 30, 86 ),
                    new ActorMovie( 31, 13 ),
                    new ActorMovie( 32, 14 ),
                    new ActorMovie( 32, 49 ),
                    new ActorMovie( 32, 53 ),
                    new ActorMovie( 34, 15 ),
                    new ActorMovie( 35, 15 ),
                    new ActorMovie( 36, 18 ),
                    new ActorMovie( 36, 36 ),
                    new ActorMovie( 36, 59 ),
                    new ActorMovie( 36, 64 ),
                    new ActorMovie( 37, 18 ),
                    new ActorMovie( 38, 18 ),
                    new ActorMovie( 39, 19 ),
                    new ActorMovie( 40, 19 ),
                    new ActorMovie( 42, 22 ),
                    new ActorMovie( 42, 26 ),
                    new ActorMovie( 42, 58 ),
                    new ActorMovie( 42, 84 ),
                    new ActorMovie( 43, 23 ),
                    new ActorMovie( 43, 57 ),
                    new ActorMovie( 44, 23 ),
                    new ActorMovie( 45, 24 ),
                    new ActorMovie( 46, 24 ),
                    new ActorMovie( 47, 25 ),
                    new ActorMovie( 47, 76 ),
                    new ActorMovie( 48, 25 ),
                    new ActorMovie( 49, 26 ),
                    new ActorMovie( 50, 26 ),
                    new ActorMovie( 51, 27 ),
                    new ActorMovie( 52, 28 ),
                    new ActorMovie( 52, 29 ),
                    new ActorMovie( 52, 52 ),
                    new ActorMovie( 53, 28 ),
                    new ActorMovie( 54, 29 ),
                    new ActorMovie( 55, 29 ),
                    new ActorMovie( 56, 30 ),
                    new ActorMovie( 57, 30 ),
                    new ActorMovie( 58, 30 ),
                    new ActorMovie( 59, 31 ),
                    new ActorMovie( 60, 31 ),
                    new ActorMovie( 61, 31 ),
                    new ActorMovie( 62, 32 ),
                    new ActorMovie( 63, 33 ),
                    new ActorMovie( 63, 35 ),
                    new ActorMovie( 65, 34 ),
                    new ActorMovie( 66, 35 ),
                    new ActorMovie( 67, 35 ),
                    new ActorMovie( 68, 36 ),
                    new ActorMovie( 68, 49 ),
                    new ActorMovie( 69, 36 ),
                    new ActorMovie( 70, 37 ),
                    new ActorMovie( 70, 84 ),
                    new ActorMovie( 71, 37 ),
                    new ActorMovie( 72, 39 ),
                    new ActorMovie( 73, 39 ),
                    new ActorMovie( 74, 40 ),
                    new ActorMovie( 74, 56 ),
                    new ActorMovie( 75, 40 ),
                    new ActorMovie( 76, 40 ),
                    new ActorMovie( 77, 42 ),
                    new ActorMovie( 78, 42 ),
                    new ActorMovie( 79, 43 ),
                    new ActorMovie( 80, 43 ),
                    new ActorMovie( 81, 44 ),
                    new ActorMovie( 82, 44 ),
                    new ActorMovie( 83, 44 ),
                    new ActorMovie( 84, 45 ),
                    new ActorMovie( 85, 46 ),
                    new ActorMovie( 86, 46 ),
                    new ActorMovie( 87, 47 ),
                    new ActorMovie( 88, 47 ),
                     new ActorMovie( 89, 48 ),
                    new ActorMovie( 89, 77 ),
                    new ActorMovie( 90, 51 ),
                    new ActorMovie( 91, 52 ),
                    new ActorMovie( 92, 53 ),
                    new ActorMovie( 93, 53 ),
                    new ActorMovie( 95, 54 ),
                    new ActorMovie( 96, 54 ),
                    new ActorMovie( 97, 55 ),
                    new ActorMovie( 98, 55 ),
                    new ActorMovie( 99, 56 ),
                    new ActorMovie( 100, 57 ),
                    new ActorMovie( 101, 58 ),
                    new ActorMovie( 102, 58 ),
                    new ActorMovie( 103, 59 ),
                    new ActorMovie( 104, 59 ),
                    new ActorMovie( 105, 60 ),
                    new ActorMovie( 105, 84 ),
                    new ActorMovie( 106, 60 ),
                    new ActorMovie( 107, 60 ),
                    new ActorMovie( 108, 62 ),
                    new ActorMovie( 109, 64 ),
                    new ActorMovie( 110, 64 ),
                    new ActorMovie( 111, 66 ),
                    new ActorMovie( 112, 66 ),
                    new ActorMovie( 115, 68 ),
                    new ActorMovie( 116, 68 ),
                    new ActorMovie( 117, 69 ),
                    new ActorMovie( 120, 70 ),
                    new ActorMovie( 121, 71 ),
                    new ActorMovie( 122, 71 ),
                    new ActorMovie( 123, 71 ),
                    new ActorMovie( 124, 73 ),
                    new ActorMovie( 125, 73 ),
                    new ActorMovie( 126, 73 ),
                    new ActorMovie( 127, 78 ),
                    new ActorMovie( 129, 79 ),
                    new ActorMovie( 130, 79 ),
                    new ActorMovie( 131, 79 ),
                    new ActorMovie( 132, 80 ),
                    new ActorMovie( 133, 80 ),
                    new ActorMovie( 134, 80 ),
                     new ActorMovie( 135, 81 ),
                    new ActorMovie( 136, 81 ),
                    new ActorMovie( 137, 81 ),
                    new ActorMovie( 138, 83 ),
                    new ActorMovie( 139, 83 ),
                    new ActorMovie( 140, 86 ),
                    new ActorMovie( 141, 87 ),
                    new ActorMovie( 142, 89 ),
                    new ActorMovie( 143, 89 ),
                    new ActorMovie( 144, 92 ),
                    new ActorMovie( 145, 92 ),
                    new ActorMovie( 146, 92 ),
                    new ActorMovie( 146, 98 ),
                    new ActorMovie( 147, 93 ),
                    new ActorMovie( 148, 93 ),
                    new ActorMovie( 149, 94 ),
                    new ActorMovie( 150, 95 ),
                    new ActorMovie( 152, 96 ),
                    new ActorMovie( 153, 97 ),
                    new ActorMovie( 154, 97 ),
                    new ActorMovie( 156, 100 ),
                    new ActorMovie( 157, 100 )
            };

            return actorsMovies;
        }
    }
}