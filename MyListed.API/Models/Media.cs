using System.ComponentModel.DataAnnotations;

namespace MyListed.API.Models;

public class Media
{
    [Key]
    [Required]
    public int Id { get; set; }
    [Required(ErrorMessage ="O titulo do filme é obrigatório")]
    public string Title { get; set; }
    [Range(1000, 3000)]
    public int Year { get; set; }
    public MediaKind Kind { get; set; }
    [MaxLength(1000)]
    public string Description { get; set; }
}
