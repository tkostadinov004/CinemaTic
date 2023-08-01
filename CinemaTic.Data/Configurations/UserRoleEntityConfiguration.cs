using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTic.Data.Configurations
{
    public class UserRoleEntityConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(GetUserRoles());
        }
        private IEnumerable<IdentityUserRole<string>> GetUserRoles()
        {
            var userRoles = new List<IdentityUserRole<string>>()
            {
                new IdentityUserRole<string>{UserId = "156fc675-02de-4250-9edb-869c85e13e61", RoleId = "4f2e306d-f18e-407f-add5-b06709864898" },
                new IdentityUserRole<string>{UserId = "2055e8c8-5a8e-49c3-9f0a-a987700af2ee", RoleId = "4f2e306d-f18e-407f-add5-b06709864898" },
                new IdentityUserRole<string>{UserId = "218dcf68-aa10-4c63-994f-50853fb19296", RoleId = "4f2e306d-f18e-407f-add5-b06709864898" },
                new IdentityUserRole<string>{UserId = "5556c45e-395d-402b-b765-750666b092fc", RoleId = "4f2e306d-f18e-407f-add5-b06709864898" },
                new IdentityUserRole<string>{UserId = "60223fcf-5fa4-434f-a4ba-9389a4f571a0", RoleId = "4f2e306d-f18e-407f-add5-b06709864898" },
                new IdentityUserRole<string>{UserId = "610ab053-2c5a-451b-9634-03b59ea4a473", RoleId = "4f2e306d-f18e-407f-add5-b06709864898" },
                new IdentityUserRole<string>{UserId = "64ca1994-bfd0-4d26-8ec4-4d1bc82bd95c", RoleId = "4f2e306d-f18e-407f-add5-b06709864898" },
                new IdentityUserRole<string>{UserId = "8980e4ca-2628-490d-840a-9c9414ab9f33", RoleId = "4f2e306d-f18e-407f-add5-b06709864898" },
                new IdentityUserRole<string>{UserId = "a9b6dc74-38a5-4794-a703-59204f461adb", RoleId = "4f2e306d-f18e-407f-add5-b06709864898" },
                new IdentityUserRole<string>{UserId = "ea811464-0c63-4c45-ac26-d4eb5bff334f", RoleId = "4f2e306d-f18e-407f-add5-b06709864898" },
                new IdentityUserRole<string>{UserId = "191d6e78-88fb-40ce-b85c-d8bcf4a1ae4c", RoleId = "ddcf49d7-098f-4dfb-a859-01efa93f73be" },
                new IdentityUserRole<string>{UserId = "1c850a33-6e0a-4c03-bb2d-c5a388042364", RoleId = "ddcf49d7-098f-4dfb-a859-01efa93f73be" },
                new IdentityUserRole<string>{UserId = "2a8f5f5c-e539-4868-837b-9a19852a904e", RoleId = "ddcf49d7-098f-4dfb-a859-01efa93f73be" },
                new IdentityUserRole<string>{UserId = "2f09c66b-0830-4fcc-8a0f-f29b0990c669", RoleId = "ddcf49d7-098f-4dfb-a859-01efa93f73be" },
                new IdentityUserRole<string>{UserId = "4634669c-c5ad-41e6-8b41-f1524c9654ad", RoleId = "ddcf49d7-098f-4dfb-a859-01efa93f73be" },
                new IdentityUserRole<string>{UserId = "96256cfb-df20-4a1f-8898-f06f634a17d7", RoleId = "ddcf49d7-098f-4dfb-a859-01efa93f73be" },
                new IdentityUserRole<string>{UserId = "bfa19e5f-4529-4276-bde8-8e6d3de2c423", RoleId = "ddcf49d7-098f-4dfb-a859-01efa93f73be" },
                new IdentityUserRole<string>{UserId = "c21bf410-3e22-4720-b01a-f2d91191a222", RoleId = "ddcf49d7-098f-4dfb-a859-01efa93f73be" },
                new IdentityUserRole<string>{UserId = "e7d88cb7-a424-4795-8965-17273642b773", RoleId = "ddcf49d7-098f-4dfb-a859-01efa93f73be" },
                new IdentityUserRole<string>{UserId = "f338b628-feaf-4a03-95ad-defb7aec5c83", RoleId = "ddcf49d7-098f-4dfb-a859-01efa93f73be" },
                new IdentityUserRole<string>{UserId = "c0102fde-8991-4e2a-bfca-cdda51f13a66", RoleId = "f3c309d2-8913-4392-bef9-04e264ed443a" },
            };
            return userRoles;
        }
    }
}
