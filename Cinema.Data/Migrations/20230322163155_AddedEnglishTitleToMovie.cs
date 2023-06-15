using Microsoft.EntityFrameworkCore.Migrations;

namespace Cinema.Data.Migrations
{
    public partial class AddedEnglishTitleToMovie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Movies",
                newName: "EnglishTitle");

            migrationBuilder.AddColumn<string>(
                name: "BulgarianTitle",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BulgarianTitle",
                table: "Movies");

            migrationBuilder.RenameColumn(
                name: "EnglishTitle",
                table: "Movies",
                newName: "Title");
        }
    }
}
