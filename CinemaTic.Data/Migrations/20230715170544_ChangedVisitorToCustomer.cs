using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaTic.Data.Migrations
{
    public partial class ChangedVisitorToCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_VisitorId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersMovies_AspNetUsers_UserId",
                table: "UsersMovies");

            migrationBuilder.DropTable(
                name: "VisitorsCinemas");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UsersMovies",
                newName: "CustomerId");

            migrationBuilder.RenameColumn(
                name: "VisitorId",
                table: "Tickets",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_VisitorId",
                table: "Tickets",
                newName: "IX_Tickets_CustomerId");

            migrationBuilder.CreateTable(
                name: "CustomersCinemas",
                columns: table => new
                {
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CinemaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomersCinemas", x => new { x.CustomerId, x.CinemaId });
                    table.ForeignKey(
                        name: "FK_CustomersCinemas_AspNetUsers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomersCinemas_Cinemas_CinemaId",
                        column: x => x.CinemaId,
                        principalTable: "Cinemas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomersCinemas_CinemaId",
                table: "CustomersCinemas",
                column: "CinemaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_CustomerId",
                table: "Tickets",
                column: "CustomerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersMovies_AspNetUsers_CustomerId",
                table: "UsersMovies",
                column: "CustomerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_CustomerId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersMovies_AspNetUsers_CustomerId",
                table: "UsersMovies");

            migrationBuilder.DropTable(
                name: "CustomersCinemas");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "UsersMovies",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Tickets",
                newName: "VisitorId");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_CustomerId",
                table: "Tickets",
                newName: "IX_Tickets_VisitorId");

            migrationBuilder.CreateTable(
                name: "VisitorsCinemas",
                columns: table => new
                {
                    VisitorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CinemaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitorsCinemas", x => new { x.VisitorId, x.CinemaId });
                    table.ForeignKey(
                        name: "FK_VisitorsCinemas_AspNetUsers_VisitorId",
                        column: x => x.VisitorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VisitorsCinemas_Cinemas_CinemaId",
                        column: x => x.CinemaId,
                        principalTable: "Cinemas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_VisitorsCinemas_CinemaId",
                table: "VisitorsCinemas",
                column: "CinemaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_VisitorId",
                table: "Tickets",
                column: "VisitorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersMovies_AspNetUsers_UserId",
                table: "UsersMovies",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
