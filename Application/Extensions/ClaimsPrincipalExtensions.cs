using System.Security.Claims;

namespace Application.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static T GetLoggedInUserId<T>(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            var loggedInUserId = principal.FindFirstValue(ClaimTypes.NameIdentifier);

            if (typeof(T) == typeof(string))
            {
                return (T)Convert.ChangeType(loggedInUserId, typeof(T));
            }
            else if (typeof(T) == typeof(Guid))
            {
                return !string.IsNullOrWhiteSpace(loggedInUserId) ? (T)Convert.ChangeType(new Guid(loggedInUserId), typeof(T)) : (T)Convert.ChangeType(Guid.Empty, typeof(T));
            }
            else if (typeof(T) == typeof(int) || typeof(T) == typeof(long))
            {
                return !string.IsNullOrWhiteSpace(loggedInUserId) ? (T)Convert.ChangeType(loggedInUserId, typeof(T)) : (T)Convert.ChangeType(0, typeof(T));
            }
            else
            {
                throw new Exception("Invalid type provided");
            }
        }

        public static string GetLoggedInUserName(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindFirstValue(ClaimTypes.Name);
        }

        public static string GetLoggedInUserEmail(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindFirstValue(ClaimTypes.Email);
        }

        public static T GetClaimValue<T>(this ClaimsPrincipal principal, string claimType)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            var claimValue = principal.FindFirstValue(claimType);

            if (typeof(T) == typeof(string))
            {
                return (T)Convert.ChangeType(claimValue, typeof(T));
            }
            else if (typeof(T) == typeof(Guid?))
            {
                return !string.IsNullOrWhiteSpace(claimValue) ? (T)Convert.ChangeType(new Guid(claimValue), typeof(T)) : (T)Convert.ChangeType(null, typeof(T));
            }
            else if (typeof(T) == typeof(Guid))
            {
                return !string.IsNullOrWhiteSpace(claimValue) ? (T)Convert.ChangeType(new Guid(claimValue), typeof(T)) : (T)Convert.ChangeType(Guid.Empty, typeof(T));
            }
            else if (typeof(T) == typeof(int) || typeof(T) == typeof(long))
            {
                return !string.IsNullOrWhiteSpace(claimValue) ? (T)Convert.ChangeType(claimValue, typeof(T)) : (T)Convert.ChangeType(0, typeof(T));
            }
            else if (typeof(T) == typeof(bool))
            {
                return !string.IsNullOrWhiteSpace(claimValue) ? (T)Convert.ChangeType(claimValue, typeof(T)) : (T)Convert.ChangeType(false, typeof(T));
            }
            else if (typeof(T) == typeof(object))
            {
                return (T)Convert.ChangeType(claimValue, typeof(T));
            }
            else
            {
                throw new InvalidCastException($"Invalid type provided {typeof(T)}");
            }
        }
    }
}