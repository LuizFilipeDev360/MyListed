using System.ComponentModel.DataAnnotations;

namespace MyListed.API.Models;

public class UserMedia
{
    public int MediaId { get; set; }
    public Media Media { get; set; }

    public string UserId { get; set; }
    public ApplicationUser User { get; set; }

    [Range(0, 10)]
    public int? Rating { get; set; }

    public string? Review { get; set; }

    public bool Watched { get; set; } = false;

    public bool Liked { get; set; } = false;
}
