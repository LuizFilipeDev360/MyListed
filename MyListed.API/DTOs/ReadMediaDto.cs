using Microsoft.AspNetCore.Mvc.Formatters;
using MyListed.API.Models;

namespace MyListed.API.DTOs;

public class ReadMediaDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int? Year { get; set; }
    public MediaKindDto Kind { get; set; }
    public string? Description { get; set; }
}
