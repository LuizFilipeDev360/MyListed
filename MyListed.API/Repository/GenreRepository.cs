using MyListed.API.Data;
using MyListed.API.Models;

namespace MyListed.API.Repository;

public class GenreRepository
{
    private MediaContext _context;

    public GenreRepository(MediaContext context)
    {
        _context = context;
    }

    public IEnumerable<Genre> GetAll()
    {
        return _context.Genres; 
    }

    public Genre? GetById(int id)
    {
        return _context.Genres.FirstOrDefault(m => m.Id == id);
    }

    public IEnumerable<Genre> GetByString(string s)
    {
        return _context.Genres.Where(m => m.Name.ToLower().Contains(s.ToLower()));
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

    public void SaveChanges()
    {
        _context.SaveChanges();
    }
}
