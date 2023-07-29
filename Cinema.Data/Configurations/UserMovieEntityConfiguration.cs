using Cinema.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace Cinema.Data.Configurations
{
    public class UserMovieEntityConfiguration : IEntityTypeConfiguration<UserMovie>
    {
        public void Configure(EntityTypeBuilder<UserMovie> builder)
        {
           builder.HasData(this.GetRatings());
        }
        private IEnumerable<UserMovie> GetRatings()
        {
            var ratings = new List<UserMovie>();
            var cinemasMovies = new List<CinemaMovie>()
            {
                 new CinemaMovie( 1, 32, new DateTime(2023, 8, 5, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9222), 14m, new DateTime(2023, 8, 20, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9284) ),
                    new CinemaMovie( 1, 47, new DateTime(2023, 8, 6, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9228), 21m, new DateTime(2023, 8, 11, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9263) ),
                    new CinemaMovie( 1, 70, new DateTime(2023, 8, 18, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9279), 17m, new DateTime(2023, 8, 27, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9347) ),
                    new CinemaMovie( 2, 59, new DateTime(2023, 8, 14, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9270), 24m, new DateTime(2023, 8, 27, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9347) ),
                    new CinemaMovie( 3, 30, new DateTime(2023, 7, 29, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9189), 5m, new DateTime(2023, 8, 12, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9265) ),
                    new CinemaMovie( 3, 52, new DateTime(2023, 8, 11, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9263), 20m, new DateTime(2023, 8, 14, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9270) ),
                    new CinemaMovie( 3, 85, new DateTime(2023, 8, 9, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9241), 17m, new DateTime(2023, 8, 9, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9241) ),
                    new CinemaMovie( 4, 67, new DateTime(2023, 7, 31, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9201), 10m, new DateTime(2023, 8, 10, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9244) ),
                    new CinemaMovie( 5, 67, new DateTime(2023, 8, 4, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9220), 25m, new DateTime(2023, 8, 11, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9263) ),
                    new CinemaMovie( 6, 18, new DateTime(2023, 8, 22, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9293), 15m, new DateTime(2023, 8, 27, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9347) ),
                    new CinemaMovie( 6, 96, new DateTime(2023, 8, 20, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9284), 25m, new DateTime(2023, 8, 20, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9284) ),
                    new CinemaMovie( 7, 5, new DateTime(2023, 8, 3, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9218), 21m, new DateTime(2023, 8, 8, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9239) ),
                    new CinemaMovie( 9, 40, new DateTime(2023, 8, 18, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9279), 20m, new DateTime(2023, 8, 28, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9352) ),
                    new CinemaMovie( 9, 50, new DateTime(2023, 8, 14, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9270), 7m, new DateTime(2023, 8, 22, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9293) ),
                    new CinemaMovie( 9, 86, new DateTime(2023, 8, 28, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9352), 14m, new DateTime(2023, 8, 28, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9352) ),
                    new CinemaMovie( 10, 23, new DateTime(2023, 8, 3, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9218), 8m, new DateTime(2023, 8, 28, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9352) ),
                    new CinemaMovie( 11, 17, new DateTime(2023, 8, 26, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9344), 9m, new DateTime(2023, 8, 27, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9347) ),
                    new CinemaMovie( 11, 22, new DateTime(2023, 8, 5, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9222), 15m, new DateTime(2023, 8, 27, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9347) ),
                    new CinemaMovie( 11, 38, new DateTime(2023, 8, 18, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9279), 16m, new DateTime(2023, 8, 28, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9352) ),
                    new CinemaMovie( 11, 41, new DateTime(2023, 8, 7, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9236), 6m, new DateTime(2023, 8, 18, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9279) ),
                    new CinemaMovie( 12, 44, new DateTime(2023, 8, 27, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9347), 13m, new DateTime(2023, 8, 28, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9352) ),
                    new CinemaMovie( 12, 100, new DateTime(2023, 8, 3, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9218), 25m, new DateTime(2023, 8, 26, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9344) ),
                    new CinemaMovie( 13, 12, new DateTime(2023, 7, 29, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9189), 8m, new DateTime(2023, 8, 13, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9268) ),
                    new CinemaMovie( 13, 57, new DateTime(2023, 8, 4, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9220), 5m, new DateTime(2023, 8, 23, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9296) ),
                    new CinemaMovie( 14, 3, new DateTime(2023, 8, 17, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9277), 11m, new DateTime(2023, 8, 21, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9286) ),
                    new CinemaMovie( 14, 29, new DateTime(2023, 8, 9, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9241), 12m, new DateTime(2023, 8, 25, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9308) ),
                    new CinemaMovie( 15, 72, new DateTime(2023, 8, 23, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9296), 16m, new DateTime(2023, 8, 27, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9347) ),
                    new CinemaMovie( 16, 79, new DateTime(2023, 8, 8, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9239), 21m, new DateTime(2023, 8, 21, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9286) ),
                    new CinemaMovie( 17, 2, new DateTime(2023, 8, 14, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9270), 15m, new DateTime(2023, 8, 16, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9275) ),
                    new CinemaMovie( 17, 38, new DateTime(2023, 8, 11, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9263), 5m, new DateTime(2023, 8, 28, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9352) ),
                    new CinemaMovie( 18, 17, new DateTime(2023, 8, 5, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9222), 14m, new DateTime(2023, 8, 25, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9308) ),
                    new CinemaMovie( 19, 88, new DateTime(2023, 8, 3, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9218), 19m, new DateTime(2023, 8, 21, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9286) ),
                    new CinemaMovie( 19, 93, new DateTime(2023, 8, 8, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9239), 13m, new DateTime(2023, 8, 15, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9272) ),
                    new CinemaMovie( 20, 9, new DateTime(2023, 8, 7, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9236), 17m, new DateTime(2023, 8, 24, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9299) ),
                    new CinemaMovie( 20, 55, new DateTime(2023, 8, 23, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9296), 18m, new DateTime(2023, 8, 27, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9347) ),
                    new CinemaMovie( 20, 82, new DateTime(2023, 8, 5, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9222), 23m, new DateTime(2023, 8, 19, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9282) ),
                    new CinemaMovie( 21, 20, new DateTime(2023, 7, 30, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9194), 9m, new DateTime(2023, 8, 18, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9279) ),
                    new CinemaMovie( 21, 27, new DateTime(2023, 8, 22, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9293), 10m, new DateTime(2023, 8, 24, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9299) ),
                    new CinemaMovie( 22, 29, new DateTime(2023, 8, 27, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9347), 21m, new DateTime(2023, 8, 28, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9352) ),
                    new CinemaMovie( 22, 48, new DateTime(2023, 8, 2, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9215), 20m, new DateTime(2023, 8, 9, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9241) ),
                    new CinemaMovie( 23, 32, new DateTime(2023, 8, 20, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9284), 8m, new DateTime(2023, 8, 25, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9308) ),
                    new CinemaMovie( 24, 36, new DateTime(2023, 8, 27, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9347), 16m, new DateTime(2023, 8, 27, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9347) ),
                    new CinemaMovie( 24, 76, new DateTime(2023, 8, 11, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9263), 7m, new DateTime(2023, 8, 26, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9344) ),
                    new CinemaMovie( 24, 88, new DateTime(2023, 8, 8, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9239), 5m, new DateTime(2023, 8, 23, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9296) ),
                    new CinemaMovie( 25, 92, new DateTime(2023, 8, 12, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9265), 21m, new DateTime(2023, 8, 26, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9344) ),
                    new CinemaMovie( 25, 98, new DateTime(2023, 8, 24, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9299), 5m, new DateTime(2023, 8, 25, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9308) ),
                    new CinemaMovie( 26, 58, new DateTime(2023, 8, 4, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9220), 13m, new DateTime(2023, 8, 13, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9268) ),
                    new CinemaMovie( 27, 7, new DateTime(2023, 7, 31, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9201), 10m, new DateTime(2023, 8, 18, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9279) ),
                    new CinemaMovie( 28, 100, new DateTime(2023, 8, 22, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9293), 15m, new DateTime(2023, 8, 27, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9347) ),
                    new CinemaMovie( 29, 41, new DateTime(2023, 8, 7, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9236), 5m, new DateTime(2023, 8, 25, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9308) ),
                    new CinemaMovie( 29, 74, new DateTime(2023, 8, 8, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9239), 11m, new DateTime(2023, 8, 16, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9275) ),
                    new CinemaMovie( 30, 26, new DateTime(2023, 8, 23, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9296), 19m, new DateTime(2023, 8, 24, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9299) ),
                    new CinemaMovie( 30, 71, new DateTime(2023, 8, 14, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9270), 20m, new DateTime(2023, 8, 26, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9344) ),
                    new CinemaMovie( 31, 4, new DateTime(2023, 8, 19, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9282), 22m, new DateTime(2023, 8, 26, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9344) ),
                    new CinemaMovie( 31, 6, new DateTime(2023, 7, 31, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9201), 13m, new DateTime(2023, 8, 3, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9218) ),
                    new CinemaMovie( 31, 31, new DateTime(2023, 8, 5, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9222), 5m, new DateTime(2023, 8, 5, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9222) ),
                    new CinemaMovie( 31, 49, new DateTime(2023, 8, 10, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9244), 11m, new DateTime(2023, 8, 27, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9347) ),
                    new CinemaMovie( 31, 53, new DateTime(2023, 8, 18, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9279), 8m, new DateTime(2023, 8, 18, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9279) ),
                    new CinemaMovie( 32, 12, new DateTime(2023, 8, 8, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9239), 12m, new DateTime(2023, 8, 11, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9263) ),
                    new CinemaMovie( 32, 62, new DateTime(2023, 8, 9, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9241), 11m, new DateTime(2023, 8, 11, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9263) ),
                    new CinemaMovie( 33, 38, new DateTime(2023, 8, 21, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9286), 22m, new DateTime(2023, 8, 22, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9293) ),
                    new CinemaMovie( 34, 24, new DateTime(2023, 7, 31, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9201), 21m, new DateTime(2023, 8, 15, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9272) ),
                     new CinemaMovie( 35, 42, new DateTime(2023, 8, 4, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9220), 8m, new DateTime(2023, 8, 19, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9282) ),
                    new CinemaMovie( 35, 63, new DateTime(2023, 7, 31, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9201), 6m, new DateTime(2023, 8, 21, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9286) ),
                    new CinemaMovie( 35, 92, new DateTime(2023, 8, 1, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9207), 21m, new DateTime(2023, 8, 3, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9218) ),
                    new CinemaMovie( 36, 2, new DateTime(2023, 8, 15, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9272), 18m, new DateTime(2023, 8, 20, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9284) ),
                    new CinemaMovie( 36, 52, new DateTime(2023, 8, 24, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9299), 11m, new DateTime(2023, 8, 27, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9347) ),
                    new CinemaMovie( 37, 7, new DateTime(2023, 8, 27, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9347), 24m, new DateTime(2023, 8, 27, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9347) ),
                    new CinemaMovie( 37, 64, new DateTime(2023, 8, 24, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9299), 13m, new DateTime(2023, 8, 26, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9344) ),
                    new CinemaMovie( 37, 68, new DateTime(2023, 8, 28, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9352), 7m, new DateTime(2023, 8, 28, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9352) ),
                    new CinemaMovie( 38, 7, new DateTime(2023, 8, 9, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9241), 9m, new DateTime(2023, 8, 14, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9270) ),
                    new CinemaMovie( 38, 26, new DateTime(2023, 8, 2, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9215), 15m, new DateTime(2023, 8, 13, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9268) ),
                    new CinemaMovie( 38, 36, new DateTime(2023, 8, 13, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9268), 21m, new DateTime(2023, 8, 23, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9296) ),
                    new CinemaMovie( 38, 67, new DateTime(2023, 7, 30, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9194), 12m, new DateTime(2023, 8, 2, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9215) ),
                    new CinemaMovie( 38, 93, new DateTime(2023, 8, 3, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9218), 22m, new DateTime(2023, 8, 16, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9275) ),
                    new CinemaMovie( 39, 90, new DateTime(2023, 8, 14, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9270), 17m, new DateTime(2023, 8, 21, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9286) ),
                    new CinemaMovie( 39, 96, new DateTime(2023, 7, 29, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9189), 20m, new DateTime(2023, 8, 6, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9228) ),
                    new CinemaMovie( 40, 22, new DateTime(2023, 8, 16, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9275), 20m, new DateTime(2023, 8, 22, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9293) ),
                    new CinemaMovie( 41, 81, new DateTime(2023, 7, 30, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9194), 18m, new DateTime(2023, 8, 6, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9228) ),
                    new CinemaMovie( 41, 85, new DateTime(2023, 8, 15, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9272), 25m, new DateTime(2023, 8, 26, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9344) ),
                    new CinemaMovie( 42, 21, new DateTime(2023, 8, 2, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9215), 15m, new DateTime(2023, 8, 12, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9265) ),
                    new CinemaMovie( 42, 37, new DateTime(2023, 8, 12, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9265), 15m, new DateTime(2023, 8, 27, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9347) ),
                    new CinemaMovie( 42, 41, new DateTime(2023, 7, 29, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9189), 22m, new DateTime(2023, 7, 31, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9201) ),
                    new CinemaMovie( 42, 55, new DateTime(2023, 8, 14, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9270), 10m, new DateTime(2023, 8, 17, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9277) ),
                    new CinemaMovie( 42, 79, new DateTime(2023, 8, 27, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9347), 9m, new DateTime(2023, 8, 27, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9347) ),
                    new CinemaMovie( 43, 86, new DateTime(2023, 8, 10, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9244), 21m, new DateTime(2023, 8, 26, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9344) ),
                    new CinemaMovie( 44, 27, new DateTime(2023, 7, 30, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9194), 9m, new DateTime(2023, 8, 9, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9241) ),
                    new CinemaMovie( 45, 81, new DateTime(2023, 8, 25, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9308), 20m, new DateTime(2023, 8, 27, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9347) ),
                    new CinemaMovie( 46, 15, new DateTime(2023, 8, 6, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9228), 12m, new DateTime(2023, 8, 24, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9299) ),
                    new CinemaMovie( 46, 86, new DateTime(2023, 8, 8, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9239), 24m, new DateTime(2023, 8, 22, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9293) ),
                    new CinemaMovie( 47, 41, new DateTime(2023, 8, 21, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9286), 12m, new DateTime(2023, 8, 24, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9299) ),
                    new CinemaMovie( 47, 49, new DateTime(2023, 8, 9, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9241), 16m, new DateTime(2023, 8, 14, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9270) ),
                    new CinemaMovie( 47, 76, new DateTime(2023, 7, 30, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9194), 5m, new DateTime(2023, 8, 27, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9347) ),
                    new CinemaMovie( 48, 51, new DateTime(2023, 8, 7, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9236), 11m, new DateTime(2023, 8, 24, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9299) ),
                    new CinemaMovie( 48, 56, new DateTime(2023, 8, 13, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9268), 13m, new DateTime(2023, 8, 25, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9308) ),
                    new CinemaMovie( 49, 21, new DateTime(2023, 8, 6, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9228), 14m, new DateTime(2023, 8, 26, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9344) ),
                    new CinemaMovie( 49, 43, new DateTime(2023, 8, 1, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9207), 21m, new DateTime(2023, 8, 7, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9236) ),
                    new CinemaMovie( 49, 48, new DateTime(2023, 8, 7, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9236), 10m, new DateTime(2023, 8, 20, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9284) ),
                    new CinemaMovie( 49, 53, new DateTime(2023, 8, 5, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9222), 12m, new DateTime(2023, 8, 10, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9244) ),
                    new CinemaMovie( 50, 61, new DateTime(2023, 8, 9, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9241), 7m, new DateTime(2023, 8, 17, 14, 5, 23, 847, DateTimeKind.Local).AddTicks(9277) )
            };
            var customersCinemas = new List<CustomerCinema>()
           {
               new CustomerCinema( 2, "191d6e78-88fb-40ce-b85c-d8bcf4a1ae4c" ),
                    new CustomerCinema( 12, "191d6e78-88fb-40ce-b85c-d8bcf4a1ae4c" ),
                    new CustomerCinema( 19, "191d6e78-88fb-40ce-b85c-d8bcf4a1ae4c" ),
                    new CustomerCinema( 20, "191d6e78-88fb-40ce-b85c-d8bcf4a1ae4c" ),
                    new CustomerCinema( 29, "191d6e78-88fb-40ce-b85c-d8bcf4a1ae4c" ),
                    new CustomerCinema( 31, "191d6e78-88fb-40ce-b85c-d8bcf4a1ae4c" ),
                    new CustomerCinema( 37, "191d6e78-88fb-40ce-b85c-d8bcf4a1ae4c" ),
                    new CustomerCinema( 41, "191d6e78-88fb-40ce-b85c-d8bcf4a1ae4c" ),
                    new CustomerCinema( 43, "191d6e78-88fb-40ce-b85c-d8bcf4a1ae4c" ),
                    new CustomerCinema( 49, "191d6e78-88fb-40ce-b85c-d8bcf4a1ae4c" ),
                    new CustomerCinema( 50, "191d6e78-88fb-40ce-b85c-d8bcf4a1ae4c" ),
                    new CustomerCinema( 7, "1c850a33-6e0a-4c03-bb2d-c5a388042364" ),
                    new CustomerCinema( 9, "1c850a33-6e0a-4c03-bb2d-c5a388042364" ),
                    new CustomerCinema( 10, "1c850a33-6e0a-4c03-bb2d-c5a388042364" ),
                    new CustomerCinema( 11, "1c850a33-6e0a-4c03-bb2d-c5a388042364" ),
                    new CustomerCinema( 12, "1c850a33-6e0a-4c03-bb2d-c5a388042364" ),
                    new CustomerCinema( 20, "1c850a33-6e0a-4c03-bb2d-c5a388042364" ),
                    new CustomerCinema( 39, "1c850a33-6e0a-4c03-bb2d-c5a388042364" ),
                    new CustomerCinema( 46, "1c850a33-6e0a-4c03-bb2d-c5a388042364" ),
                    new CustomerCinema( 15, "2a8f5f5c-e539-4868-837b-9a19852a904e" ),
                    new CustomerCinema( 16, "2a8f5f5c-e539-4868-837b-9a19852a904e" ),
                    new CustomerCinema( 19, "2a8f5f5c-e539-4868-837b-9a19852a904e" ),
                    new CustomerCinema( 24, "2a8f5f5c-e539-4868-837b-9a19852a904e" ),
                    new CustomerCinema( 29, "2a8f5f5c-e539-4868-837b-9a19852a904e" ),
                    new CustomerCinema( 30, "2a8f5f5c-e539-4868-837b-9a19852a904e" ),
                    new CustomerCinema( 45, "2a8f5f5c-e539-4868-837b-9a19852a904e" ),
                    new CustomerCinema( 49, "2a8f5f5c-e539-4868-837b-9a19852a904e" ),
                    new CustomerCinema( 4, "2f09c66b-0830-4fcc-8a0f-f29b0990c669" ),
                    new CustomerCinema( 10, "2f09c66b-0830-4fcc-8a0f-f29b0990c669" ),
                    new CustomerCinema( 13, "2f09c66b-0830-4fcc-8a0f-f29b0990c669" ),
                    new CustomerCinema( 22, "2f09c66b-0830-4fcc-8a0f-f29b0990c669" ),
                    new CustomerCinema( 28, "2f09c66b-0830-4fcc-8a0f-f29b0990c669" ),
                    new CustomerCinema( 36, "2f09c66b-0830-4fcc-8a0f-f29b0990c669" ),
                    new CustomerCinema( 47, "2f09c66b-0830-4fcc-8a0f-f29b0990c669" ),
                    new CustomerCinema( 49, "2f09c66b-0830-4fcc-8a0f-f29b0990c669" ),
                    new CustomerCinema( 6, "4634669c-c5ad-41e6-8b41-f1524c9654ad" ),
                    new CustomerCinema( 8, "4634669c-c5ad-41e6-8b41-f1524c9654ad" ),
                    new CustomerCinema( 9, "4634669c-c5ad-41e6-8b41-f1524c9654ad" ),
                    new CustomerCinema( 10, "4634669c-c5ad-41e6-8b41-f1524c9654ad" ),
                    new CustomerCinema( 22, "4634669c-c5ad-41e6-8b41-f1524c9654ad" ),
                    new CustomerCinema( 24, "4634669c-c5ad-41e6-8b41-f1524c9654ad" ),
                    new CustomerCinema( 25, "4634669c-c5ad-41e6-8b41-f1524c9654ad" ),
                    new CustomerCinema( 33, "4634669c-c5ad-41e6-8b41-f1524c9654ad" ),
                    new CustomerCinema( 7, "96256cfb-df20-4a1f-8898-f06f634a17d7" ),
                    new CustomerCinema( 12, "96256cfb-df20-4a1f-8898-f06f634a17d7" ),
                    new CustomerCinema( 14, "96256cfb-df20-4a1f-8898-f06f634a17d7" ),
                    new CustomerCinema( 16, "96256cfb-df20-4a1f-8898-f06f634a17d7" ),
                    new CustomerCinema( 20, "96256cfb-df20-4a1f-8898-f06f634a17d7" ),
                    new CustomerCinema( 23, "96256cfb-df20-4a1f-8898-f06f634a17d7" ),
                    new CustomerCinema( 28, "96256cfb-df20-4a1f-8898-f06f634a17d7" ),
                    new CustomerCinema( 46, "96256cfb-df20-4a1f-8898-f06f634a17d7" ),
                    new CustomerCinema( 47, "96256cfb-df20-4a1f-8898-f06f634a17d7" ),
                    new CustomerCinema( 1, "bfa19e5f-4529-4276-bde8-8e6d3de2c423" ),
                    new CustomerCinema( 3, "bfa19e5f-4529-4276-bde8-8e6d3de2c423" ),
                    new CustomerCinema( 7, "bfa19e5f-4529-4276-bde8-8e6d3de2c423" ),
                    new CustomerCinema( 15, "bfa19e5f-4529-4276-bde8-8e6d3de2c423" ),
                    new CustomerCinema( 20, "bfa19e5f-4529-4276-bde8-8e6d3de2c423" ),
                    new CustomerCinema( 24, "bfa19e5f-4529-4276-bde8-8e6d3de2c423" ),
                    new CustomerCinema( 36, "bfa19e5f-4529-4276-bde8-8e6d3de2c423" ),
                    new CustomerCinema( 39, "bfa19e5f-4529-4276-bde8-8e6d3de2c423" ),
                    new CustomerCinema( 45, "bfa19e5f-4529-4276-bde8-8e6d3de2c423" ),
                    new CustomerCinema( 5, "c21bf410-3e22-4720-b01a-f2d91191a222" ),
                    new CustomerCinema( 19, "c21bf410-3e22-4720-b01a-f2d91191a222" ),
                    new CustomerCinema( 26, "c21bf410-3e22-4720-b01a-f2d91191a222" ),
                    new CustomerCinema( 34, "c21bf410-3e22-4720-b01a-f2d91191a222" ),
                    new CustomerCinema( 37, "c21bf410-3e22-4720-b01a-f2d91191a222" ),
                    new CustomerCinema( 41, "c21bf410-3e22-4720-b01a-f2d91191a222" ),
                    new CustomerCinema( 45, "c21bf410-3e22-4720-b01a-f2d91191a222" ),
                    new CustomerCinema( 46, "c21bf410-3e22-4720-b01a-f2d91191a222" ),
                    new CustomerCinema( 1, "e7d88cb7-a424-4795-8965-17273642b773" ),
                    new CustomerCinema( 9, "e7d88cb7-a424-4795-8965-17273642b773" ),
                    new CustomerCinema( 19, "e7d88cb7-a424-4795-8965-17273642b773" ),
                    new CustomerCinema( 23, "e7d88cb7-a424-4795-8965-17273642b773" ),
                    new CustomerCinema( 25, "e7d88cb7-a424-4795-8965-17273642b773" ),
                    new CustomerCinema( 28, "e7d88cb7-a424-4795-8965-17273642b773" ),
                    new CustomerCinema( 37, "e7d88cb7-a424-4795-8965-17273642b773" ),
                    new CustomerCinema( 39, "e7d88cb7-a424-4795-8965-17273642b773" ),
                    new CustomerCinema( 6, "f338b628-feaf-4a03-95ad-defb7aec5c83" ),
                    new CustomerCinema( 8, "f338b628-feaf-4a03-95ad-defb7aec5c83" ),
                    new CustomerCinema( 15, "f338b628-feaf-4a03-95ad-defb7aec5c83" ),
                    new CustomerCinema( 17, "f338b628-feaf-4a03-95ad-defb7aec5c83" ),
                    new CustomerCinema( 24, "f338b628-feaf-4a03-95ad-defb7aec5c83" ),
                    new CustomerCinema( 34, "f338b628-feaf-4a03-95ad-defb7aec5c83" ),
                    new CustomerCinema( 38, "f338b628-feaf-4a03-95ad-defb7aec5c83" ),
                    new CustomerCinema( 42, "f338b628-feaf-4a03-95ad-defb7aec5c83" ),
                    new CustomerCinema( 44, "f338b628-feaf-4a03-95ad-defb7aec5c83" ),
                    new CustomerCinema( 45, "f338b628-feaf-4a03-95ad-defb7aec5c83" ),
                    new CustomerCinema( 48, "f338b628-feaf-4a03-95ad-defb7aec5c83" ),
                    new CustomerCinema( 49, "f338b628-feaf-4a03-95ad-defb7aec5c83" )
           };

            var customers = new List<ApplicationUser>()
            {
                 new ApplicationUser( "156fc675-02de-4250-9edb-869c85e13e61", 0, "8f06bfbc-e1e3-4f95-9bc1-30add0031c34", new DateTime(2023, 7, 29, 12, 59, 18, 234, DateTimeKind.Local).AddTicks(7086), "owner1@owner.com", true, "James", "Johnson", false, null, "OWNER1@OWNER.COM", "OWNER1@OWNER.COM", null, null, false, "profilePicURL", "1969a695-01c0-49be-8d42-482cb1c327bc", false, "OWNER1@OWNER.COM" ),
                    new ApplicationUser( "191d6e78-88fb-40ce-b85c-d8bcf4a1ae4c", 0, "1c85f3a1-2adc-4bc0-8e52-72f91f9c11e6", new DateTime(2023, 7, 29, 12, 59, 18, 234, DateTimeKind.Local).AddTicks(7401), "customer2@customer.com", true, "Owais", "Flynn", false, null, "CUSTOMER2@CUSTOMER.COM", "CUSTOMER2@CUSTOMER.COM", null, null, false, "profilePicURL", "23628fe3-34d3-436b-b12b-7386bda03b50", false, "CUSTOMER2@CUSTOMER.COM" ),
                    new ApplicationUser( "1c850a33-6e0a-4c03-bb2d-c5a388042364", 0, "7ee1ee23-0839-40da-908d-b87ef4d668df", new DateTime(2023, 7, 29, 12, 59, 18, 234, DateTimeKind.Local).AddTicks(7518), "customer6@customer.com", true, "Peggy", "Pope", false, null, "CUSTOMER6@CUSTOMER.COM", "CUSTOMER6@CUSTOMER.COM", null, null, false, "profilePicURL", "d3103320-f4b9-4654-ba99-654bc1cdf6c4", false, "CUSTOMER6@CUSTOMER.COM" ),
                    new ApplicationUser( "2055e8c8-5a8e-49c3-9f0a-a987700af2ee", 0, "a294bebb-1a63-4316-8f84-629edf1d64e0", new DateTime(2023, 7, 29, 12, 59, 18, 234, DateTimeKind.Local).AddTicks(7180), "owner2@owner.com", true, "Mary", "Lou", false, null, "OWNER2@OWNER.COM", "OWNER2@OWNER.COM", null, null, false, "profilePicURL", "0dcc8f9a-b2bd-40a5-8f68-ad4c06ccd772", false, "OWNER2@OWNER.COM" ),
                    new ApplicationUser( "218dcf68-aa10-4c63-994f-50853fb19296", 0, "76e4b52a-578d-47d2-8a5f-c0f9fc0b5c3e", new DateTime(2023, 7, 29, 12, 59, 18, 234, DateTimeKind.Local).AddTicks(7212), "owner3@owner.com", true, "Margarita", "Costner", false, null, "OWNER3@OWNER.COM", "OWNER3@OWNER.COM", null, null, false, "profilePicURL", "ea79ff42-c2cd-49bd-8d71-1cc1c1f27dab", false, "OWNER3@OWNER.COM" ),
                    new ApplicationUser( "2a8f5f5c-e539-4868-837b-9a19852a904e", 0, "5012f086-228e-4796-9b77-9f767892e80c", new DateTime(2023, 7, 29, 12, 59, 18, 234, DateTimeKind.Local).AddTicks(7385), "customer1@customer.com", true, "Barney", "Hobbs", false, null, "CUSTOMER1@CUSTOMER.COM", "CUSTOMER1@CUSTOMER.COM", null, null, false, "profilePicURL", "41af5f84-823a-4d6e-8dc3-6917d36c1983", false, "CUSTOMER1@CUSTOMER.COM" ),
                    new ApplicationUser( "2f09c66b-0830-4fcc-8a0f-f29b0990c669", 0, "159b0533-e321-4cca-a2de-243612f00487", new DateTime(2023, 7, 29, 12, 59, 18, 234, DateTimeKind.Local).AddTicks(7553), "customer8@customer.com", true, "Frazer", "Hensley", false, null, "CUSTOMER8@CUSTOMER.COM", "CUSTOMER8@CUSTOMER.COM", null, null, false, "profilePicURL", "11d3f4f3-b612-4683-90db-2a0c77867a06", false, "CUSTOMER8@CUSTOMER.COM" ),
                    new ApplicationUser( "4634669c-c5ad-41e6-8b41-f1524c9654ad", 0, "2485e1c3-2020-40bc-b989-f73593f7ed05", new DateTime(2023, 7, 29, 12, 59, 18, 234, DateTimeKind.Local).AddTicks(7419), "customer3@customer.com", true, "Kiran", "Delacruz", false, null, "CUSTOMER3@CUSTOMER.COM", "CUSTOMER3@CUSTOMER.COM", null, null, false, "profilePicURL", "18e0ad1c-2ce4-494c-8a08-4eb3e491aae1", false, "CUSTOMER3@CUSTOMER.COM" ),
                    new ApplicationUser( "5556c45e-395d-402b-b765-750666b092fc", 0, "b65be569-e85a-4a74-a7bc-425b7635069f", new DateTime(2023, 7, 29, 12, 59, 18, 234, DateTimeKind.Local).AddTicks(7264), "owner6@owner.com", true, "Javier", "Gonzalez", false, null, "OWNER6@OWNER.COM", "OWNER6@OWNER.COM", null, null, false, "profilePicURL", "35a42130-bf0d-4f5a-85a4-032f27fb34fe", false, "OWNER6@OWNER.COM" ),
                    new ApplicationUser( "60223fcf-5fa4-434f-a4ba-9389a4f571a0", 0, "4f65a024-e0d8-424b-95be-4c4966e0ed73", new DateTime(2023, 7, 29, 12, 59, 18, 234, DateTimeKind.Local).AddTicks(7299), "owner8@owner.com", true, "John", "Chang", false, null, "OWNER8@OWNER.COM", "OWNER8@OWNER.COM", null, null, false, "profilePicURL", "5caf143b-da62-492e-9c4c-3a631cdd77b9", false, "OWNER8@OWNER.COM" ),
                    new ApplicationUser( "610ab053-2c5a-451b-9634-03b59ea4a473", 0, "169c576b-71f5-4ef3-8fcb-3479eca00a65", new DateTime(2023, 7, 29, 12, 59, 18, 234, DateTimeKind.Local).AddTicks(7347), "owner9@owner.com", true, "Lily", "May", false, null, "OWNER9@OWNER.COM", "OWNER9@OWNER.COM", null, null, false, "profilePicURL", "cf814eb1-d2f0-47ac-8dbd-54482814dc77", false, "OWNER9@OWNER.COM" ),
                    new ApplicationUser( "64ca1994-bfd0-4d26-8ec4-4d1bc82bd95c", 0, "b26c078d-4fca-474d-9a24-a10a61a69c88", new DateTime(2023, 7, 29, 12, 59, 18, 234, DateTimeKind.Local).AddTicks(7283), "owner7@owner.com", true, "Marco", "Cruz", false, null, "OWNER7@OWNER.COM", "OWNER7@OWNER.COM", null, null, false, "profilePicURL", "2f09438a-adca-4bed-8a26-4ac7ae834dd3", false, "OWNER7@OWNER.COM" ),
                    new ApplicationUser( "8980e4ca-2628-490d-840a-9c9414ab9f33", 0, "d135f42d-5dae-407f-a9d4-c4ec642f61cc", new DateTime(2023, 7, 29, 12, 59, 18, 234, DateTimeKind.Local).AddTicks(7364), "owner10@owner.com", true, "Jack", "Marston", false, null, "OWNER10@OWNER.COM", "OWNER10@OWNER.COM", null, null, false, "profilePicURL", "69434a7f-f6f5-4f2f-9aec-4594d2e1bd27", false, "OWNER10@OWNER.COM" ),
                    new ApplicationUser( "96256cfb-df20-4a1f-8898-f06f634a17d7", 0, "4effdcbd-6484-43ad-bab0-83c882123a07", new DateTime(2023, 7, 29, 12, 59, 18, 234, DateTimeKind.Local).AddTicks(7587), "customer10@customer.com", true, "Bryony", "Becker", false, null, "CUSTOMER10@CUSTOMER.COM", "CUSTOMER10@CUSTOMER.COM", null, null, false, "profilePicURL", "84717c40-eafd-40e7-b0a1-96e7e0937a09", false, "CUSTOMER10@CUSTOMER.COM" ),
                    new ApplicationUser( "a9b6dc74-38a5-4794-a703-59204f461adb", 0, "a0a0780d-c13c-4218-8b33-03da54eec896", new DateTime(2023, 7, 29, 12, 59, 18, 234, DateTimeKind.Local).AddTicks(7228), "owner4@owner.com", true, "Gabriel", "Peric", false, null, "OWNER4@OWNER.COM", "OWNER4@OWNER.COM", null, null, false, "profilePicURL", "d795d540-219a-4312-bbae-4d3b0558c2d5", false, "OWNER4@OWNER.COM" ),
                    new ApplicationUser( "bfa19e5f-4529-4276-bde8-8e6d3de2c423", 0, "13ea44e7-8dae-4bb7-976e-64a952c9004c", new DateTime(2023, 7, 29, 12, 59, 18, 234, DateTimeKind.Local).AddTicks(7435), "customer4@customer.com", true, "Ava-Rose", "Chapman", false, null, "CUSTOMER4@CUSTOMER.COM", "CUSTOMER4@CUSTOMER.COM", null, null, false, "profilePicURL", "eede314b-fa5b-4b39-927c-59c9959e9990", false, "CUSTOMER4@CUSTOMER.COM" ),
                    new ApplicationUser( "c21bf410-3e22-4720-b01a-f2d91191a222", 0, "87fe1bfb-3589-4d5e-a44e-c8adcb6d5214", new DateTime(2023, 7, 29, 12, 59, 18, 234, DateTimeKind.Local).AddTicks(7453), "customer5@customer.com", true, "Sienna", "Barnett", false, null, "CUSTOMER5@CUSTOMER.COM", "CUSTOMER5@CUSTOMER.COM", null, null, false, "profilePicURL", "0f9d29a6-6105-4649-a166-0762aee21984", false, "CUSTOMER5@CUSTOMER.COM" ),
                    new ApplicationUser( "e7d88cb7-a424-4795-8965-17273642b773", 0, "8e7b2c00-3070-485c-8e23-06551a9cff1f", new DateTime(2023, 7, 29, 12, 59, 18, 234, DateTimeKind.Local).AddTicks(7571), "customer9@customer.com", true, "Sadia", "Francis", false, null, "CUSTOMER9@CUSTOMER.COM", "CUSTOMER9@CUSTOMER.COM", null, null, false, "profilePicURL", "7ca95f02-1224-4ae7-a823-0e9243192c8b", false, "CUSTOMER9@CUSTOMER.COM" ),
                    new ApplicationUser( "ea811464-0c63-4c45-ac26-d4eb5bff334f", 0, "9bfe5e83-4173-4624-ba5d-f7cd07563c4f", new DateTime(2023, 7, 29, 12, 59, 18, 234, DateTimeKind.Local).AddTicks(7246), "owner5@owner.com", true, "Percy", "Jackson", false, null, "OWNER5@OWNER.COM", "OWNER5@OWNER.COM", null, null, false, "profilePicURL", "4b0d4e53-c70f-4118-8c78-b63d6adb1548", false, "OWNER5@OWNER.COM" ),
                    new ApplicationUser( "f338b628-feaf-4a03-95ad-defb7aec5c83", 0, "096f10f9-3070-45c1-824d-1e96bc810fdc", new DateTime(2023, 7, 29, 12, 59, 18, 234, DateTimeKind.Local).AddTicks(7537), "customer7@customer.com", true, "Madiha", "Brock", false, null, "CUSTOMER7@CUSTOMER.COM", "CUSTOMER7@CUSTOMER.COM", null, null, false, "profilePicURL", "d6172bcd-0893-437a-b705-43122add4781", false, "CUSTOMER7@CUSTOMER.COM" )
            }.Where(i => i.Email.StartsWith("customer")).ToList();
            for (int i = 0; i < 100; i++)
            {
                int cinemaMovieIndex = new Random().Next(cinemasMovies.Count);
                int customerCinemaIndex = new Random().Next(customersCinemas.Count);

                var cinemaMovie = cinemasMovies[cinemaMovieIndex];
                var customerCinema = customersCinemas[customerCinemaIndex];
                if (customerCinema.CinemaId == cinemaMovie.CinemaId)
                {
                    if (!ratings.Any(i => i.CustomerId == customerCinema.CustomerId && i.MovieId == cinemaMovie.MovieId))
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
            return ratings;
        }
    }
}
