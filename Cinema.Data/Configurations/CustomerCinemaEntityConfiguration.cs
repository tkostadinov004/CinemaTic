using Cinema.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace Cinema.Data.Configurations
{
    public class CustomerCinemaEntityConfiguration : IEntityTypeConfiguration<CustomerCinema>
    {
        public void Configure(EntityTypeBuilder<CustomerCinema> builder)
        {
            builder.HasData(this.GetCustomerCinemas());
        }
        private IEnumerable<CustomerCinema> GetCustomerCinemas()
        {
           var customerCinemas = new List<CustomerCinema>()
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
            return customerCinemas;
        }
    }
}
