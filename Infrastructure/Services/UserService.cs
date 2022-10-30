using Application.Common.Interfaces;
using Application.Constants;
using Application.Extensions;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        public Guid UserId { get; }

        public string FullName { get; }

        public string Email { get; }

        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            ClaimsPrincipal principal = httpContextAccessor?.HttpContext?.User;
            if (principal == null)
                throw new NullReferenceException($"{nameof(UserService)} - httpContextAccessor is NULL.");

            UserId = principal.GetClaimValue<Guid>(CustomClaimTypes.UserId);
            FullName = principal.GetClaimValue<string>(CustomClaimTypes.FullName);
            Email = principal.GetClaimValue<string>(CustomClaimTypes.Email);
        }
    }
}