using System.ComponentModel.DataAnnotations;

namespace MyListed.API.DTOs;

public class ReadUserDto
{
    public string UserName { get; set; }

    public string Email { get; set; }

    public DateTime CreatedAt { get; set; }
}
