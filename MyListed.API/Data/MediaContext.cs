using Microsoft.EntityFrameworkCore;
using MyListed.API.Models;

namespace MyListed.API.Data;

public class MediaContext : DbContext
{
    public MediaContext(DbContextOptions<MediaContext> opts): base(opts)
    {
        
    }

    public DbSet<Media> Media { get; set; }
}
