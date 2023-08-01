using Microsoft.EntityFrameworkCore.Migrations;

namespace CinemaTic.Data.Migrations
{
    public partial class FixedTicket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_VisitorId1",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_VisitorId1",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "VisitorId1",
                table: "Tickets");

            migrationBuilder.AlterColumn<string>(
                name: "VisitorId",
                table: "Tickets",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_VisitorId",
                table: "Tickets",
                column: "VisitorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_VisitorId",
                table: "Tickets",
                column: "VisitorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_VisitorId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_VisitorId",
                table: "Tickets");

            migrationBuilder.AlterColumn<int>(
                name: "VisitorId",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VisitorId1",
                table: "Tickets",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_VisitorId1",
                table: "Tickets",
                column: "VisitorId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_VisitorId1",
                table: "Tickets",
                column: "VisitorId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
