using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaTic.Data.Migrations
{
    public partial class RemovedSeatsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Seats_SeatId",
                table: "Tickets");

            migrationBuilder.DropTable(
                name: "Seats");

            migrationBuilder.RenameColumn(
                name: "SeatId",
                table: "Tickets",
                newName: "SectorId");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_SeatId",
                table: "Tickets",
                newName: "IX_Tickets_SectorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Sectors_SectorId",
                table: "Tickets",
                column: "SectorId",
                principalTable: "Sectors",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Sectors_SectorId",
                table: "Tickets");

            migrationBuilder.RenameColumn(
                name: "SectorId",
                table: "Tickets",
                newName: "SeatId");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_SectorId",
                table: "Tickets",
                newName: "IX_Tickets_SeatId");

            migrationBuilder.CreateTable(
                name: "Seats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsOccupied = table.Column<bool>(type: "bit", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SeatNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sector = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seats", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Seats_SeatId",
                table: "Tickets",
                column: "SeatId",
                principalTable: "Seats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
