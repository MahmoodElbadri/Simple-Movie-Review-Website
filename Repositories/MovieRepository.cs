﻿using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RepositoriesContract;

namespace Repositories
{
    public class MovieRepository : IMoviesRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<MovieRepository> _logger;
        public MovieRepository(ApplicationDbContext db, ILogger<MovieRepository> logger)
        {
            _db = db;
            _logger = logger;
        }
        public async Task AddAsync(Movie movie)
        {
            _logger.LogInformation("Adding movie");
            await _db.Movies.AddAsync(movie);
            await _db.SaveChangesAsync();
            _logger.LogInformation("Movie added");
        }

        public async Task DeleteAsync(int id)
        {
            _logger.LogWarning("Deleting movie");
            Movie? movie = await GetByIdAsync(id);
            if (movie == null)
            {
                _logger.LogWarning($"Movie with id {id} not found");
                return;
            }
            try
            {
                _db.Remove(movie);
                await _db.SaveChangesAsync();
                _logger.LogInformation("Movie deleted");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the movie");
                throw;
            }
        }


        public async Task<IEnumerable<Movie>> GetAllAsync()
        {
            _logger.LogInformation("Getting all movies");
            return await _db.Movies.Include("Genre").Include("Reviews"). ToListAsync();
        }

        public async Task<Movie>? GetByIdAsync(int id)
        {
            _logger.LogInformation($"Getting movie with id: {id}");
            Movie? movie = await _db.Movies.FirstOrDefaultAsync(tmp => tmp.MovieId == id);
            _logger.LogInformation(movie == null ? "Movie not found" : "Movie found");
            return movie;
        }

        public async Task UpdateAsync(Movie movie)
        {
            _logger.LogInformation($"Updating movie with id: {movie.MovieId}");
            Movie? existMovie = await GetByIdAsync(movie.MovieId);
            try
            {
                if (existMovie != null)
                {
                    _db.Movies.Update(movie);
                    await _db.SaveChangesAsync();
                    _logger.LogInformation("Movie updated");
                }
                else
                {
                    _logger.LogWarning("Movie not found");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the movie");
                throw;
            }
        }
    }
}