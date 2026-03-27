using Microsoft.AspNetCore.Identity;

namespace MyListed.API.Models;

public class ApplicationUser : IdentityUser
{
        public DateTime CreatedAt { get; set; }

        public ApplicationUser()
        {
            CreatedAt = DateTime.UtcNow;
        }

    public ICollection<UserMedia>? UserMedia { get; set; } = new List<UserMedia>();
}
