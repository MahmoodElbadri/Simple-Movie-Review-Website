using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class GetMoviesWithGenresAndReviews_SP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sp_GetAllMoviesWithGenresAndReviews = @"
                CREATE PROCEDURE GET_ALL_MOVIES_WITH_REVIEWS_AND_GENRE_FROM_VS
                AS
                BEGIN
	                SELECT M.MovieId, 
	                       M.Title,  
	                       M.ReleaseDate, 
	                       G.Name AS GenreName,
	                       R.ReviewId, 
	                       R.Comment, 
	                       R.Rating, 
	                       R.CreatedAt
	                FROM Movies M
	                INNER JOIN Genres G
		                ON M.GenreId = G.GenreId
	                LEFT JOIN Reviews R
		                ON M.MovieId = R.MovieId
                END;
                GO
                ";
            migrationBuilder.Sql(sp_GetAllMoviesWithGenresAndReviews);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string sp_GetAllMoviesWithGenresAndReviews = @"DROP GET_ALL_MOVIES_WITH_REVIEWS_AND_GENRE_FROM_VS";
            migrationBuilder.Sql(sp_GetAllMoviesWithGenresAndReviews);
        }
    }
}
