using System.ComponentModel.DataAnnotations;

namespace MyListed.API.DTOs;

public class RegisterAuthDto
{
    [EmailAddress]
    public string Email { get; set; }

    [MinLength(6)]
    public string Password { get; set; }

    [Compare("Password")]
    public string ConfirmPassword { get; set; }
}
