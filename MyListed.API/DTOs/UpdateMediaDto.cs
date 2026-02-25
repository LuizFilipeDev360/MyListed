using Microsoft.AspNetCore.Mvc.Formatters;
using MyListed.API.Models;
using System.ComponentModel.DataAnnotations;

namespace MyListed.API.DTOs;

public class UpdateMediaDto
{
    [Required(ErrorMessage = "O titulo do filme é obrigatório")]
    public string Title { get; set; }
    [Range(1000, 3000)]
    public int? Year { get; set; }
    public MediaKind Kind { get; set; }
    [MaxLength(1000)]
    public string? Description { get; set; }
    public List<int> GenreIds { get; set; }
}
