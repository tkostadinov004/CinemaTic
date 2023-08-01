using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaTic.Data.Migrations
{
    public partial class CinemaRef : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CinemaId",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_CinemaId",
                table: "Tickets",
                column: "CinemaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Cinemas_CinemaId",
                table: "Tickets",
                column: "CinemaId",
                principalTable: "Cinemas",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Cinemas_CinemaId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_CinemaId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "CinemaId",
                table: "Tickets");
        }
    }
}
