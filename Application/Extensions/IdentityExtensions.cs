using Application.Models;
using Microsoft.AspNetCore.Identity;

namespace Application.Extensions
{
    public static class IdentityExtensions
    {
        public static Result ToApplicationResult(this IdentityResult result, string message = null)
        {
            return result.Succeeded
                ? Result.Success()
                : Result.Failure(result.Errors.Select(e => e.Description).ToArray());
        }
    }
}