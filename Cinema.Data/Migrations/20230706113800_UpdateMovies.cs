using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinema.Data.Migrations
{
    public partial class UpdateMovies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CinemasMovies_Cinemas_CinemasId",
                table: "CinemasMovies");

            migrationBuilder.DropForeignKey(
                name: "FK_CinemasMovies_Movies_MoviesId",
                table: "CinemasMovies");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Movies");

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

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "CinemasMovies",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "MoviePrice",
                table: "CinemasMovies",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CinemasMovies_Cinemas_CinemaId",
                table: "CinemasMovies");

            migrationBuilder.DropForeignKey(
                name: "FK_CinemasMovies_Movies_MovieId",
                table: "CinemasMovies");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "CinemasMovies");

            migrationBuilder.DropColumn(
                name: "MoviePrice",
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

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Movies",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Movies",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

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
    }
}
