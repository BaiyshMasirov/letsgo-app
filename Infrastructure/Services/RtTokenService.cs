using Application.Common.Interfaces;
using Application.Models;
using Domain.Entities;
using Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Services
{
    public class RtTokenService : IRtTokenService
    {
        private readonly IApplicationEFContext _context;
        private readonly JwtSettings _jwtSettings;

        public RtTokenService(IApplicationEFContext context, IOptions<JwtSettings> options)
        {
            _context = context;
            _jwtSettings = options.Value;
        }

        public async Task<string> GenerateRToken(Guid userId)
        {
            var refreshToken = Guid.NewGuid().ToString().Replace("-", "");

            var rToken = new RToken
            {
                UserId = userId,
                RefreshToken = refreshToken,
                IsStop = false
            };

            _context.RTokens.Add(rToken);
            await _context.SaveChangesAsync(new CancellationToken());

            return refreshToken;
        }

        public async Task<string> RefreshToken(Guid userId, string refreshToken)
        {
            var rToken = await _context.RTokens.FirstOrDefaultAsync(rt => rt.UserId == userId && rt.RefreshToken == refreshToken);

            if (rToken == null || rToken.IsStop)
                return null;

            await _context.BeginTransactionAsync();

            rToken.IsStop = true;

            _context.RTokens.Update(rToken);

            var newRefreshToken = Guid.NewGuid().ToString().Replace("-", "");

            _context.RTokens.Add(new RToken
            {
                UserId = userId,
                RefreshToken = newRefreshToken,
                IsStop = false
            });

            await _context.CommitTransactionAsync();

            return newRefreshToken;
        }

        public async Task StopRefreshToken(Guid userId)
        {
            var rToken = await _context.RTokens.FirstOrDefaultAsync(rt => rt.UserId == userId && !rt.IsStop);
            if (rToken != null)
            {
                rToken.IsStop = true;

                _context.RTokens.Update(rToken);
                await _context.SaveChangesAsync(new CancellationToken());
            }
        }

        public string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _jwtSettings.Issuer,
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}