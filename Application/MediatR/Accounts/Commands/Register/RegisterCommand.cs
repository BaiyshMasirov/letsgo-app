using Application.Models;
using Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.Accounts.Commands.Register
{
    public class RegisterCommand : IRequest<Result>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result>
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<RegisterCommandHandler> _logger;

        public RegisterCommandHandler(UserManager<User> userManager,
                                      ILogger<RegisterCommandHandler> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<Result> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existingUser = await _userManager.Users.Where(x => x.UserName == request.Email).FirstOrDefaultAsync(cancellationToken);

                if (existingUser != null)
                    return Result.Failure("Такой пользователь уже существует");

                User user = new()
                {
                    UserName = request.Name,
                    PhoneNumber = request.PhoneNumber,
                    Email = request.Email
                };

                IdentityResult result = await _userManager.CreateAsync(user, request.Password);

                /* string rToken = await _rtTokenService.GenerateRToken(user.Id);
                 if (string.IsNullOrWhiteSpace(rToken))
                     return Result.Failure("Не удалось сформировать токен");

                 var token = _rtTokenService.GenerateJwtToken(user);*/

                return Result.Success("Заявка отправлена, ожидайте подтверждения");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Account creation failed with error");
                return Result.Failure("Возникли ошибки при регистрации");
            }
        }
    }
}