using Microsoft.AspNetCore.Mvc.Formatters;
using MyListed.API.Models;
using System.ComponentModel.DataAnnotations;

namespace MyListed.API.DTOs;

public class CreateMediaDto
{
    [Required(ErrorMessage = "The film title is required.")]
    public string Title { get; set; }
    [Range(1000, 3000)]
    public int? Year { get; set; }
    public MediaKind Kind { get; set; } = 0;
    [MaxLength(1000)]
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public List<int> GenreIds { get; set; }
}
