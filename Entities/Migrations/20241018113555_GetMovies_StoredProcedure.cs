using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class GetMovies_StoredProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sp_GetAllMovies = @"
            CREATE PROCEDURE GETALLMOVIES_FROM_VS
            AS
            BEGIN
                SELECT *
                FROM Movies
            END;
            ";
            migrationBuilder.Sql(sp_GetAllMovies);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string sp_GetAllMovies = @"
            DROP PROCEDURE GETALLMOVIES_WITH_NO_WHERE_CLAUSE_FROM_VS";
            migrationBuilder.Sql(sp_GetAllMovies);
        }
    }
}
