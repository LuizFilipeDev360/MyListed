using System.ComponentModel.DataAnnotations;

namespace MyListed.API.Models;

public class Genre
{
    [Key]
    [Required]
    public int Id { get; set; }
    [Required(ErrorMessage = "O nome do gênero é obrigatório")]
    public string Name { get; set; }
}
