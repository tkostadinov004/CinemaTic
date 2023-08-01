using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaTic.Data.Migrations
{
    public partial class AddedDates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "CinemasMovies",
                newName: "ToDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "FromDate",
                table: "CinemasMovies",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FromDate",
                table: "CinemasMovies");

            migrationBuilder.RenameColumn(
                name: "ToDate",
                table: "CinemasMovies",
                newName: "Date");
        }
    }
}
