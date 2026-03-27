using MyListed.API.Data;
using MyListed.API.Models;
using Microsoft.EntityFrameworkCore;

namespace MyListed.API.Repository;

public class UserMediaRepository
{
    private MediaContext _context;

    public UserMediaRepository(MediaContext context)
    {
        _context = context;
    }

    public async Task<bool> ExistsAsync(string userId, int mediaId)
    {
        return await _context.UserMedia.AnyAsync(um => um.UserId == userId && um.MediaId == mediaId);
    }

    public async Task<IEnumerable<UserMedia>> GetAllAsync(string userId)
    {
        //return await _context.UserMedia.FirstOrDefaultAsync(um => um.UserId == userId && um.MediaId == mediaId);
        var userMedias = _context.UserMedia.Where(um => um.UserId == userId).Include(um => um.Media).Include(um => um.User);
        return await userMedias.ToListAsync();
    }

    public async Task<UserMedia?> GetByIdAsync(string userId,int mediaId)
    {
        var userMedia = await _context.UserMedia.Include(um => um.Media).Include(um => um.User).FirstOrDefaultAsync(um => um.MediaId == mediaId && um.UserId == userId);
        return userMedia;
    }

    public void Add(UserMedia userMedia)
    {
        _context.UserMedia.Add(userMedia);
    }

    public void Update(UserMedia userMedia)
    {
        _context.UserMedia.Update(userMedia);
    }

    public void Remove(UserMedia userMedia)
    {
        _context.UserMedia.Remove(userMedia);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

}
