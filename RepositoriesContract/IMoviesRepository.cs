using Entities;

namespace RepositoriesContract
{
    public interface IMoviesRepository
    {
        Task<IEnumerable<Movie>> GetAllAsync();
        Task<Movie>? GetByIdAsync(int id);
        Task AddAsync(Movie movie);
        Task UpdateAsync(Movie movie);
        Task DeleteAsync(int id);
    }
}
