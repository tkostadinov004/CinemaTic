﻿using CinemaTic.Data.Models;
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

namespace CinemaTic.Data.Configurations
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
                new Ticket( 1, 6, "4634669c-c5ad-41e6-8b41-f1524c9654ad", new DateTime(2023, 8, 8, 15, 10, 0, 0, DateTimeKind.Unspecified), 60, 23m, 376, "R29C2" ),
                    new Ticket( 2, 47, "2f09c66b-0830-4fcc-8a0f-f29b0990c669", new DateTime(2023, 8, 4, 23, 40, 0, 0, DateTimeKind.Unspecified), 70, 9m, 503, "R9C47" ),
                    new Ticket( 3, 20, "bfa19e5f-4529-4276-bde8-8e6d3de2c423", new DateTime(2023, 7, 31, 18, 20, 0, 0, DateTimeKind.Unspecified), 70, 21m, 528, "R18C29" ),
                    new Ticket( 4, 41, "191d6e78-88fb-40ce-b85c-d8bcf4a1ae4c", new DateTime(2023, 8, 17, 20, 20, 0, 0, DateTimeKind.Unspecified), 39, 22m, 508, "R28C9" ),
                    new Ticket( 5, 46, "c21bf410-3e22-4720-b01a-f2d91191a222", new DateTime(2023, 7, 30, 21, 15, 0, 0, DateTimeKind.Unspecified), 69, 8m, 585, "R23C35" ),
                    new Ticket( 6, 45, "c21bf410-3e22-4720-b01a-f2d91191a222", new DateTime(2023, 8, 3, 19, 30, 0, 0, DateTimeKind.Unspecified), 96, 7m, 255, "R34C23" ),
                    new Ticket( 7, 34, "f338b628-feaf-4a03-95ad-defb7aec5c83", new DateTime(2023, 7, 31, 14, 50, 0, 0, DateTimeKind.Unspecified), 57, 8m, 143, "R6C18" ),
                    new Ticket( 8, 44, "f338b628-feaf-4a03-95ad-defb7aec5c83", new DateTime(2023, 8, 5, 19, 50, 0, 0, DateTimeKind.Unspecified), 92, 24m, 454, "R38C15" ),
                    new Ticket( 9, 36, "bfa19e5f-4529-4276-bde8-8e6d3de2c423", new DateTime(2023, 8, 4, 9, 15, 0, 0, DateTimeKind.Unspecified), 8, 10m, 282, "R47C15" ),
                    new Ticket( 10, 23, "e7d88cb7-a424-4795-8965-17273642b773", new DateTime(2023, 8, 8, 9, 15, 0, 0, DateTimeKind.Unspecified), 48, 25m, 96, "R28C15" ),
                    new Ticket( 11, 16, "2a8f5f5c-e539-4868-837b-9a19852a904e", new DateTime(2023, 8, 9, 8, 0, 0, 0, DateTimeKind.Unspecified), 51, 6m, 134, "R18C5" ),
                    new Ticket( 12, 10, "1c850a33-6e0a-4c03-bb2d-c5a388042364", new DateTime(2023, 8, 15, 20, 20, 0, 0, DateTimeKind.Unspecified), 32, 22m, 447, "R4C33" ),
                    new Ticket( 13, 13, "2f09c66b-0830-4fcc-8a0f-f29b0990c669", new DateTime(2023, 8, 12, 10, 30, 0, 0, DateTimeKind.Unspecified), 10, 6m, 340, "R26C8" ),
                    new Ticket( 14, 34, "c21bf410-3e22-4720-b01a-f2d91191a222", new DateTime(2023, 8, 14, 18, 10, 0, 0, DateTimeKind.Unspecified), 89, 17m, 443, "R53C15" ),
                    new Ticket( 15, 25, "4634669c-c5ad-41e6-8b41-f1524c9654ad", new DateTime(2023, 8, 17, 18, 15, 0, 0, DateTimeKind.Unspecified), 24, 9m, 81, "R13C23" ),
                    new Ticket( 16, 23, "e7d88cb7-a424-4795-8965-17273642b773", new DateTime(2023, 8, 17, 19, 10, 0, 0, DateTimeKind.Unspecified), 21, 14m, 567, "R11C33" ),
                    new Ticket( 17, 46, "c21bf410-3e22-4720-b01a-f2d91191a222", new DateTime(2023, 7, 31, 10, 0, 0, 0, DateTimeKind.Unspecified), 81, 24m, 260, "R17C13" ),
                    new Ticket( 18, 25, "e7d88cb7-a424-4795-8965-17273642b773", new DateTime(2023, 8, 17, 15, 50, 0, 0, DateTimeKind.Unspecified), 24, 9m, 551, "R17C10" ),
                    new Ticket( 19, 49, "191d6e78-88fb-40ce-b85c-d8bcf4a1ae4c", new DateTime(2023, 8, 2, 19, 15, 0, 0, DateTimeKind.Unspecified), 67, 23m, 137, "R18C57" ),
                    new Ticket( 20, 42, "f338b628-feaf-4a03-95ad-defb7aec5c83", new DateTime(2023, 8, 14, 15, 45, 0, 0, DateTimeKind.Unspecified), 18, 14m, 378, "R23C32" ),
                    new Ticket( 21, 45, "f338b628-feaf-4a03-95ad-defb7aec5c83", new DateTime(2023, 8, 2, 21, 0, 0, 0, DateTimeKind.Unspecified), 96, 7m, 197, "R32C6" ),
                    new Ticket( 22, 26, "c21bf410-3e22-4720-b01a-f2d91191a222", new DateTime(2023, 7, 31, 12, 20, 0, 0, DateTimeKind.Unspecified), 82, 24m, 304, "R27C24" ),
                    new Ticket( 23, 29, "191d6e78-88fb-40ce-b85c-d8bcf4a1ae4c", new DateTime(2023, 8, 16, 21, 15, 0, 0, DateTimeKind.Unspecified), 36, 5m, 253, "R29C50" ),
                    new Ticket( 24, 1, "e7d88cb7-a424-4795-8965-17273642b773", new DateTime(2023, 8, 13, 11, 30, 0, 0, DateTimeKind.Unspecified), 8, 6m, 479, "R15C49" ),
                    new Ticket( 25, 41, "191d6e78-88fb-40ce-b85c-d8bcf4a1ae4c", new DateTime(2023, 8, 17, 8, 20, 0, 0, DateTimeKind.Unspecified), 86, 25m, 495, "R37C29" ),
                    new Ticket( 26, 9, "e7d88cb7-a424-4795-8965-17273642b773", new DateTime(2023, 8, 11, 12, 30, 0, 0, DateTimeKind.Unspecified), 63, 14m, 592, "R19C12" ),
                    new Ticket( 27, 43, "191d6e78-88fb-40ce-b85c-d8bcf4a1ae4c", new DateTime(2023, 8, 14, 21, 50, 0, 0, DateTimeKind.Unspecified), 17, 18m, 204, "R9C30" ),
                    new Ticket( 28, 50, "191d6e78-88fb-40ce-b85c-d8bcf4a1ae4c", new DateTime(2023, 8, 4, 18, 45, 0, 0, DateTimeKind.Unspecified), 99, 18m, 144, "R1C34" ),
                    new Ticket( 29, 23, "e7d88cb7-a424-4795-8965-17273642b773", new DateTime(2023, 8, 11, 12, 50, 0, 0, DateTimeKind.Unspecified), 48, 25m, 359, "R4C26" ),
                    new Ticket( 30, 34, "c21bf410-3e22-4720-b01a-f2d91191a222", new DateTime(2023, 8, 2, 13, 15, 0, 0, DateTimeKind.Unspecified), 68, 16m, 515, "R15C23" ),
                    new Ticket( 31, 17, "f338b628-feaf-4a03-95ad-defb7aec5c83", new DateTime(2023, 8, 5, 13, 10, 0, 0, DateTimeKind.Unspecified), 95, 9m, 496, "R46C2" ),
                    new Ticket( 32, 6, "f338b628-feaf-4a03-95ad-defb7aec5c83", new DateTime(2023, 8, 10, 13, 0, 0, 0, DateTimeKind.Unspecified), 69, 8m, 343, "R31C16" ),
                    new Ticket( 33, 45, "c21bf410-3e22-4720-b01a-f2d91191a222", new DateTime(2023, 8, 3, 23, 40, 0, 0, DateTimeKind.Unspecified), 79, 7m, 609, "R19C30" ),
                    new Ticket( 34, 7, "96256cfb-df20-4a1f-8898-f06f634a17d7", new DateTime(2023, 8, 12, 20, 50, 0, 0, DateTimeKind.Unspecified), 13, 19m, 532, "R29C33" ),
                    new Ticket( 35, 49, "2f09c66b-0830-4fcc-8a0f-f29b0990c669", new DateTime(2023, 8, 2, 22, 45, 0, 0, DateTimeKind.Unspecified), 83, 22m, 613, "R23C18" ),
                    new Ticket( 36, 20, "1c850a33-6e0a-4c03-bb2d-c5a388042364", new DateTime(2023, 8, 3, 16, 0, 0, 0, DateTimeKind.Unspecified), 49, 18m, 36, "R48C27" ),
                    new Ticket( 37, 31, "191d6e78-88fb-40ce-b85c-d8bcf4a1ae4c", new DateTime(2023, 8, 11, 17, 10, 0, 0, DateTimeKind.Unspecified), 52, 17m, 32, "R36C2" ),
                    new Ticket( 38, 45, "c21bf410-3e22-4720-b01a-f2d91191a222", new DateTime(2023, 8, 7, 16, 45, 0, 0, DateTimeKind.Unspecified), 29, 16m, 279, "R33C4" ),
                    new Ticket( 39, 46, "c21bf410-3e22-4720-b01a-f2d91191a222", new DateTime(2023, 8, 2, 15, 15, 0, 0, DateTimeKind.Unspecified), 33, 6m, 461, "R20C21" ),
                    new Ticket( 40, 37, "191d6e78-88fb-40ce-b85c-d8bcf4a1ae4c", new DateTime(2023, 8, 14, 14, 15, 0, 0, DateTimeKind.Unspecified), 3, 12m, 239, "R44C5" ),
                    new Ticket( 41, 45, "f338b628-feaf-4a03-95ad-defb7aec5c83", new DateTime(2023, 7, 31, 13, 45, 0, 0, DateTimeKind.Unspecified), 93, 21m, 383, "R41C27" ),
                    new Ticket( 42, 2, "191d6e78-88fb-40ce-b85c-d8bcf4a1ae4c", new DateTime(2023, 8, 11, 18, 40, 0, 0, DateTimeKind.Unspecified), 19, 16m, 144, "R8C41" ),
                     new Ticket( 43, 20, "bfa19e5f-4529-4276-bde8-8e6d3de2c423", new DateTime(2023, 8, 2, 20, 45, 0, 0, DateTimeKind.Unspecified), 49, 18m, 137, "R18C58" ),
                    new Ticket( 44, 4, "2f09c66b-0830-4fcc-8a0f-f29b0990c669", new DateTime(2023, 8, 12, 21, 10, 0, 0, DateTimeKind.Unspecified), 64, 8m, 632, "R3C11" ),
                    new Ticket( 45, 10, "4634669c-c5ad-41e6-8b41-f1524c9654ad", new DateTime(2023, 8, 7, 19, 50, 0, 0, DateTimeKind.Unspecified), 55, 6m, 575, "R31C33" ),
                    new Ticket( 46, 8, "4634669c-c5ad-41e6-8b41-f1524c9654ad", new DateTime(2023, 8, 9, 16, 20, 0, 0, DateTimeKind.Unspecified), 94, 15m, 464, "R27C16" ),
                    new Ticket( 47, 11, "1c850a33-6e0a-4c03-bb2d-c5a388042364", new DateTime(2023, 8, 14, 8, 0, 0, 0, DateTimeKind.Unspecified), 24, 12m, 433, "R6C7" ),
                    new Ticket( 48, 7, "96256cfb-df20-4a1f-8898-f06f634a17d7", new DateTime(2023, 8, 13, 18, 30, 0, 0, DateTimeKind.Unspecified), 19, 12m, 621, "R7C23" ),
                    new Ticket( 49, 42, "f338b628-feaf-4a03-95ad-defb7aec5c83", new DateTime(2023, 8, 12, 9, 10, 0, 0, DateTimeKind.Unspecified), 73, 8m, 411, "R4C31" ),
                    new Ticket( 50, 1, "e7d88cb7-a424-4795-8965-17273642b773", new DateTime(2023, 8, 14, 11, 0, 0, 0, DateTimeKind.Unspecified), 82, 22m, 33, "R32C18" ),
                    new Ticket( 51, 41, "c21bf410-3e22-4720-b01a-f2d91191a222", new DateTime(2023, 8, 17, 20, 20, 0, 0, DateTimeKind.Unspecified), 39, 22m, 594, "R11C31" ),
                    new Ticket( 52, 1, "e7d88cb7-a424-4795-8965-17273642b773", new DateTime(2023, 8, 16, 13, 30, 0, 0, DateTimeKind.Unspecified), 82, 22m, 159, "R22C24" ),
                    new Ticket( 53, 49, "2f09c66b-0830-4fcc-8a0f-f29b0990c669", new DateTime(2023, 8, 2, 19, 15, 0, 0, DateTimeKind.Unspecified), 67, 23m, 37, "R41C39" ),
                    new Ticket( 54, 16, "96256cfb-df20-4a1f-8898-f06f634a17d7", new DateTime(2023, 8, 9, 9, 45, 0, 0, DateTimeKind.Unspecified), 69, 14m, 553, "R25C12" ),
                    new Ticket( 55, 26, "c21bf410-3e22-4720-b01a-f2d91191a222", new DateTime(2023, 8, 10, 8, 30, 0, 0, DateTimeKind.Unspecified), 54, 22m, 149, "R29C17" ),
                    new Ticket( 56, 22, "4634669c-c5ad-41e6-8b41-f1524c9654ad", new DateTime(2023, 8, 2, 8, 45, 0, 0, DateTimeKind.Unspecified), 61, 11m, 131, "R8C27" ),
                    new Ticket( 57, 3, "bfa19e5f-4529-4276-bde8-8e6d3de2c423", new DateTime(2023, 8, 5, 17, 50, 0, 0, DateTimeKind.Unspecified), 68, 7m, 1, "R3C2" ),
                    new Ticket( 58, 8, "f338b628-feaf-4a03-95ad-defb7aec5c83", new DateTime(2023, 8, 16, 10, 30, 0, 0, DateTimeKind.Unspecified), 91, 20m, 296, "R36C31" ),
                    new Ticket( 59, 2, "191d6e78-88fb-40ce-b85c-d8bcf4a1ae4c", new DateTime(2023, 8, 10, 14, 0, 0, 0, DateTimeKind.Unspecified), 44, 10m, 419, "R34C27" ),
                    new Ticket( 60, 45, "c21bf410-3e22-4720-b01a-f2d91191a222", new DateTime(2023, 8, 6, 8, 10, 0, 0, DateTimeKind.Unspecified), 32, 14m, 425, "R29C5" ),
                    new Ticket( 61, 36, "2f09c66b-0830-4fcc-8a0f-f29b0990c669", new DateTime(2023, 7, 30, 19, 0, 0, 0, DateTimeKind.Unspecified), 1, 20m, 573, "R33C7" ),
                    new Ticket( 62, 22, "4634669c-c5ad-41e6-8b41-f1524c9654ad", new DateTime(2023, 8, 8, 19, 0, 0, 0, DateTimeKind.Unspecified), 75, 24m, 301, "R18C29" ),
                    new Ticket( 63, 46, "c21bf410-3e22-4720-b01a-f2d91191a222", new DateTime(2023, 7, 30, 9, 30, 0, 0, DateTimeKind.Unspecified), 69, 8m, 211, "R23C31" ),
                    new Ticket( 64, 10, "2f09c66b-0830-4fcc-8a0f-f29b0990c669", new DateTime(2023, 8, 6, 8, 50, 0, 0, DateTimeKind.Unspecified), 55, 6m, 27, "R17C18" ),
                    new Ticket( 65, 37, "c21bf410-3e22-4720-b01a-f2d91191a222", new DateTime(2023, 8, 5, 12, 15, 0, 0, DateTimeKind.Unspecified), 37, 20m, 445, "R2C14" ),
                    new Ticket( 66, 45, "f338b628-feaf-4a03-95ad-defb7aec5c83", new DateTime(2023, 8, 7, 20, 10, 0, 0, DateTimeKind.Unspecified), 32, 14m, 120, "R16C35" ),
                    new Ticket( 67, 12, "191d6e78-88fb-40ce-b85c-d8bcf4a1ae4c", new DateTime(2023, 8, 16, 12, 30, 0, 0, DateTimeKind.Unspecified), 77, 11m, 584, "R29C29" ),
                    new Ticket( 68, 17, "f338b628-feaf-4a03-95ad-defb7aec5c83", new DateTime(2023, 8, 5, 21, 30, 0, 0, DateTimeKind.Unspecified), 35, 22m, 630, "R52C13" ),
                    new Ticket( 69, 22, "4634669c-c5ad-41e6-8b41-f1524c9654ad", new DateTime(2023, 8, 16, 8, 20, 0, 0, DateTimeKind.Unspecified), 50, 9m, 522, "R54C6" ),
                    new Ticket( 70, 26, "c21bf410-3e22-4720-b01a-f2d91191a222", new DateTime(2023, 8, 8, 12, 30, 0, 0, DateTimeKind.Unspecified), 54, 22m, 614, "R25C37" ),
                    new Ticket( 71, 45, "f338b628-feaf-4a03-95ad-defb7aec5c83", new DateTime(2023, 8, 3, 23, 40, 0, 0, DateTimeKind.Unspecified), 79, 7m, 536, "R43C2" ),
                    new Ticket( 72, 34, "f338b628-feaf-4a03-95ad-defb7aec5c83", new DateTime(2023, 8, 1, 15, 30, 0, 0, DateTimeKind.Unspecified), 57, 8m, 236, "R35C11" ),
                    new Ticket( 73, 13, "2f09c66b-0830-4fcc-8a0f-f29b0990c669", new DateTime(2023, 8, 15, 20, 50, 0, 0, DateTimeKind.Unspecified), 10, 6m, 382, "R45C7" ),
                    new Ticket( 74, 17, "f338b628-feaf-4a03-95ad-defb7aec5c83", new DateTime(2023, 8, 3, 14, 10, 0, 0, DateTimeKind.Unspecified), 22, 16m, 238, "R40C32" ),
                    new Ticket( 75, 37, "c21bf410-3e22-4720-b01a-f2d91191a222", new DateTime(2023, 8, 5, 20, 45, 0, 0, DateTimeKind.Unspecified), 98, 8m, 532, "R24C34" ),
                    new Ticket( 76, 45, "2a8f5f5c-e539-4868-837b-9a19852a904e", new DateTime(2023, 8, 8, 15, 40, 0, 0, DateTimeKind.Unspecified), 96, 7m, 355, "R50C17" ),
                    new Ticket( 77, 10, "4634669c-c5ad-41e6-8b41-f1524c9654ad", new DateTime(2023, 8, 1, 11, 30, 0, 0, DateTimeKind.Unspecified), 28, 19m, 178, "R9C7" ),
                    new Ticket( 78, 5, "c21bf410-3e22-4720-b01a-f2d91191a222", new DateTime(2023, 8, 16, 12, 20, 0, 0, DateTimeKind.Unspecified), 66, 24m, 439, "R34C5" ),
                    new Ticket( 79, 46, "c21bf410-3e22-4720-b01a-f2d91191a222", new DateTime(2023, 7, 30, 16, 30, 0, 0, DateTimeKind.Unspecified), 69, 8m, 361, "R14C7" ),
                    new Ticket( 80, 10, "2f09c66b-0830-4fcc-8a0f-f29b0990c669", new DateTime(2023, 8, 7, 19, 50, 0, 0, DateTimeKind.Unspecified), 55, 6m, 619, "R35C47" ),
                    new Ticket( 81, 20, "1c850a33-6e0a-4c03-bb2d-c5a388042364", new DateTime(2023, 8, 2, 17, 50, 0, 0, DateTimeKind.Unspecified), 29, 8m, 254, "R35C14" ),
                    new Ticket( 82, 45, "bfa19e5f-4529-4276-bde8-8e6d3de2c423", new DateTime(2023, 8, 11, 13, 10, 0, 0, DateTimeKind.Unspecified), 79, 7m, 360, "R5C39" ),
                    new Ticket( 83, 7, "bfa19e5f-4529-4276-bde8-8e6d3de2c423", new DateTime(2023, 8, 6, 9, 10, 0, 0, DateTimeKind.Unspecified), 30, 10m, 183, "R21C24" ),
                    new Ticket( 84, 45, "2a8f5f5c-e539-4868-837b-9a19852a904e", new DateTime(2023, 7, 31, 22, 0, 0, 0, DateTimeKind.Unspecified), 62, 9m, 512, "R7C3" ),
                      new Ticket( 85, 37, "c21bf410-3e22-4720-b01a-f2d91191a222", new DateTime(2023, 8, 7, 13, 0, 0, 0, DateTimeKind.Unspecified), 79, 17m, 275, "R18C31" ),
                    new Ticket( 86, 45, "f338b628-feaf-4a03-95ad-defb7aec5c83", new DateTime(2023, 8, 16, 13, 45, 0, 0, DateTimeKind.Unspecified), 36, 13m, 164, "R59C6" ),
                    new Ticket( 87, 45, "2a8f5f5c-e539-4868-837b-9a19852a904e", new DateTime(2023, 8, 6, 9, 20, 0, 0, DateTimeKind.Unspecified), 79, 7m, 49, "R22C5" ),
                    new Ticket( 88, 6, "4634669c-c5ad-41e6-8b41-f1524c9654ad", new DateTime(2023, 8, 13, 14, 0, 0, 0, DateTimeKind.Unspecified), 28, 8m, 571, "R30C34" ),
                    new Ticket( 89, 11, "1c850a33-6e0a-4c03-bb2d-c5a388042364", new DateTime(2023, 8, 15, 15, 20, 0, 0, DateTimeKind.Unspecified), 24, 12m, 197, "R35C8" ),
                    new Ticket( 90, 16, "2a8f5f5c-e539-4868-837b-9a19852a904e", new DateTime(2023, 8, 11, 12, 15, 0, 0, DateTimeKind.Unspecified), 2, 15m, 325, "R5C24" ),
                    new Ticket( 91, 1, "e7d88cb7-a424-4795-8965-17273642b773", new DateTime(2023, 8, 14, 11, 0, 0, 0, DateTimeKind.Unspecified), 82, 22m, 498, "R55C5" ),
                    new Ticket( 92, 2, "191d6e78-88fb-40ce-b85c-d8bcf4a1ae4c", new DateTime(2023, 8, 14, 8, 50, 0, 0, DateTimeKind.Unspecified), 5, 22m, 269, "R58C22" ),
                    new Ticket( 93, 45, "c21bf410-3e22-4720-b01a-f2d91191a222", new DateTime(2023, 8, 1, 23, 0, 0, 0, DateTimeKind.Unspecified), 93, 21m, 85, "R39C18" ),
                    new Ticket( 94, 10, "1c850a33-6e0a-4c03-bb2d-c5a388042364", new DateTime(2023, 8, 6, 19, 45, 0, 0, DateTimeKind.Unspecified), 55, 6m, 326, "R19C9" ),
                    new Ticket( 95, 14, "96256cfb-df20-4a1f-8898-f06f634a17d7", new DateTime(2023, 8, 7, 11, 30, 0, 0, DateTimeKind.Unspecified), 7, 24m, 642, "R21C43" ),
                    new Ticket( 96, 6, "4634669c-c5ad-41e6-8b41-f1524c9654ad", new DateTime(2023, 8, 11, 11, 45, 0, 0, DateTimeKind.Unspecified), 69, 8m, 594, "R18C34" ),
                    new Ticket( 97, 49, "191d6e78-88fb-40ce-b85c-d8bcf4a1ae4c", new DateTime(2023, 8, 2, 19, 15, 0, 0, DateTimeKind.Unspecified), 67, 23m, 437, "R26C10" ),
                    new Ticket( 98, 10, "1c850a33-6e0a-4c03-bb2d-c5a388042364", new DateTime(2023, 8, 16, 22, 15, 0, 0, DateTimeKind.Unspecified), 100, 11m, 493, "R26C30" ),
                    new Ticket( 99, 24, "bfa19e5f-4529-4276-bde8-8e6d3de2c423", new DateTime(2023, 8, 14, 15, 15, 0, 0, DateTimeKind.Unspecified), 73, 24m, 453, "R23C37" ),
                    new Ticket( 100, 49, "2f09c66b-0830-4fcc-8a0f-f29b0990c669", new DateTime(2023, 8, 2, 19, 15, 0, 0, DateTimeKind.Unspecified), 67, 23m, 164, "R51C4" ),
                    new Ticket( 101, 10, "4634669c-c5ad-41e6-8b41-f1524c9654ad", new DateTime(2023, 8, 4, 11, 30, 0, 0, DateTimeKind.Unspecified), 67, 7m, 592, "R14C5" ),
                    new Ticket( 102, 4, "2f09c66b-0830-4fcc-8a0f-f29b0990c669", new DateTime(2023, 8, 12, 18, 50, 0, 0, DateTimeKind.Unspecified), 48, 5m, 443, "R51C11" ),
                    new Ticket( 103, 14, "96256cfb-df20-4a1f-8898-f06f634a17d7", new DateTime(2023, 8, 13, 20, 45, 0, 0, DateTimeKind.Unspecified), 7, 24m, 551, "R14C14" ),
                    new Ticket( 104, 17, "f338b628-feaf-4a03-95ad-defb7aec5c83", new DateTime(2023, 8, 11, 8, 0, 0, 0, DateTimeKind.Unspecified), 7, 5m, 70, "R18C12" ),
                    new Ticket( 105, 16, "2a8f5f5c-e539-4868-837b-9a19852a904e", new DateTime(2023, 8, 6, 20, 40, 0, 0, DateTimeKind.Unspecified), 91, 15m, 465, "R26C35" ),
                    new Ticket( 106, 12, "96256cfb-df20-4a1f-8898-f06f634a17d7", new DateTime(2023, 8, 11, 19, 50, 0, 0, DateTimeKind.Unspecified), 13, 16m, 604, "R8C2" ),
                    new Ticket( 107, 36, "bfa19e5f-4529-4276-bde8-8e6d3de2c423", new DateTime(2023, 8, 4, 9, 15, 0, 0, DateTimeKind.Unspecified), 8, 10m, 146, "R19C24" ),
                    new Ticket( 108, 47, "2f09c66b-0830-4fcc-8a0f-f29b0990c669", new DateTime(2023, 8, 16, 9, 20, 0, 0, DateTimeKind.Unspecified), 29, 21m, 490, "R12C4" ),
                    new Ticket( 109, 49, "191d6e78-88fb-40ce-b85c-d8bcf4a1ae4c", new DateTime(2023, 8, 3, 13, 20, 0, 0, DateTimeKind.Unspecified), 19, 8m, 309, "R43C11" ),
                    new Ticket( 110, 50, "191d6e78-88fb-40ce-b85c-d8bcf4a1ae4c", new DateTime(2023, 8, 14, 12, 50, 0, 0, DateTimeKind.Unspecified), 20, 22m, 507, "R19C55" ),
                    new Ticket( 111, 42, "f338b628-feaf-4a03-95ad-defb7aec5c83", new DateTime(2023, 8, 16, 8, 15, 0, 0, DateTimeKind.Unspecified), 88, 17m, 53, "R7C11" ),
                    new Ticket( 112, 46, "1c850a33-6e0a-4c03-bb2d-c5a388042364", new DateTime(2023, 8, 2, 12, 0, 0, 0, DateTimeKind.Unspecified), 57, 25m, 50, "R28C25" ),
                    new Ticket( 113, 1, "e7d88cb7-a424-4795-8965-17273642b773", new DateTime(2023, 8, 16, 23, 50, 0, 0, DateTimeKind.Unspecified), 82, 22m, 328, "R30C5" ),
                    new Ticket( 114, 15, "f338b628-feaf-4a03-95ad-defb7aec5c83", new DateTime(2023, 8, 15, 21, 45, 0, 0, DateTimeKind.Unspecified), 81, 11m, 464, "R21C21" ),
                    new Ticket( 115, 28, "96256cfb-df20-4a1f-8898-f06f634a17d7", new DateTime(2023, 8, 12, 11, 10, 0, 0, DateTimeKind.Unspecified), 98, 5m, 428, "R40C19" ),
                    new Ticket( 116, 10, "2f09c66b-0830-4fcc-8a0f-f29b0990c669", new DateTime(2023, 8, 16, 9, 30, 0, 0, DateTimeKind.Unspecified), 82, 20m, 270, "R8C14" ),
                    new Ticket( 117, 7, "96256cfb-df20-4a1f-8898-f06f634a17d7", new DateTime(2023, 8, 14, 22, 50, 0, 0, DateTimeKind.Unspecified), 19, 12m, 583, "R29C7" ),
                    new Ticket( 118, 49, "2a8f5f5c-e539-4868-837b-9a19852a904e", new DateTime(2023, 8, 2, 19, 15, 0, 0, DateTimeKind.Unspecified), 67, 23m, 314, "R10C39" ),
                    new Ticket( 119, 10, "1c850a33-6e0a-4c03-bb2d-c5a388042364", new DateTime(2023, 8, 7, 19, 50, 0, 0, DateTimeKind.Unspecified), 55, 6m, 305, "R26C34" ),
                    new Ticket( 120, 34, "f338b628-feaf-4a03-95ad-defb7aec5c83", new DateTime(2023, 8, 15, 18, 15, 0, 0, DateTimeKind.Unspecified), 87, 10m, 345, "R42C22" ),
                    new Ticket( 121, 8, "4634669c-c5ad-41e6-8b41-f1524c9654ad", new DateTime(2023, 8, 9, 16, 20, 0, 0, DateTimeKind.Unspecified), 94, 15m, 633, "R7C20" ),
                    new Ticket( 122, 5, "c21bf410-3e22-4720-b01a-f2d91191a222", new DateTime(2023, 8, 10, 20, 45, 0, 0, DateTimeKind.Unspecified), 44, 5m, 87, "R50C19" ),
                    new Ticket( 123, 49, "2f09c66b-0830-4fcc-8a0f-f29b0990c669", new DateTime(2023, 8, 2, 11, 20, 0, 0, DateTimeKind.Unspecified), 83, 22m, 306, "R38C6" ),
                    new Ticket( 124, 34, "c21bf410-3e22-4720-b01a-f2d91191a222", new DateTime(2023, 8, 13, 11, 40, 0, 0, DateTimeKind.Unspecified), 89, 17m, 163, "R49C16" ),
                    new Ticket( 125, 16, "2a8f5f5c-e539-4868-837b-9a19852a904e", new DateTime(2023, 8, 14, 11, 30, 0, 0, DateTimeKind.Unspecified), 13, 10m, 3, "R11C12" ),
                    new Ticket( 126, 37, "c21bf410-3e22-4720-b01a-f2d91191a222", new DateTime(2023, 8, 5, 12, 15, 0, 0, DateTimeKind.Unspecified), 37, 20m, 593, "R14C20" ),
                    new Ticket( 127, 45, "2a8f5f5c-e539-4868-837b-9a19852a904e", new DateTime(2023, 8, 3, 18, 20, 0, 0, DateTimeKind.Unspecified), 93, 21m, 549, "R3C15" ),
                    new Ticket( 128, 12, "191d6e78-88fb-40ce-b85c-d8bcf4a1ae4c", new DateTime(2023, 8, 11, 19, 45, 0, 0, DateTimeKind.Unspecified), 13, 16m, 101, "R37C31" ),
                    new Ticket( 129, 1, "bfa19e5f-4529-4276-bde8-8e6d3de2c423", new DateTime(2023, 8, 13, 11, 30, 0, 0, DateTimeKind.Unspecified), 8, 6m, 559, "R54C6" ),
                    new Ticket( 130, 45, "f338b628-feaf-4a03-95ad-defb7aec5c83", new DateTime(2023, 8, 6, 8, 10, 0, 0, DateTimeKind.Unspecified), 32, 14m, 38, "R59C12" ),
                    new Ticket( 131, 10, "4634669c-c5ad-41e6-8b41-f1524c9654ad", new DateTime(2023, 8, 10, 22, 0, 0, 0, DateTimeKind.Unspecified), 92, 6m, 346, "R3C15" ),
                    new Ticket( 132, 46, "96256cfb-df20-4a1f-8898-f06f634a17d7", new DateTime(2023, 8, 12, 12, 50, 0, 0, DateTimeKind.Unspecified), 4, 13m, 319, "R11C46" ),
                    new Ticket( 133, 14, "96256cfb-df20-4a1f-8898-f06f634a17d7", new DateTime(2023, 8, 16, 15, 40, 0, 0, DateTimeKind.Unspecified), 24, 8m, 34, "R36C38" ),
                    new Ticket( 134, 49, "191d6e78-88fb-40ce-b85c-d8bcf4a1ae4c", new DateTime(2023, 8, 4, 17, 50, 0, 0, DateTimeKind.Unspecified), 83, 22m, 293, "R30C35" ),
                    new Ticket( 135, 22, "4634669c-c5ad-41e6-8b41-f1524c9654ad", new DateTime(2023, 8, 3, 10, 15, 0, 0, DateTimeKind.Unspecified), 75, 24m, 619, "R33C49" ),
                    new Ticket( 136, 10, "1c850a33-6e0a-4c03-bb2d-c5a388042364", new DateTime(2023, 8, 6, 15, 40, 0, 0, DateTimeKind.Unspecified), 85, 14m, 118, "R14C4" ),
                    new Ticket( 137, 38, "f338b628-feaf-4a03-95ad-defb7aec5c83", new DateTime(2023, 8, 17, 16, 15, 0, 0, DateTimeKind.Unspecified), 29, 12m, 142, "R7C8" ),
                    new Ticket( 138, 19, "191d6e78-88fb-40ce-b85c-d8bcf4a1ae4c", new DateTime(2023, 8, 14, 16, 50, 0, 0, DateTimeKind.Unspecified), 99, 18m, 592, "R13C10" ),
                    new Ticket( 139, 49, "2f09c66b-0830-4fcc-8a0f-f29b0990c669", new DateTime(2023, 8, 2, 19, 15, 0, 0, DateTimeKind.Unspecified), 67, 23m, 215, "R2C1" ),
                    new Ticket( 140, 6, "4634669c-c5ad-41e6-8b41-f1524c9654ad", new DateTime(2023, 8, 13, 18, 20, 0, 0, DateTimeKind.Unspecified), 53, 11m, 285, "R5C11" ),
                    new Ticket( 141, 22, "4634669c-c5ad-41e6-8b41-f1524c9654ad", new DateTime(2023, 8, 1, 20, 45, 0, 0, DateTimeKind.Unspecified), 66, 19m, 43, "R6C34" ),
                    new Ticket( 142, 49, "2a8f5f5c-e539-4868-837b-9a19852a904e", new DateTime(2023, 7, 31, 18, 40, 0, 0, DateTimeKind.Unspecified), 83, 22m, 363, "R16C43" ),
                    new Ticket( 143, 38, "f338b628-feaf-4a03-95ad-defb7aec5c83", new DateTime(2023, 8, 7, 14, 0, 0, 0, DateTimeKind.Unspecified), 59, 22m, 236, "R37C8" ),
                    new Ticket( 144, 45, "f338b628-feaf-4a03-95ad-defb7aec5c83", new DateTime(2023, 7, 31, 19, 20, 0, 0, DateTimeKind.Unspecified), 62, 9m, 138, "R27C12" ),
                    new Ticket( 145, 2, "191d6e78-88fb-40ce-b85c-d8bcf4a1ae4c", new DateTime(2023, 8, 12, 22, 45, 0, 0, DateTimeKind.Unspecified), 19, 16m, 478, "R18C44" ),
                    new Ticket( 146, 19, "2a8f5f5c-e539-4868-837b-9a19852a904e", new DateTime(2023, 8, 8, 8, 10, 0, 0, DateTimeKind.Unspecified), 99, 18m, 333, "R48C18" ),
                    new Ticket( 147, 12, "1c850a33-6e0a-4c03-bb2d-c5a388042364", new DateTime(2023, 8, 10, 11, 10, 0, 0, DateTimeKind.Unspecified), 37, 11m, 152, "R32C25" ),
                    new Ticket( 148, 26, "c21bf410-3e22-4720-b01a-f2d91191a222", new DateTime(2023, 8, 10, 12, 30, 0, 0, DateTimeKind.Unspecified), 99, 6m, 237, "R39C22" ),
                    new Ticket( 149, 16, "2a8f5f5c-e539-4868-837b-9a19852a904e", new DateTime(2023, 8, 9, 9, 45, 0, 0, DateTimeKind.Unspecified), 69, 14m, 98, "R29C36" ),
                    new Ticket( 150, 36, "2f09c66b-0830-4fcc-8a0f-f29b0990c669", new DateTime(2023, 8, 4, 9, 15, 0, 0, DateTimeKind.Unspecified), 8, 10m, 619, "R33C47" ),
                    new Ticket( 151, 37, "c21bf410-3e22-4720-b01a-f2d91191a222", new DateTime(2023, 8, 15, 11, 10, 0, 0, DateTimeKind.Unspecified), 3, 12m, 34, "R35C44" ),
                    new Ticket( 152, 38, "f338b628-feaf-4a03-95ad-defb7aec5c83", new DateTime(2023, 8, 11, 14, 15, 0, 0, DateTimeKind.Unspecified), 57, 7m, 158, "R26C1" ),
                    new Ticket( 153, 2, "191d6e78-88fb-40ce-b85c-d8bcf4a1ae4c", new DateTime(2023, 8, 12, 13, 45, 0, 0, DateTimeKind.Unspecified), 77, 21m, 482, "R30C34" ),
                    new Ticket( 154, 45, "bfa19e5f-4529-4276-bde8-8e6d3de2c423", new DateTime(2023, 8, 8, 9, 0, 0, 0, DateTimeKind.Unspecified), 24, 5m, 121, "R14C49" ),
                    new Ticket( 155, 44, "f338b628-feaf-4a03-95ad-defb7aec5c83", new DateTime(2023, 8, 15, 20, 30, 0, 0, DateTimeKind.Unspecified), 13, 24m, 386, "R8C21" ),
                    new Ticket( 156, 22, "4634669c-c5ad-41e6-8b41-f1524c9654ad", new DateTime(2023, 7, 31, 14, 0, 0, 0, DateTimeKind.Unspecified), 23, 17m, 385, "R5C8" ),
                    new Ticket( 157, 17, "f338b628-feaf-4a03-95ad-defb7aec5c83", new DateTime(2023, 8, 3, 13, 15, 0, 0, DateTimeKind.Unspecified), 22, 16m, 73, "R30C17" ),
                    new Ticket( 158, 1, "bfa19e5f-4529-4276-bde8-8e6d3de2c423", new DateTime(2023, 8, 15, 14, 40, 0, 0, DateTimeKind.Unspecified), 80, 13m, 551, "R15C3" ),
                    new Ticket( 159, 17, "f338b628-feaf-4a03-95ad-defb7aec5c83", new DateTime(2023, 8, 5, 9, 15, 0, 0, DateTimeKind.Unspecified), 25, 10m, 77, "R49C18" ),
                    new Ticket( 160, 24, "4634669c-c5ad-41e6-8b41-f1524c9654ad", new DateTime(2023, 8, 4, 23, 15, 0, 0, DateTimeKind.Unspecified), 74, 23m, 613, "R22C25" ),
                    new Ticket( 161, 49, "2a8f5f5c-e539-4868-837b-9a19852a904e", new DateTime(2023, 8, 2, 19, 15, 0, 0, DateTimeKind.Unspecified), 67, 23m, 400, "R8C47" ),
                    new Ticket( 162, 23, "96256cfb-df20-4a1f-8898-f06f634a17d7", new DateTime(2023, 7, 30, 20, 20, 0, 0, DateTimeKind.Unspecified), 31, 10m, 68, "R3C12" ),
                    new Ticket( 163, 5, "c21bf410-3e22-4720-b01a-f2d91191a222", new DateTime(2023, 8, 9, 15, 40, 0, 0, DateTimeKind.Unspecified), 21, 19m, 171, "R30C26" ),
                    new Ticket( 164, 20, "191d6e78-88fb-40ce-b85c-d8bcf4a1ae4c", new DateTime(2023, 8, 5, 16, 30, 0, 0, DateTimeKind.Unspecified), 49, 18m, 391, "R34C4" ),
                    new Ticket( 165, 7, "1c850a33-6e0a-4c03-bb2d-c5a388042364", new DateTime(2023, 7, 30, 12, 45, 0, 0, DateTimeKind.Unspecified), 94, 22m, 339, "R19C17" ),
                    new Ticket( 166, 42, "f338b628-feaf-4a03-95ad-defb7aec5c83", new DateTime(2023, 8, 9, 22, 45, 0, 0, DateTimeKind.Unspecified), 73, 8m, 302, "R20C34" ),
                    new Ticket( 167, 46, "1c850a33-6e0a-4c03-bb2d-c5a388042364", new DateTime(2023, 8, 4, 18, 10, 0, 0, DateTimeKind.Unspecified), 2, 22m, 226, "R23C47" ),
                    new Ticket( 168, 30, "2a8f5f5c-e539-4868-837b-9a19852a904e", new DateTime(2023, 8, 15, 12, 45, 0, 0, DateTimeKind.Unspecified), 89, 18m, 529, "R13C35" ),
                     new Ticket( 169, 49, "191d6e78-88fb-40ce-b85c-d8bcf4a1ae4c", new DateTime(2023, 8, 2, 12, 40, 0, 0, DateTimeKind.Unspecified), 19, 8m, 211, "R24C36" ),
                    new Ticket( 170, 46, "96256cfb-df20-4a1f-8898-f06f634a17d7", new DateTime(2023, 7, 30, 21, 15, 0, 0, DateTimeKind.Unspecified), 69, 8m, 618, "R35C42" ),
                    new Ticket( 171, 4, "2f09c66b-0830-4fcc-8a0f-f29b0990c669", new DateTime(2023, 8, 11, 15, 40, 0, 0, DateTimeKind.Unspecified), 64, 8m, 435, "R18C6" ),
                    new Ticket( 172, 9, "4634669c-c5ad-41e6-8b41-f1524c9654ad", new DateTime(2023, 8, 9, 12, 40, 0, 0, DateTimeKind.Unspecified), 48, 25m, 345, "R44C28" ),
                    new Ticket( 173, 15, "bfa19e5f-4529-4276-bde8-8e6d3de2c423", new DateTime(2023, 8, 10, 10, 30, 0, 0, DateTimeKind.Unspecified), 81, 11m, 381, "R31C32" ),
                    new Ticket( 174, 17, "f338b628-feaf-4a03-95ad-defb7aec5c83", new DateTime(2023, 8, 11, 8, 0, 0, 0, DateTimeKind.Unspecified), 7, 5m, 74, "R34C15" ),
                    new Ticket( 175, 39, "1c850a33-6e0a-4c03-bb2d-c5a388042364", new DateTime(2023, 7, 31, 9, 50, 0, 0, DateTimeKind.Unspecified), 3, 17m, 35, "R46C12" ),
                    new Ticket( 176, 12, "96256cfb-df20-4a1f-8898-f06f634a17d7", new DateTime(2023, 8, 6, 10, 30, 0, 0, DateTimeKind.Unspecified), 37, 11m, 289, "R15C27" ),
                    new Ticket( 177, 24, "bfa19e5f-4529-4276-bde8-8e6d3de2c423", new DateTime(2023, 8, 17, 14, 10, 0, 0, DateTimeKind.Unspecified), 24, 19m, 619, "R34C49" ),
                    new Ticket( 178, 30, "2a8f5f5c-e539-4868-837b-9a19852a904e", new DateTime(2023, 8, 16, 14, 45, 0, 0, DateTimeKind.Unspecified), 89, 18m, 513, "R7C21" ),
                    new Ticket( 179, 20, "1c850a33-6e0a-4c03-bb2d-c5a388042364", new DateTime(2023, 8, 8, 11, 40, 0, 0, DateTimeKind.Unspecified), 70, 21m, 297, "R7C2" ),
                    new Ticket( 180, 46, "1c850a33-6e0a-4c03-bb2d-c5a388042364", new DateTime(2023, 8, 4, 23, 10, 0, 0, DateTimeKind.Unspecified), 2, 22m, 399, "R7C35" ),
                    new Ticket( 181, 49, "f338b628-feaf-4a03-95ad-defb7aec5c83", new DateTime(2023, 8, 3, 13, 20, 0, 0, DateTimeKind.Unspecified), 19, 8m, 274, "R18C16" ),
                    new Ticket( 182, 46, "96256cfb-df20-4a1f-8898-f06f634a17d7", new DateTime(2023, 8, 12, 9, 50, 0, 0, DateTimeKind.Unspecified), 2, 22m, 351, "R26C19" ),
                    new Ticket( 183, 45, "f338b628-feaf-4a03-95ad-defb7aec5c83", new DateTime(2023, 8, 16, 15, 15, 0, 0, DateTimeKind.Unspecified), 33, 18m, 237, "R31C20" ),
                    new Ticket( 184, 10, "2f09c66b-0830-4fcc-8a0f-f29b0990c669", new DateTime(2023, 8, 15, 20, 20, 0, 0, DateTimeKind.Unspecified), 32, 22m, 341, "R24C26" ),
                    new Ticket( 185, 45, "f338b628-feaf-4a03-95ad-defb7aec5c83", new DateTime(2023, 8, 7, 20, 10, 0, 0, DateTimeKind.Unspecified), 32, 14m, 516, "R24C10" ),
                    new Ticket( 186, 42, "f338b628-feaf-4a03-95ad-defb7aec5c83", new DateTime(2023, 8, 15, 9, 15, 0, 0, DateTimeKind.Unspecified), 3, 22m, 98, "R29C34" ),
                    new Ticket( 187, 4, "2f09c66b-0830-4fcc-8a0f-f29b0990c669", new DateTime(2023, 8, 8, 15, 30, 0, 0, DateTimeKind.Unspecified), 48, 5m, 385, "R1C7" ),
                    new Ticket( 188, 49, "191d6e78-88fb-40ce-b85c-d8bcf4a1ae4c", new DateTime(2023, 8, 2, 22, 45, 0, 0, DateTimeKind.Unspecified), 83, 22m, 375, "R12C35" ),
                    new Ticket( 189, 37, "c21bf410-3e22-4720-b01a-f2d91191a222", new DateTime(2023, 8, 8, 12, 50, 0, 0, DateTimeKind.Unspecified), 98, 8m, 219, "R16C6" ),
                    new Ticket( 190, 5, "c21bf410-3e22-4720-b01a-f2d91191a222", new DateTime(2023, 8, 14, 14, 10, 0, 0, DateTimeKind.Unspecified), 28, 22m, 437, "R28C12" ),
                    new Ticket( 191, 6, "f338b628-feaf-4a03-95ad-defb7aec5c83", new DateTime(2023, 8, 6, 19, 20, 0, 0, DateTimeKind.Unspecified), 60, 23m, 97, "R29C26" ),
                    new Ticket( 192, 45, "c21bf410-3e22-4720-b01a-f2d91191a222", new DateTime(2023, 8, 1, 15, 40, 0, 0, DateTimeKind.Unspecified), 93, 21m, 483, "R22C46" ),
                    new Ticket( 193, 45, "2a8f5f5c-e539-4868-837b-9a19852a904e", new DateTime(2023, 8, 13, 9, 50, 0, 0, DateTimeKind.Unspecified), 36, 13m, 599, "R37C25" ),
                    new Ticket( 194, 2, "191d6e78-88fb-40ce-b85c-d8bcf4a1ae4c", new DateTime(2023, 8, 9, 11, 30, 0, 0, DateTimeKind.Unspecified), 75, 23m, 564, "R2C49" ),
                    new Ticket( 195, 46, "1c850a33-6e0a-4c03-bb2d-c5a388042364", new DateTime(2023, 7, 31, 10, 0, 0, 0, DateTimeKind.Unspecified), 81, 24m, 393, "R49C12" ),
                    new Ticket( 196, 49, "2f09c66b-0830-4fcc-8a0f-f29b0990c669", new DateTime(2023, 8, 2, 19, 15, 0, 0, DateTimeKind.Unspecified), 67, 23m, 420, "R34C37" ),
                    new Ticket( 197, 5, "c21bf410-3e22-4720-b01a-f2d91191a222", new DateTime(2023, 8, 7, 9, 20, 0, 0, DateTimeKind.Unspecified), 5, 20m, 548, "R46C20" ),
                    new Ticket( 198, 17, "f338b628-feaf-4a03-95ad-defb7aec5c83", new DateTime(2023, 7, 30, 23, 40, 0, 0, DateTimeKind.Unspecified), 91, 24m, 178, "R4C11" ),
                    new Ticket( 199, 43, "191d6e78-88fb-40ce-b85c-d8bcf4a1ae4c", new DateTime(2023, 8, 17, 19, 50, 0, 0, DateTimeKind.Unspecified), 17, 18m, 496, "R48C14" ),
                    new Ticket( 200, 44, "f338b628-feaf-4a03-95ad-defb7aec5c83", new DateTime(2023, 8, 5, 19, 50, 0, 0, DateTimeKind.Unspecified), 92, 24m, 521, "R43C17" )
            };
            
            return tickets;
        }
    }
}
