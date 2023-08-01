using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinema.Data.Migrations
{
    public partial class MadeGenreOptional : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Genres_GenreId",
                table: "Movies");

            migrationBuilder.AlterColumn<int>(
                name: "GenreId",
                table: "Movies",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "156fc675-02de-4250-9edb-869c85e13e61",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEAJKbXNmZ6Kq5Aq+2Ap36XSG1HHmjmaOqVSIES6Se3x2j1pzhNpw936voepWBYk8Dw==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "191d6e78-88fb-40ce-b85c-d8bcf4a1ae4c",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAENoKtOuGN9zHhhmWQsrWdtWxLSuutNYoVqR6mvvBd97UBmF1cAEBxLixusBvYEkZug==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1c850a33-6e0a-4c03-bb2d-c5a388042364",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAELeEBikUXJkMSqK+8M7I1TFC9tU1kHOTCbT3FLUdlsuuFtTGi+xg1isielHDj92GYg==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2055e8c8-5a8e-49c3-9f0a-a987700af2ee",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEKyXVEkYiitu/g4nxJzPhgP+b1zOMe5sKZgWyYb4ivr3QESMx++GwyRl9XdXN/zyjg==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "218dcf68-aa10-4c63-994f-50853fb19296",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEMM4s/8yxlKO0HUvakBulpawkq1im5l712Su+rA515XeIJN3Q2GhpDBNRfvr8ezqUQ==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2a8f5f5c-e539-4868-837b-9a19852a904e",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEPf8ikx9O1fm44G5VTFu5bA5q0uofddhSGDPeBo4O+fzMbny3dTaiQ4lnrP66+yfgA==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2f09c66b-0830-4fcc-8a0f-f29b0990c669",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAED+bqQEOxUqrepjmbG3OSF0fMvGZuJ2NUweHquJ/EOCZkFJTFFhZ4rk2jWuI4d8bAw==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4634669c-c5ad-41e6-8b41-f1524c9654ad",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEC4NKJug/G9yHOv6hMjYJLjNCXhH5kMhzFln6zkbKJblPnaS1eNt6UkTK8S+43RwCA==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5556c45e-395d-402b-b765-750666b092fc",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEKHPnUk8n54S6MaqM9pPhE0AStNZ9i2YV44kqZXf4s3G5ZwEU693lVF9gjkaFKZ6Fg==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "60223fcf-5fa4-434f-a4ba-9389a4f571a0",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEHXIfLGu12AwfJetgS7pnkoC1sTguD+UkQ3D3rH90fBw/0Lrq/KR9k0yAhdf1gLzfA==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "610ab053-2c5a-451b-9634-03b59ea4a473",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAECtFZ97TVshZqY7cQ8zSjtRGPZVE/DO1xYF3FtGtR6rA8JdiIOPmjJfW3eOcWaSXQQ==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "64ca1994-bfd0-4d26-8ec4-4d1bc82bd95c",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEJrrvpfG8+DyZjTX9Q+ASVZeAR1CjLy+B3h3CtmaPXeszHOD2+1D7SnrvZVlNhP2Gg==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8980e4ca-2628-490d-840a-9c9414ab9f33",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEK6CwLxVkn69B8Oug1b68FFdTeazYA/oOPd0sU4/sqXAdxERpps7E1DxcTZ1k5ITnw==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "96256cfb-df20-4a1f-8898-f06f634a17d7",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEMLsRhqghauwGYcJ2klGv8ZFRznCQvyyKvvCfE/QuD5JwuONSomB3R+IvrmcFgsoog==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a9b6dc74-38a5-4794-a703-59204f461adb",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAECwyiTlvhWXzOypYwVpmeIWhUgE8p4SuYTZ9a2/fS3DsyqXT9sZGYcmgd5cflIWhog==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bfa19e5f-4529-4276-bde8-8e6d3de2c423",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEBbqZ60rT6iDMOl0kByAniZCDU9skJzfmVWgLBGRVc8fZ58d5ASQZxnQfkfC2fbcpw==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c0102fde-8991-4e2a-bfca-cdda51f13a66",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEAkQXuHgg8JuVWHp4dLcIoBeUEPcXBzqbADycF9I3AKFt70vfsrT4TNPpq8VD1dSew==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c21bf410-3e22-4720-b01a-f2d91191a222",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEG8FqqlgyF0SuV4gnxFzIiQ9oBwZnzgIumUSRdK9f/NEZQEFWTl/iRN9VUJ3XuQ1kA==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e7d88cb7-a424-4795-8965-17273642b773",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAENourNW0VmlkK2hKaA+MnYXIeaOFXoS05TRMrQlPlVoOFz+GzE7QmTJk44gKXCIaYg==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ea811464-0c63-4c45-ac26-d4eb5bff334f",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEDSv9nI24IYA8Pln52jsWZqd5Lga7FW+UHC7hWGLS3EyIVqjFOzjoJgirDAiNm0y/w==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f338b628-feaf-4a03-95ad-defb7aec5c83",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEIfhE0EgrCFdmAHxbmVPV+k5nWh4adVwJWHVJfUabmnHPeVZI9Vz9yroFzR40w2YjQ==");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Genres_GenreId",
                table: "Movies",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Genres_GenreId",
                table: "Movies");

            migrationBuilder.AlterColumn<int>(
                name: "GenreId",
                table: "Movies",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "156fc675-02de-4250-9edb-869c85e13e61",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEIYdVbgmiuz1BnPMRPrPu3EWq3UfLAIBF/tE/nqG5aVgINEYeX46MDFw9GX/+zTjbA==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "191d6e78-88fb-40ce-b85c-d8bcf4a1ae4c",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEKVwKPAZTataE6hIw84BvWlq6cRTXNnlYt0oRepxQYt1bFUISOJcnKNmacLNL0/P2g==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1c850a33-6e0a-4c03-bb2d-c5a388042364",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEHub9LhSfEjuZ5a4xxiIgGqYM1XZ6m2MPJHl/HKNRxB+ZFTWXWCPLx+GXy7Wswqpwg==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2055e8c8-5a8e-49c3-9f0a-a987700af2ee",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEH9590bWETDx/f3eWFDK/nhUYCcO8ZbMJXB9tSzrfdzUrkH5TmnuYQdSW6Tdnpiz8g==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "218dcf68-aa10-4c63-994f-50853fb19296",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEEy1rD0+YNdrKjS3xMbXk1lAMo4PeS0kUf3GRRZ7Niljy26EwB2xbrd4AyoaruRv1A==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2a8f5f5c-e539-4868-837b-9a19852a904e",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEEUPcUZDJ4o+uRZ0A8Cb23yeQlCobllpj97wK5CQ1AYKes43jUtgQQOL3iUP/8dLRA==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2f09c66b-0830-4fcc-8a0f-f29b0990c669",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEFoeANuh9F5HuucSWm4m5cGI2ERKR+/bvmS7aU2wBfCoosApuSja/Er97O3eqbVnjA==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4634669c-c5ad-41e6-8b41-f1524c9654ad",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEAo9riMnRZd7BiDCRw9CxwHhPgDnbDHhLNkNUwtmA8gNMfYUX0M8lgLpLNB3UcbE4g==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5556c45e-395d-402b-b765-750666b092fc",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAECPxWkbe+LEWvSxClQyskMjLrUqyizhNP/IuupgOAbRB5OzPXG6j3aad9BtDl6p/Tg==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "60223fcf-5fa4-434f-a4ba-9389a4f571a0",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEBhX8g5FB07aDTi3JuRA4LAWhNK10GLfc5KvUV7nArXVbX+ZXsVHK9f5MNevdqR2/Q==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "610ab053-2c5a-451b-9634-03b59ea4a473",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEOV6ksAqnvTwcMf9wrhLoILqSvPmpNgRNYls1agDYXWc6sCnHx6cKNnCFskAZNq0kw==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "64ca1994-bfd0-4d26-8ec4-4d1bc82bd95c",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEBDfow5xrqGsBjw4oodC5stWB3lf6bHEa44w1/px+t0neX4lJzTB2S+M65YBoqIF4Q==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8980e4ca-2628-490d-840a-9c9414ab9f33",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEJTvPltEGNBIqB+sQZAvHOg3Suo85ro5WE8BON5I1PHgNDNtkVpj5hRWKAdtWbXwRA==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "96256cfb-df20-4a1f-8898-f06f634a17d7",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAELy8a4IThPWS/VOyJET02THwwzV+aw15jhnnDjBcvPHGkZpnOsJoQ6ZSECVCsZVPOw==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a9b6dc74-38a5-4794-a703-59204f461adb",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEB/axwPztarbXOMlDcO7bUMnqAU8pd8PrwN32w0RgLY3NloSHEXnd6c2I1Wc4Cqtwg==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bfa19e5f-4529-4276-bde8-8e6d3de2c423",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEBonXqX3UdNpBwptGunJ+/d+t5T9Fw+8YYjRCDdvtotgaxR8xdKEtpZY4LemicGxjw==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c0102fde-8991-4e2a-bfca-cdda51f13a66",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEMYGdB9gV3gxxqBhZBNZXMOwdADmqVUhS+jgVgD+/ez2eYFf3i2KI2Nbd5mbyuX7Uw==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c21bf410-3e22-4720-b01a-f2d91191a222",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEGuiVM+Ma5X9ORwziUxZo5qk0ba5xKIn2/QzkCAhEh7Q7wx3E4B5ZKaBaU0Tn9qu4g==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e7d88cb7-a424-4795-8965-17273642b773",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAELPednz6bhPDwj2u8h47E3tqdVUff4XB30zNBp3DBATdQxClHkm2MGBFDMUpOFpfjA==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ea811464-0c63-4c45-ac26-d4eb5bff334f",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEGBcz2Nu5L5I2hjU2fets75haPy65WClET7KODy7NKRSpaBc0Sf3UJH4YsT2ULw4aw==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f338b628-feaf-4a03-95ad-defb7aec5c83",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEFygr789cCFas2axywXM6reY2oV+84t0EuopGJ50lT/7VbrohPXm+alh2v5Ogv3l9A==");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Genres_GenreId",
                table: "Movies",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
