using Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Domain.Identity
{
    public class User : IdentityUser<Guid>
    {
        public string AvatarPath { get; set; }

        public UserStatus Status { get; set; }
    }
}