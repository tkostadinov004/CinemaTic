using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinema.Data.Migrations
{
    public partial class ChangedPriceName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MoviePrice",
                table: "CinemasMovies",
                newName: "TicketPrice");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TicketPrice",
                table: "CinemasMovies",
                newName: "MoviePrice");
        }
    }
}
