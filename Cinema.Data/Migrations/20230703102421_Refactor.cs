using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinema.Data.Migrations
{
    public partial class Refactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActorsMovies_Actors_ActorId",
                table: "ActorsMovies");

            migrationBuilder.DropForeignKey(
                name: "FK_ActorsMovies_Movies_MovieId",
                table: "ActorsMovies");

            migrationBuilder.RenameColumn(
                name: "MovieId",
                table: "ActorsMovies",
                newName: "MoviesId");

            migrationBuilder.RenameColumn(
                name: "ActorId",
                table: "ActorsMovies",
                newName: "ActorsId");

            migrationBuilder.RenameIndex(
                name: "IX_ActorsMovies_MovieId",
                table: "ActorsMovies",
                newName: "IX_ActorsMovies_MoviesId");

            migrationBuilder.AddColumn<string>(
                name: "AddedById",
                table: "Movies",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Director",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_AddedById",
                table: "Movies",
                column: "AddedById");

            migrationBuilder.AddForeignKey(
                name: "FK_ActorsMovies_Actors_ActorsId",
                table: "ActorsMovies",
                column: "ActorsId",
                principalTable: "Actors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ActorsMovies_Movies_MoviesId",
                table: "ActorsMovies",
                column: "MoviesId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_AspNetUsers_AddedById",
                table: "Movies",
                column: "AddedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActorsMovies_Actors_ActorsId",
                table: "ActorsMovies");

            migrationBuilder.DropForeignKey(
                name: "FK_ActorsMovies_Movies_MoviesId",
                table: "ActorsMovies");

            migrationBuilder.DropForeignKey(
                name: "FK_Movies_AspNetUsers_AddedById",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_AddedById",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "AddedById",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Director",
                table: "Movies");

            migrationBuilder.RenameColumn(
                name: "MoviesId",
                table: "ActorsMovies",
                newName: "MovieId");

            migrationBuilder.RenameColumn(
                name: "ActorsId",
                table: "ActorsMovies",
                newName: "ActorId");

            migrationBuilder.RenameIndex(
                name: "IX_ActorsMovies_MoviesId",
                table: "ActorsMovies",
                newName: "IX_ActorsMovies_MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActorsMovies_Actors_ActorId",
                table: "ActorsMovies",
                column: "ActorId",
                principalTable: "Actors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ActorsMovies_Movies_MovieId",
                table: "ActorsMovies",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
