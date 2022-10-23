using Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Domain.Identity
{
    public class User : IdentityUser<Guid>
    {
        public string AvatarPath { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public string FullName => $"{LastName} {FirstName} {MiddleName}";

        public bool IsAdmin { get; set; }

        public UserStatus Status { get; set; }
    }
}