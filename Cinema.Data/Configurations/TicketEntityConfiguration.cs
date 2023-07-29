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
    public class TicketEntityConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.HasData(this.GetTickets());
        }
        private IEnumerable<Ticket> GetTickets()
        {
            var tickets = new List<Ticket>()
            {
                 new Ticket( 1, 30, "1c850a33-6e0a-4c03-bb2d-c5a388042364", new DateTime(2023, 8, 19, 17, 45, 0, 0, DateTimeKind.Unspecified), 71, 20m, 190, "R51C24" ),
                    new Ticket( 2, 11, "c21bf410-3e22-4720-b01a-f2d91191a222", new DateTime(2023, 8, 15, 9, 15, 0, 0, DateTimeKind.Unspecified), 22, 15m, 167, "R32C32" ),
                    new Ticket( 3, 24, "c21bf410-3e22-4720-b01a-f2d91191a222", new DateTime(2023, 8, 24, 23, 0, 0, 0, DateTimeKind.Unspecified), 76, 7m, 263, "R10C16" ),
                    new Ticket( 4, 46, "1c850a33-6e0a-4c03-bb2d-c5a388042364", new DateTime(2023, 8, 16, 12, 30, 0, 0, DateTimeKind.Unspecified), 15, 12m, 437, "R31C31" ),
                    new Ticket( 5, 42, "96256cfb-df20-4a1f-8898-f06f634a17d7", new DateTime(2023, 8, 17, 10, 40, 0, 0, DateTimeKind.Unspecified), 55, 10m, 79, "R54C16" ),
                    new Ticket( 6, 24, "2f09c66b-0830-4fcc-8a0f-f29b0990c669", new DateTime(2023, 8, 21, 19, 45, 0, 0, DateTimeKind.Unspecified), 76, 7m, 143, "R6C30" ),
                    new Ticket( 7, 3, "e7d88cb7-a424-4795-8965-17273642b773", new DateTime(2023, 8, 9, 21, 0, 0, 0, DateTimeKind.Unspecified), 30, 5m, 306, "R20C13" ),
                    new Ticket( 8, 42, "1c850a33-6e0a-4c03-bb2d-c5a388042364", new DateTime(2023, 8, 17, 14, 20, 0, 0, DateTimeKind.Unspecified), 55, 10m, 62, "R49C17" ),
                    new Ticket( 9, 37, "bfa19e5f-4529-4276-bde8-8e6d3de2c423", new DateTime(2023, 8, 28, 10, 20, 0, 0, DateTimeKind.Unspecified), 68, 7m, 37, "R25C47" ),
                    new Ticket( 10, 13, "96256cfb-df20-4a1f-8898-f06f634a17d7", new DateTime(2023, 8, 3, 14, 15, 0, 0, DateTimeKind.Unspecified), 12, 8m, 396, "R11C35" ),
                    new Ticket( 11, 45, "2f09c66b-0830-4fcc-8a0f-f29b0990c669", new DateTime(2023, 8, 25, 23, 10, 0, 0, DateTimeKind.Unspecified), 81, 20m, 189, "R53C9" ),
                    new Ticket( 12, 22, "2f09c66b-0830-4fcc-8a0f-f29b0990c669", new DateTime(2023, 8, 7, 10, 40, 0, 0, DateTimeKind.Unspecified), 48, 20m, 201, "R46C23" ),
                    new Ticket( 13, 25, "2f09c66b-0830-4fcc-8a0f-f29b0990c669", new DateTime(2023, 8, 18, 10, 15, 0, 0, DateTimeKind.Unspecified), 92, 21m, 131, "R47C6" ),
                    new Ticket( 14, 46, "96256cfb-df20-4a1f-8898-f06f634a17d7", new DateTime(2023, 8, 16, 12, 30, 0, 0, DateTimeKind.Unspecified), 15, 12m, 10, "R2C20" ),
                    new Ticket( 15, 26, "c21bf410-3e22-4720-b01a-f2d91191a222", new DateTime(2023, 8, 13, 22, 30, 0, 0, DateTimeKind.Unspecified), 58, 13m, 264, "R14C33" ),
                    new Ticket( 16, 42, "2a8f5f5c-e539-4868-837b-9a19852a904e", new DateTime(2023, 8, 3, 18, 45, 0, 0, DateTimeKind.Unspecified), 21, 15m, 296, "R11C13" ),
                    new Ticket( 17, 12, "c21bf410-3e22-4720-b01a-f2d91191a222", new DateTime(2023, 8, 28, 20, 20, 0, 0, DateTimeKind.Unspecified), 44, 13m, 274, "R29C25" ),
                     new Ticket( 18, 24, "1c850a33-6e0a-4c03-bb2d-c5a388042364", new DateTime(2023, 8, 14, 22, 40, 0, 0, DateTimeKind.Unspecified), 88, 5m, 319, "R40C23" ),
                    new Ticket( 19, 47, "1c850a33-6e0a-4c03-bb2d-c5a388042364", new DateTime(2023, 8, 21, 23, 50, 0, 0, DateTimeKind.Unspecified), 41, 12m, 178, "R17C47" ),
                    new Ticket( 20, 48, "2a8f5f5c-e539-4868-837b-9a19852a904e", new DateTime(2023, 8, 21, 11, 10, 0, 0, DateTimeKind.Unspecified), 51, 11m, 146, "R21C27" ),
                    new Ticket( 21, 46, "191d6e78-88fb-40ce-b85c-d8bcf4a1ae4c", new DateTime(2023, 8, 16, 12, 30, 0, 0, DateTimeKind.Unspecified), 15, 12m, 205, "R3C40" ),
                    new Ticket( 22, 36, "4634669c-c5ad-41e6-8b41-f1524c9654ad", new DateTime(2023, 8, 18, 14, 0, 0, 0, DateTimeKind.Unspecified), 2, 18m, 428, "R14C16" ),
                    new Ticket( 23, 4, "1c850a33-6e0a-4c03-bb2d-c5a388042364", new DateTime(2023, 7, 31, 15, 10, 0, 0, DateTimeKind.Unspecified), 67, 10m, 122, "R37C30" ),
                    new Ticket( 24, 37, "4634669c-c5ad-41e6-8b41-f1524c9654ad", new DateTime(2023, 8, 25, 12, 50, 0, 0, DateTimeKind.Unspecified), 64, 13m, 222, "R8C22" ),
                    new Ticket( 25, 38, "4634669c-c5ad-41e6-8b41-f1524c9654ad", new DateTime(2023, 8, 12, 10, 40, 0, 0, DateTimeKind.Unspecified), 7, 9m, 349, "R40C5" ),
                    new Ticket( 26, 38, "bfa19e5f-4529-4276-bde8-8e6d3de2c423", new DateTime(2023, 8, 17, 15, 10, 0, 0, DateTimeKind.Unspecified), 36, 21m, 445, "R54C4" ),
                    new Ticket( 27, 6, "2a8f5f5c-e539-4868-837b-9a19852a904e", new DateTime(2023, 8, 20, 15, 10, 0, 0, DateTimeKind.Unspecified), 96, 25m, 356, "R15C58" ),
                    new Ticket( 28, 38, "1c850a33-6e0a-4c03-bb2d-c5a388042364", new DateTime(2023, 8, 12, 10, 40, 0, 0, DateTimeKind.Unspecified), 7, 9m, 122, "R43C24" ),
                    new Ticket( 29, 49, "191d6e78-88fb-40ce-b85c-d8bcf4a1ae4c", new DateTime(2023, 8, 11, 10, 20, 0, 0, DateTimeKind.Unspecified), 48, 10m, 352, "R50C20" ),
                    new Ticket( 30, 38, "e7d88cb7-a424-4795-8965-17273642b773", new DateTime(2023, 8, 11, 8, 40, 0, 0, DateTimeKind.Unspecified), 93, 22m, 149, "R35C22" ),
                    new Ticket( 31, 37, "1c850a33-6e0a-4c03-bb2d-c5a388042364", new DateTime(2023, 8, 28, 10, 20, 0, 0, DateTimeKind.Unspecified), 68, 7m, 327, "R44C13" ),
                    new Ticket( 32, 37, "191d6e78-88fb-40ce-b85c-d8bcf4a1ae4c", new DateTime(2023, 8, 25, 20, 45, 0, 0, DateTimeKind.Unspecified), 64, 13m, 269, "R37C27" ),
                    new Ticket( 33, 39, "c21bf410-3e22-4720-b01a-f2d91191a222", new DateTime(2023, 8, 19, 20, 30, 0, 0, DateTimeKind.Unspecified), 90, 17m, 216, "R28C16" ),
                    new Ticket( 34, 32, "96256cfb-df20-4a1f-8898-f06f634a17d7", new DateTime(2023, 8, 10, 22, 50, 0, 0, DateTimeKind.Unspecified), 62, 11m, 124, "R52C16" ),
                    new Ticket( 35, 31, "2f09c66b-0830-4fcc-8a0f-f29b0990c669", new DateTime(2023, 8, 3, 10, 50, 0, 0, DateTimeKind.Unspecified), 6, 13m, 308, "R35C4" ),
                    new Ticket( 36, 32, "4634669c-c5ad-41e6-8b41-f1524c9654ad", new DateTime(2023, 8, 10, 22, 50, 0, 0, DateTimeKind.Unspecified), 62, 11m, 400, "R28C36" ),
                    new Ticket( 37, 37, "2a8f5f5c-e539-4868-837b-9a19852a904e", new DateTime(2023, 8, 28, 10, 20, 0, 0, DateTimeKind.Unspecified), 68, 7m, 155, "R19C12" ),
                    new Ticket( 38, 25, "f338b628-feaf-4a03-95ad-defb7aec5c83", new DateTime(2023, 8, 18, 10, 15, 0, 0, DateTimeKind.Unspecified), 92, 21m, 204, "R9C29" ),
                    new Ticket( 39, 39, "4634669c-c5ad-41e6-8b41-f1524c9654ad", new DateTime(2023, 7, 29, 19, 15, 0, 0, DateTimeKind.Unspecified), 96, 20m, 298, "R22C13" ),
                    new Ticket( 40, 32, "f338b628-feaf-4a03-95ad-defb7aec5c83", new DateTime(2023, 8, 11, 18, 45, 0, 0, DateTimeKind.Unspecified), 12, 12m, 23, "R21C32" ),
                    new Ticket( 41, 43, "1c850a33-6e0a-4c03-bb2d-c5a388042364", new DateTime(2023, 8, 13, 20, 45, 0, 0, DateTimeKind.Unspecified), 86, 21m, 227, "R27C39" ),
                    new Ticket( 42, 9, "c21bf410-3e22-4720-b01a-f2d91191a222", new DateTime(2023, 8, 18, 19, 30, 0, 0, DateTimeKind.Unspecified), 50, 7m, 218, "R35C7" ),
                    new Ticket( 43, 25, "2a8f5f5c-e539-4868-837b-9a19852a904e", new DateTime(2023, 8, 18, 10, 15, 0, 0, DateTimeKind.Unspecified), 92, 21m, 273, "R30C7" ),
                    new Ticket( 44, 12, "2a8f5f5c-e539-4868-837b-9a19852a904e", new DateTime(2023, 8, 27, 21, 45, 0, 0, DateTimeKind.Unspecified), 44, 13m, 192, "R10C23" ),
                    new Ticket( 45, 20, "2f09c66b-0830-4fcc-8a0f-f29b0990c669", new DateTime(2023, 8, 8, 17, 45, 0, 0, DateTimeKind.Unspecified), 82, 23m, 385, "R48C27" ),
                    new Ticket( 46, 31, "1c850a33-6e0a-4c03-bb2d-c5a388042364", new DateTime(2023, 8, 18, 20, 20, 0, 0, DateTimeKind.Unspecified), 53, 8m, 27, "R59C9" ),
                    new Ticket( 47, 32, "96256cfb-df20-4a1f-8898-f06f634a17d7", new DateTime(2023, 8, 11, 18, 45, 0, 0, DateTimeKind.Unspecified), 12, 12m, 236, "R46C17" ),
                    new Ticket( 48, 3, "e7d88cb7-a424-4795-8965-17273642b773", new DateTime(2023, 8, 11, 18, 20, 0, 0, DateTimeKind.Unspecified), 30, 5m, 356, "R13C60" ),
                    new Ticket( 49, 36, "4634669c-c5ad-41e6-8b41-f1524c9654ad", new DateTime(2023, 8, 24, 22, 10, 0, 0, DateTimeKind.Unspecified), 52, 11m, 119, "R17C11" ),
                    new Ticket( 50, 42, "f338b628-feaf-4a03-95ad-defb7aec5c83", new DateTime(2023, 8, 3, 18, 45, 0, 0, DateTimeKind.Unspecified), 21, 15m, 439, "R15C11" ),
                    new Ticket( 51, 41, "2a8f5f5c-e539-4868-837b-9a19852a904e", new DateTime(2023, 8, 24, 15, 50, 0, 0, DateTimeKind.Unspecified), 85, 25m, 229, "R15C7" ),
                    new Ticket( 52, 35, "1c850a33-6e0a-4c03-bb2d-c5a388042364", new DateTime(2023, 8, 18, 23, 50, 0, 0, DateTimeKind.Unspecified), 63, 6m, 348, "R26C21" ),
                    new Ticket( 53, 31, "bfa19e5f-4529-4276-bde8-8e6d3de2c423", new DateTime(2023, 8, 3, 10, 50, 0, 0, DateTimeKind.Unspecified), 6, 13m, 347, "R26C1" ),
                    new Ticket( 54, 37, "2f09c66b-0830-4fcc-8a0f-f29b0990c669", new DateTime(2023, 8, 27, 18, 30, 0, 0, DateTimeKind.Unspecified), 7, 24m, 215, "R22C13" ),
                    new Ticket( 55, 26, "c21bf410-3e22-4720-b01a-f2d91191a222", new DateTime(2023, 8, 13, 22, 30, 0, 0, DateTimeKind.Unspecified), 58, 13m, 103, "R28C15" ),
                    new Ticket( 56, 11, "e7d88cb7-a424-4795-8965-17273642b773", new DateTime(2023, 8, 15, 16, 0, 0, 0, DateTimeKind.Unspecified), 41, 6m, 232, "R18C20" ),
                    new Ticket( 57, 15, "2a8f5f5c-e539-4868-837b-9a19852a904e", new DateTime(2023, 8, 26, 10, 10, 0, 0, DateTimeKind.Unspecified), 72, 16m, 55, "R14C5" ),
                    new Ticket( 58, 45, "f338b628-feaf-4a03-95ad-defb7aec5c83", new DateTime(2023, 8, 25, 23, 10, 0, 0, DateTimeKind.Unspecified), 81, 20m, 378, "R12C14" ),
                    new Ticket( 59, 47, "c21bf410-3e22-4720-b01a-f2d91191a222", new DateTime(2023, 8, 21, 8, 30, 0, 0, DateTimeKind.Unspecified), 41, 12m, 334, "R6C18" ),
                    new Ticket( 60, 36, "2a8f5f5c-e539-4868-837b-9a19852a904e", new DateTime(2023, 8, 18, 14, 0, 0, 0, DateTimeKind.Unspecified), 2, 18m, 325, "R23C19" ),
                    new Ticket( 61, 22, "2f09c66b-0830-4fcc-8a0f-f29b0990c669", new DateTime(2023, 8, 27, 21, 30, 0, 0, DateTimeKind.Unspecified), 29, 21m, 344, "R35C47" ),
                    new Ticket( 62, 48, "2a8f5f5c-e539-4868-837b-9a19852a904e", new DateTime(2023, 8, 21, 11, 10, 0, 0, DateTimeKind.Unspecified), 51, 11m, 399, "R28C20" ),
                    new Ticket( 63, 11, "96256cfb-df20-4a1f-8898-f06f634a17d7", new DateTime(2023, 8, 27, 10, 0, 0, 0, DateTimeKind.Unspecified), 38, 16m, 353, "R11C13" ),
                    new Ticket( 64, 46, "bfa19e5f-4529-4276-bde8-8e6d3de2c423", new DateTime(2023, 8, 16, 12, 30, 0, 0, DateTimeKind.Unspecified), 15, 12m, 135, "R5C33" ),
                    new Ticket( 65, 49, "2f09c66b-0830-4fcc-8a0f-f29b0990c669", new DateTime(2023, 8, 7, 20, 20, 0, 0, DateTimeKind.Unspecified), 53, 12m, 34, "R18C1" ),
                    new Ticket( 66, 22, "191d6e78-88fb-40ce-b85c-d8bcf4a1ae4c", new DateTime(2023, 8, 7, 10, 40, 0, 0, DateTimeKind.Unspecified), 48, 20m, 161, "R2C35" ),
                    new Ticket( 67, 30, "4634669c-c5ad-41e6-8b41-f1524c9654ad", new DateTime(2023, 8, 23, 15, 15, 0, 0, DateTimeKind.Unspecified), 26, 19m, 201, "R47C29" ),
                    new Ticket( 68, 42, "1c850a33-6e0a-4c03-bb2d-c5a388042364", new DateTime(2023, 8, 17, 14, 20, 0, 0, DateTimeKind.Unspecified), 55, 10m, 12, "R20C11" ),
                    new Ticket( 69, 9, "4634669c-c5ad-41e6-8b41-f1524c9654ad", new DateTime(2023, 8, 28, 17, 50, 0, 0, DateTimeKind.Unspecified), 86, 14m, 234, "R32C26" ),
                    new Ticket( 70, 38, "bfa19e5f-4529-4276-bde8-8e6d3de2c423", new DateTime(2023, 8, 2, 13, 15, 0, 0, DateTimeKind.Unspecified), 67, 12m, 348, "R27C29" ),
                    new Ticket( 71, 49, "2a8f5f5c-e539-4868-837b-9a19852a904e", new DateTime(2023, 8, 1, 18, 50, 0, 0, DateTimeKind.Unspecified), 43, 21m, 64, "R10C30" ),
                    new Ticket( 72, 36, "1c850a33-6e0a-4c03-bb2d-c5a388042364", new DateTime(2023, 8, 18, 14, 0, 0, 0, DateTimeKind.Unspecified), 2, 18m, 439, "R12C14" ),
                    new Ticket( 73, 34, "191d6e78-88fb-40ce-b85c-d8bcf4a1ae4c", new DateTime(2023, 8, 15, 22, 10, 0, 0, DateTimeKind.Unspecified), 24, 21m, 174, "R13C47" ),
                    new Ticket( 74, 32, "96256cfb-df20-4a1f-8898-f06f634a17d7", new DateTime(2023, 8, 10, 22, 50, 0, 0, DateTimeKind.Unspecified), 62, 11m, 52, "R35C25" ),
                    new Ticket( 75, 48, "4634669c-c5ad-41e6-8b41-f1524c9654ad", new DateTime(2023, 8, 21, 11, 10, 0, 0, DateTimeKind.Unspecified), 51, 11m, 343, "R31C32" ),
                    new Ticket( 76, 5, "191d6e78-88fb-40ce-b85c-d8bcf4a1ae4c", new DateTime(2023, 8, 6, 21, 0, 0, 0, DateTimeKind.Unspecified), 67, 25m, 24, "R34C2" ),
                    new Ticket( 77, 41, "2a8f5f5c-e539-4868-837b-9a19852a904e", new DateTime(2023, 8, 25, 17, 40, 0, 0, DateTimeKind.Unspecified), 85, 25m, 368, "R56C22" ),
                    new Ticket( 78, 3, "4634669c-c5ad-41e6-8b41-f1524c9654ad", new DateTime(2023, 8, 14, 21, 20, 0, 0, DateTimeKind.Unspecified), 52, 20m, 394, "R14C8" ),
                    new Ticket( 79, 40, "1c850a33-6e0a-4c03-bb2d-c5a388042364", new DateTime(2023, 8, 18, 18, 45, 0, 0, DateTimeKind.Unspecified), 22, 20m, 383, "R36C29" ),
                    new Ticket( 80, 35, "2a8f5f5c-e539-4868-837b-9a19852a904e", new DateTime(2023, 8, 11, 19, 15, 0, 0, DateTimeKind.Unspecified), 42, 8m, 230, "R15C28" ),
                    new Ticket( 81, 3, "191d6e78-88fb-40ce-b85c-d8bcf4a1ae4c", new DateTime(2023, 8, 5, 10, 30, 0, 0, DateTimeKind.Unspecified), 30, 5m, 295, "R31C33" ),
                    new Ticket( 82, 49, "2f09c66b-0830-4fcc-8a0f-f29b0990c669", new DateTime(2023, 8, 9, 10, 15, 0, 0, DateTimeKind.Unspecified), 48, 10m, 233, "R32C14" ),
                    new Ticket( 83, 24, "e7d88cb7-a424-4795-8965-17273642b773", new DateTime(2023, 8, 18, 18, 40, 0, 0, DateTimeKind.Unspecified), 88, 5m, 323, "R15C32" ),
                    new Ticket( 84, 37, "191d6e78-88fb-40ce-b85c-d8bcf4a1ae4c", new DateTime(2023, 8, 27, 18, 30, 0, 0, DateTimeKind.Unspecified), 7, 24m, 113, "R36C6" ),
                    new Ticket( 85, 49, "96256cfb-df20-4a1f-8898-f06f634a17d7", new DateTime(2023, 8, 8, 16, 40, 0, 0, DateTimeKind.Unspecified), 53, 12m, 376, "R34C29" ),
                    new Ticket( 86, 20, "2a8f5f5c-e539-4868-837b-9a19852a904e", new DateTime(2023, 8, 9, 21, 40, 0, 0, DateTimeKind.Unspecified), 9, 17m, 230, "R4C28" ),
                    new Ticket( 87, 9, "2f09c66b-0830-4fcc-8a0f-f29b0990c669", new DateTime(2023, 8, 28, 13, 40, 0, 0, DateTimeKind.Unspecified), 86, 14m, 169, "R46C27" ),
                    new Ticket( 88, 46, "2f09c66b-0830-4fcc-8a0f-f29b0990c669", new DateTime(2023, 8, 17, 15, 50, 0, 0, DateTimeKind.Unspecified), 86, 24m, 393, "R53C19" ),
                    new Ticket( 89, 47, "191d6e78-88fb-40ce-b85c-d8bcf4a1ae4c", new DateTime(2023, 8, 13, 21, 50, 0, 0, DateTimeKind.Unspecified), 49, 16m, 185, "R26C9" ),
                    new Ticket( 90, 40, "4634669c-c5ad-41e6-8b41-f1524c9654ad", new DateTime(2023, 8, 18, 18, 45, 0, 0, DateTimeKind.Unspecified), 22, 20m, 299, "R25C18" ),
                    new Ticket( 91, 45, "c21bf410-3e22-4720-b01a-f2d91191a222", new DateTime(2023, 8, 25, 23, 10, 0, 0, DateTimeKind.Unspecified), 81, 20m, 138, "R25C35" ),
                    new Ticket( 92, 49, "e7d88cb7-a424-4795-8965-17273642b773", new DateTime(2023, 8, 11, 10, 20, 0, 0, DateTimeKind.Unspecified), 48, 10m, 404, "R35C42" ),
                    new Ticket( 93, 15, "1c850a33-6e0a-4c03-bb2d-c5a388042364", new DateTime(2023, 8, 26, 10, 10, 0, 0, DateTimeKind.Unspecified), 72, 16m, 99, "R26C47" ),
                    new Ticket( 94, 31, "f338b628-feaf-4a03-95ad-defb7aec5c83", new DateTime(2023, 8, 27, 16, 30, 0, 0, DateTimeKind.Unspecified), 49, 11m, 267, "R17C32" ),
                    new Ticket( 95, 11, "191d6e78-88fb-40ce-b85c-d8bcf4a1ae4c", new DateTime(2023, 8, 18, 11, 30, 0, 0, DateTimeKind.Unspecified), 38, 16m, 368, "R56C24" ),
                    new Ticket( 96, 35, "e7d88cb7-a424-4795-8965-17273642b773", new DateTime(2023, 8, 18, 23, 50, 0, 0, DateTimeKind.Unspecified), 63, 6m, 148, "R34C15" ),
                    new Ticket( 97, 41, "bfa19e5f-4529-4276-bde8-8e6d3de2c423", new DateTime(2023, 8, 1, 11, 20, 0, 0, DateTimeKind.Unspecified), 81, 18m, 192, "R5C23" ),
                    new Ticket( 98, 13, "4634669c-c5ad-41e6-8b41-f1524c9654ad", new DateTime(2023, 8, 20, 12, 30, 0, 0, DateTimeKind.Unspecified), 57, 5m, 272, "R6C17" ),
                    new Ticket( 99, 37, "1c850a33-6e0a-4c03-bb2d-c5a388042364", new DateTime(2023, 8, 27, 18, 30, 0, 0, DateTimeKind.Unspecified), 7, 24m, 294, "R34C26" ),
                    new Ticket( 100, 49, "bfa19e5f-4529-4276-bde8-8e6d3de2c423", new DateTime(2023, 8, 1, 18, 50, 0, 0, DateTimeKind.Unspecified), 43, 21m, 239, "R20C3" )
            };
            
            return tickets;
        }
    }
}
