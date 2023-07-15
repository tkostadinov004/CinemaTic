using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinema.Data.Migrations
{
    public partial class ChangedColors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HeadingColor",
                table: "Cinemas",
                newName: "ButtonTextColor");

            migrationBuilder.AddColumn<string>(
                name: "AccentColor",
                table: "Cinemas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BoardColor",
                table: "Cinemas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccentColor",
                table: "Cinemas");

            migrationBuilder.DropColumn(
                name: "BoardColor",
                table: "Cinemas");

            migrationBuilder.RenameColumn(
                name: "ButtonTextColor",
                table: "Cinemas",
                newName: "HeadingColor");
        }
    }
}
