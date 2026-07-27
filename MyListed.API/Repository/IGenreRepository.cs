using Microsoft.EntityFrameworkCore;
using MyListed.API.Models;

namespace MyListed.API.Repository;

public interface IGenreRepository
{
    Task<IEnumerable<Genre>> GetAllAsync();

    Task<Genre?> GetByIdAsync(int id);
    Task<IEnumerable<Genre>> GetByStringAsync(string s);

    void Add(Genre genre);

    void Update(Genre genre);

    void Remove(Genre genre);

    Task SaveChangesAsync();
}
