using Microsoft.EntityFrameworkCore;
using MyListed.API.Data;
using MyListed.API.Models;
using System.Threading.Tasks;

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
        var media = _context.Media.Include(m => m.MediaGenres).ThenInclude(mg => mg.Genre).AsQueryable();
        return media;
    }

    public async Task<Media?> GetById(int id)
    {
        var media = await _context.Media.Include(m => m.MediaGenres).ThenInclude(mg => mg.Genre).FirstOrDefaultAsync(m => m.Id == id);
        return media;
    }

    public IEnumerable<Media> GetByString(string s)
    {
        s = s.ToLower();

        var query = _context.Media
        .Include(m => m.MediaGenres)
            .ThenInclude(mg => mg.Genre)
        .AsQueryable();

        if (!string.IsNullOrWhiteSpace(s))
        {
            query = query.Where(m =>
                EF.Functions.Like(m.Title.ToLower(), $"%{s}%"));
        }


        return query;
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
