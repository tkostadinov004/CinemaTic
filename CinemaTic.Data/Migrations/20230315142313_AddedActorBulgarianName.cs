using Microsoft.EntityFrameworkCore.Migrations;

namespace CinemaTic.Data.Migrations
{
    public partial class AddedActorBulgarianName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BulgarianFullName",
                table: "Actors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BulgarianFullName",
                table: "Actors");
        }
    }
}
