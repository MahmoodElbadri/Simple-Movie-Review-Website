using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public sealed class ApplicationDbContext : IdentityDbContext<IdentityUser>, IDbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Seed Genres
            modelBuilder.Entity<Genre>().HasData(
                new Genre { GenreId = 1, Name = "Action" },
                new Genre { GenreId = 2, Name = "Drama" },
                new Genre { GenreId = 3, Name = "Comedy" }
            );

            // Seed Movies
            modelBuilder.Entity<Movie>().HasData(
                new Movie
                {
                    MovieId = 1,
                    Title = "Inception",
                    Description = "A mind-bending thriller by Christopher Nolan.",
                    GenreId = 1,  // Action
                    ReleaseDate = new DateTime(2010, 7, 16),
                    AverageRating = 4.8f
                },
                new Movie
                {
                    MovieId = 2,
                    Title = "The Godfather",
                    Description = "A classic mafia drama directed by Francis Ford Coppola.",
                    GenreId = 2,  // Drama
                    ReleaseDate = new DateTime(1972, 3, 24),
                    AverageRating = 4.9f
                },
                new Movie
                {
                    MovieId = 3,
                    Title = "The Hangover",
                    Description = "A comedy about a Las Vegas bachelor party gone wrong.",
                    GenreId = 3,  // Comedy
                    ReleaseDate = new DateTime(2009, 6, 5),
                    AverageRating = 4.5f
                }
            );

            // Seed Reviews (Optional)
            modelBuilder.Entity<Review>().HasData(
                new Review
                {
                    ReviewId = 1,
                    MovieId = 1,  // Inception
                    UserId = "testuser1",  // Replace with actual User ID later
                    Rating = 5,
                    Comment = "Amazing movie! A must-watch for sci-fi fans.",
                    CreatedAt = DateTime.Now
                },
                new Review
                {
                    ReviewId = 2,
                    MovieId = 2,  // The Godfather
                    UserId = "testuser2",  // Replace with actual User ID later
                    Rating = 5,
                    Comment = "An iconic masterpiece of cinema.",
                    CreatedAt = DateTime.Now
                }
            );
        }

        public DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            throw new NotImplementedException();
        }
    }
}
