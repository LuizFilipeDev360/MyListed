using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyListed.API.Models;

namespace MyListed.API.Data;

public class MediaContext : IdentityDbContext<ApplicationUser>
{
    public MediaContext(DbContextOptions<MediaContext> opts): base(opts)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MediaGenre>()
            .HasKey(mc => new { mc.MediaId, mc.GenreId });

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Media> Media { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<MediaGenre> MediaGenres { get; set; }
}
