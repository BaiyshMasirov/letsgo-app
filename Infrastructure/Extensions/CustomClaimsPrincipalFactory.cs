using Application.Constants;
using Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace Infrastructure.Extensions
{
    public class CustomClaimsPrincipalFactory : UserClaimsPrincipalFactory<User, Role>
    {
        public CustomClaimsPrincipalFactory(UserManager<User> userManager,
                                            RoleManager<Role> roleManager,
                                            IOptions<IdentityOptions> optionsAccessor)
                                              : base(userManager, roleManager, optionsAccessor)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim(CustomClaimTypes.UserId, user.Id.ToString()));
            identity.AddClaim(new Claim(CustomClaimTypes.FullName, user.FullName));
            identity.AddClaim(new Claim(CustomClaimTypes.Email, user.Email)); 
            return identity;
        }
    }
}