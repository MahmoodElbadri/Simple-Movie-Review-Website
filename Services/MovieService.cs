using Entities;
using Microsoft.Extensions.Logging;
using RepositoriesContract;
using ServiceContracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public class MovieService : IMovieService
    {
        private readonly IMoviesRepository _repo;
        private readonly ILogger<MovieService> _logger;

        public MovieService(IMoviesRepository repo, ILogger<MovieService> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task AddMovieAsync(Movie movie)
        {
            _logger.LogInformation("Adding movie");
            await _repo.AddAsync(movie);
            _logger.LogInformation("Movie added");
        }

        public async Task DeleteMovieAsync(int id)
        {
            _logger.LogWarning("Deleting movie");
            await _repo.DeleteAsync(id);
            _logger.LogWarning("Movie deleted");
        }

        public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
        {
            _logger.LogInformation("Getting all movies");
            return await _repo.GetAllAsync();
        }

        public async Task<Movie?> GetMovieByIdAsync(int id)
        {
            _logger.LogInformation("Getting movie by id");
            return await _repo.GetByIdAsync(id);
        }

        public async Task UpdateMovieAsync(Movie movie)
        {
            _logger.LogInformation("Updating movie");
            await _repo.UpdateAsync(movie);
            _logger.LogInformation("Movie updated");
        }
    }
}
