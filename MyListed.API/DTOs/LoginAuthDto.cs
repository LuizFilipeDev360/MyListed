using System.ComponentModel.DataAnnotations;

namespace MyListed.API.DTOs;

public class LoginAuthDto
{
    [Required]
    public string User { get; set; }

    [Required]
    public string Password { get; set; }
}
