using System.ComponentModel.DataAnnotations;

namespace MyListed.API.DTOs;

public class LoginAuthDto
{
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}
