using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoriesContract
{
    public interface IGenreRepositories
    {
        Task<IEnumerable<Genre>> GetAllAsync();
        Task<Genre>? GetByIdAsync(int id);
        Task AddAsync(Genre genre);
        Task UpdateAsync(Genre genre);
        Task DeleteAsync(int id);
    }
}
