using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyListed.API.Models;

namespace MyListed.API.Data;

public class MediaContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
{
    public MediaContext(DbContextOptions<MediaContext> opts): base(opts)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MediaGenre>()
            .HasKey(mg => new { mg.MediaId, mg.GenreId });

        modelBuilder.Entity<UserMedia>()
            .HasKey(um => new { um.UserId, um.MediaId });

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Media> Media { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<MediaGenre> MediaGenres { get; set; }
    public DbSet<UserMedia> UserMedia { get; set; }
}
