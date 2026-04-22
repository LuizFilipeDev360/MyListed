using System.ComponentModel.DataAnnotations;

namespace MyListed.API.DTOs;

public class ReadUserMediaDto
{
    public int MediaId { get; set; }

    public string Titulo { get; set; }

    public string? ImageUrl { get; set; }

    [Range(0, 10)]
    public int? Rating { get; set; }

    public string? Review { get; set; }

    public bool Watched { get; set; } = false;

    public bool Liked { get; set; } = false;
}
