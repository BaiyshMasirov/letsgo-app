using Application.Constants;
using Domain.Enums;
using Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Seeds
{
    internal static class AdminUserSeed
    {
        internal static ModelBuilder AddApplicationUserSeedData(this ModelBuilder builder)
        {
            var user =
                new User
                {
                    Id = new Guid("794A3441-6CDA-47A2-B194-7422CF5A9467"),
                    FirstName = "Admin",
                    LastName = "Adminov",
                    MiddleName = "Adminovich",
                    UserName = "Admin",
                    NormalizedUserName = "Admin".ToUpper(),
                    Email = "admin@mail.test",
                    NormalizedEmail = "admin@mail.test".ToUpper(),
                    SecurityStamp = "0382afaf-aeae-47ef-983d-c194ba94c64e",
                    ConcurrencyStamp = "c94b51e5-52f3-4a06-a91b-f22a1588f9a4",
                    PhoneNumber = "0556646400",
                    Status = UserStatus.Active, 
                    IsAdmin = true,
                };

            user.PasswordHash = new PasswordHasher<User>().HashPassword(user, "12345678");

            var role =
                new Role
                {
                    Id = new Guid("EEF47149-D7D1-4296-AE0E-3FF6421598EC"),
                    Name = Roles.SUPER_ADMIN,
                    NormalizedName = Roles.SUPER_ADMIN.ToUpper(),
                    ConcurrencyStamp = "41DB063E-C8F8-437D-917C-72C0AC4EBB90"
                };

            builder.Entity<User>().HasData(user);
            builder.Entity<Role>().HasData(role);
            builder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                UserId = user.Id,
                RoleId = role.Id
            });

            return builder;
        }
    }
}