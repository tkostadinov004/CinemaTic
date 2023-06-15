using Microsoft.EntityFrameworkCore.Migrations;

namespace Cinema.Data.Migrations
{
    public partial class ChangedUserRating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "UserRating",
                table: "Movies",
                type: "decimal(1)",
                precision: 1,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(1)",
                oldPrecision: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "UserRating",
                table: "Movies",
                type: "decimal(1)",
                precision: 1,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(1)",
                oldPrecision: 1,
                oldNullable: true);
        }
    }
}
