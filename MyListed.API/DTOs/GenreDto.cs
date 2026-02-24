using System.ComponentModel.DataAnnotations;

namespace MyListed.API.DTOs;

public class GenreDto
{
    [Required(ErrorMessage = "O nome do gênero é obrigatório")]
    public string Name { get; set; }
}
