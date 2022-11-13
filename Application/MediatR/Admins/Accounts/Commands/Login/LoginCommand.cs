using Application.Models;
using Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.Admins.Accounts.Commands
{
    public class LoginCommand : IRequest<Result>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, Result>
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<LoginCommandHandler> _logger;

        public LoginCommandHandler(UserManager<User> userManager,
                                   SignInManager<User> signInManager,
                                   ILogger<LoginCommandHandler> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        public async Task<Result> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var admin = await _userManager.FindByEmailAsync(request.Email);

                if (admin == null)
                    return Result.Failure("Пользователь по данной почте не найден");                  

                var result = await _signInManager.PasswordSignInAsync(admin, request.Password, request.RememberMe, false);

                return result.Succeeded
                    ? Result.Success("Вы успешно вошли в систему")
                    : Result.Failure("Не удалось авторизоваться");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Admin login failed with error");
                return Result.Failure("Произошли ошибки при авторизации");
            }
        }
    }
}