using Application.Common.Interfaces;
using Application.Models;
using Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.Users.Accounts.Commands
{
    public class LoginCommand : IRequest<Result>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string? FirebaseToken { get; set; }
    }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, Result>
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IRtTokenService _rtTokenService;
        private readonly ILogger<LoginCommandHandler> _logger;

        public LoginCommandHandler(UserManager<User> userManager,
                                   SignInManager<User> signInManager,
                                   IRtTokenService rtTokenService,
                                   ILogger<LoginCommandHandler> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _rtTokenService = rtTokenService;
            _logger = logger;
        }

        public async Task<Result> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(request.Email);

                if (user == null)
                    return Result.Failure("Пользователь по данной почте не найден");

                var signInResult = await _signInManager.PasswordSignInAsync(user, request.Password, false, false);

                if (!signInResult.Succeeded)
                    return Result.Failure("Неправильный логин или пароль");

                string rToken = await _rtTokenService.GenerateRToken(user.Id);

                if (string.IsNullOrWhiteSpace(rToken))
                    return Result.Failure("Токен не найден.");

                var jwtToken = _rtTokenService.GenerateJwtToken(user);

                return Result.Success(new ApiUserToken(jwtToken, rToken));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "User login failed with error");
                return Result.Failure("Возникли ошибки при авторизации");
            }
        }
    }
}