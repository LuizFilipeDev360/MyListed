using Microsoft.EntityFrameworkCore;
using MyListed.API.Data;
using MyListed.API.Models;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MyListed.API.Repository;

public class MediaRepository
{
    private MediaContext _context;

    public MediaRepository(MediaContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Media>> GetAllAsync()
    {
        var media = _context.Media.Include(m => m.MediaGenres).ThenInclude(mg => mg.Genre).AsQueryable();
        return await media.ToListAsync();
    }

    public async Task<Media?> GetByIdAsync(int id)
    {
        var media = await _context.Media.Include(m => m.MediaGenres).ThenInclude(mg => mg.Genre).FirstOrDefaultAsync(m => m.Id == id);
        return media;
    }

    public async Task<IEnumerable<Media>> GetByStringAsync(string s)
    {
        var query = _context.Media
        .Include(m => m.MediaGenres)
            .ThenInclude(mg => mg.Genre)
        .AsQueryable();

        if (!string.IsNullOrWhiteSpace(s))
        {
            query = query.Where(m =>
                EF.Functions.Like(m.Title, $"%{s}%"));
        }


        return await query.ToListAsync();
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

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
