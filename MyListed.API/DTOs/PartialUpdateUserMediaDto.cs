using System.ComponentModel.DataAnnotations;

namespace MyListed.API.DTOs;

public class PartialUpdateUserMediaDto
{
    [Range(0, 10)]
    public int? Rating { get; set; }

    public string? Review { get; set; }

    public bool? Watched { get; set; }

    public bool? Liked { get; set; }
}
