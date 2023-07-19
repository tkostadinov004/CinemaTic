using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinema.Data.Migrations
{
    public partial class ChangedTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Time",
                table: "CinemasMoviesTimes");

            migrationBuilder.RenameColumn(
                name: "ForDate",
                table: "CinemasMoviesTimes",
                newName: "ForDateTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ForDateTime",
                table: "CinemasMoviesTimes",
                newName: "ForDate");

            migrationBuilder.AddColumn<string>(
                name: "Time",
                table: "CinemasMoviesTimes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
