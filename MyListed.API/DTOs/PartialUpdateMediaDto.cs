using MyListed.API.Models;
using System.ComponentModel.DataAnnotations;

namespace MyListed.API.DTOs;

public class PartialUpdateMediaDto
{
    public string? Title { get; set; }
    public int? Year { get; set; }
    public MediaKind? Kind { get; set; }
    [MaxLength(1000)]
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public List<int>? GenreIds { get; set; }
}
