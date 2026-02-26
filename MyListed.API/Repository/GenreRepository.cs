using Microsoft.EntityFrameworkCore;
using MyListed.API.Data;
using MyListed.API.Models;
using System.Threading.Tasks;

namespace MyListed.API.Repository;

public class GenreRepository
{
    private MediaContext _context;

    public GenreRepository(MediaContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Genre>> GetAllAsync()
    {
        var genre =  _context.Genres;
        return await genre.ToListAsync();
    }

    public async Task<Genre?> GetByIdAsync(int id)
    {
        var genre =  await _context.Genres.FirstOrDefaultAsync(m => m.Id == id);
        return genre;
    }

    public async Task<IEnumerable<Genre>> GetByStringAsync(string s)
    {
        var query = _context.Genres.AsQueryable();

        if (!string.IsNullOrWhiteSpace(s))
        {
            query = query.Where(m =>
                EF.Functions.Like(m.Name, $"%{s}%"));
        }

        return await query.ToListAsync();
    }

    public void Add(Genre genre)
    {
        _context.Genres.Add(genre);
    }

    public void Update(Genre genre)
    {
        _context.Genres.Update(genre);
    }

    public void Remove(Genre genre)
    {
        _context.Genres.Remove(genre);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
