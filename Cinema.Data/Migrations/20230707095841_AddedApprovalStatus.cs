using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinema.Data.Migrations
{
    public partial class AddedApprovalStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Cinemas");

            migrationBuilder.AddColumn<int>(
                name: "ApprovalStatus",
                table: "Cinemas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovalStatus",
                table: "Cinemas");

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Cinemas",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
