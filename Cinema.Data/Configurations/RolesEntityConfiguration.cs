using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Data.Configurations
{
    public class RolesEntityConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(GetRoles());
        }
        private IEnumerable<IdentityRole> GetRoles()
        {
            var roles = new List<IdentityRole>()
            {
                new IdentityRole
                {
                    Id = "4f2e306d-f18e-407f-add5-b06709864898",
                    Name = "Owner",
                    NormalizedName = "OWNER",
                    ConcurrencyStamp = "986d6ab3-c3ef-4c63-9f83-c5e9542da90d"
                }, 
                new IdentityRole
                {
                    Id = "ddcf49d7-098f-4dfb-a859-01efa93f73be",
                    Name = "Customer",
                    NormalizedName = "CUSTOMER",
                    ConcurrencyStamp = "b1c2e206-4e9c-4b8d-9342-00c75ef366eb"
                },
                new IdentityRole
                {
                    Id = "f3c309d2-8913-4392-bef9-04e264ed443a",
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR",
                    ConcurrencyStamp = "f821d92f-1d22-41cb-9e38-3fed772c8834"
                }
            };
            return roles;
        }
    }
}
