using MyListed.API.Data;
using MyListed.API.Models;

namespace MyListed.API.Repository;

public class MediaRepository
{
    private MediaContext _context;

    public MediaRepository(MediaContext context)
    {
        _context = context;
    }

    public IEnumerable<Media> GetAll()
    {
        return _context.Media; 
    }

    public Media? GetById(int id)
    {
        return _context.Media.FirstOrDefault(m => m.Id == id);
    }

    public IEnumerable<Media> GetByString(string s)
    {
        return _context.Media.Where(m => m.Title.ToLower().Contains(s.ToLower()));
    }

    public void Add(Media media)
    {
        _context.Media.Add(media);
    }

    public void Update(Media media)
    {
        _context.Media.Update(media);
    }

    public void Remove(Media media)
    {
        _context.Media.Remove(media);
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }
}
