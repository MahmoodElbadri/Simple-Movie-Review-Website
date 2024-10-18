using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class AddMovieWithReviewsAndGenreDto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MovieWithReviewsAndGenreDtos",
                columns: table => new
                {
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GenreName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "ReviewDto",
                columns: table => new
                {
                    ReviewId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<float>(type: "real", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 18, 16, 35, 56, 22, DateTimeKind.Local).AddTicks(6581));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 18, 16, 35, 56, 22, DateTimeKind.Local).AddTicks(6586));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieWithReviewsAndGenreDtos");

            migrationBuilder.DropTable(
                name: "ReviewDto");

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 18, 15, 37, 28, 303, DateTimeKind.Local).AddTicks(8554));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 18, 15, 37, 28, 303, DateTimeKind.Local).AddTicks(8564));
        }
    }
}
