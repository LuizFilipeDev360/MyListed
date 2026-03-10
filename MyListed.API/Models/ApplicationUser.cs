using Microsoft.AspNetCore.Identity;

namespace MyListed.API.Models;

public class ApplicationUser : IdentityUser
{
        public DateTime CreatedAt { get; set; }
}
