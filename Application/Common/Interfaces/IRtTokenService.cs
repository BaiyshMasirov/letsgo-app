using Domain.Identity;

namespace Application.Common.Interfaces
{
    public interface IRtTokenService
    {
        Task<string> GenerateRToken(Guid userId);

        Task<string> RefreshToken(Guid userId, string refreshToken);

        Task StopRefreshToken(Guid userId);

        string GenerateJwtToken(User user);
    }
}