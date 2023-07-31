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
            var ratings = new List<UserMovie>()
            {
                new UserMovie("191d6e78-88fb-40ce-b85c-d8bcf4a1ae4c", 86, 2m ),
                    new UserMovie("191d6e78-88fb-40ce-b85c-d8bcf4a1ae4c", 100, 10m ),
                    new UserMovie( "2a8f5f5c-e539-4868-837b-9a19852a904e", 43, 2m ),
                    new UserMovie( "4634669c-c5ad-41e6-8b41-f1524c9654ad", 76, 10m )
            };
            return ratings;
        }
    }
}
