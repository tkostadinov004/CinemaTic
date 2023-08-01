using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaTic.Data.Migrations
{
    public partial class ChangedMovieTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CinemasMoviesTimes",
                table: "CinemasMoviesTimes");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "CinemasMoviesTimes",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CinemasMoviesTimes",
                table: "CinemasMoviesTimes",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CinemasMoviesTimes_CinemaId",
                table: "CinemasMoviesTimes",
                column: "CinemaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CinemasMoviesTimes",
                table: "CinemasMoviesTimes");

            migrationBuilder.DropIndex(
                name: "IX_CinemasMoviesTimes_CinemaId",
                table: "CinemasMoviesTimes");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CinemasMoviesTimes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CinemasMoviesTimes",
                table: "CinemasMoviesTimes",
                columns: new[] { "CinemaId", "MovieId" });
        }
    }
}
