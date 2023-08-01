using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaTic.Data.Migrations
{
    public partial class FixCinema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CinemasMovies_Cinemas_CinemaId",
                table: "CinemasMovies");

            migrationBuilder.DropForeignKey(
                name: "FK_CinemasMovies_Movies_MovieId",
                table: "CinemasMovies");

            migrationBuilder.RenameColumn(
                name: "MovieId",
                table: "CinemasMovies",
                newName: "MoviesId");

            migrationBuilder.RenameColumn(
                name: "CinemaId",
                table: "CinemasMovies",
                newName: "CinemasId");

            migrationBuilder.RenameIndex(
                name: "IX_CinemasMovies_MovieId",
                table: "CinemasMovies",
                newName: "IX_CinemasMovies_MoviesId");

            migrationBuilder.AddForeignKey(
                name: "FK_CinemasMovies_Cinemas_CinemasId",
                table: "CinemasMovies",
                column: "CinemasId",
                principalTable: "Cinemas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CinemasMovies_Movies_MoviesId",
                table: "CinemasMovies",
                column: "MoviesId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CinemasMovies_Cinemas_CinemasId",
                table: "CinemasMovies");

            migrationBuilder.DropForeignKey(
                name: "FK_CinemasMovies_Movies_MoviesId",
                table: "CinemasMovies");

            migrationBuilder.RenameColumn(
                name: "MoviesId",
                table: "CinemasMovies",
                newName: "MovieId");

            migrationBuilder.RenameColumn(
                name: "CinemasId",
                table: "CinemasMovies",
                newName: "CinemaId");

            migrationBuilder.RenameIndex(
                name: "IX_CinemasMovies_MoviesId",
                table: "CinemasMovies",
                newName: "IX_CinemasMovies_MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_CinemasMovies_Cinemas_CinemaId",
                table: "CinemasMovies",
                column: "CinemaId",
                principalTable: "Cinemas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CinemasMovies_Movies_MovieId",
                table: "CinemasMovies",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
