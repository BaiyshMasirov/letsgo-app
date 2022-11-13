using Application.Models;
using Domain.Enums;
using Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.Users.Accounts.Commands
{
    public class RegisterUserCommand : IRequest<Result>
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result>
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<RegisterUserCommandHandler> _logger;

        public RegisterUserCommandHandler(UserManager<User> userManager,
                                         ILogger<RegisterUserCommandHandler> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<Result> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existingUser = await _userManager.Users
                    .Where(x => x.UserName == request.Email || x.PhoneNumber == request.PhoneNumber)
                    .FirstOrDefaultAsync(cancellationToken);

                if (existingUser != null)
                    return Result.Failure("Пользователь с такой почтой или номером телефона уже сущетсвует");

                User user = new()
                {
                    UserName = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    MiddleName = request.MiddleName,
                    PhoneNumber = request.PhoneNumber,
                    Email = request.Email,
                    LockoutEnabled = false,
                    Status = UserStatus.Active,
                    IsAdmin = false
                };

                IdentityResult result = await _userManager.CreateAsync(user, request.Password);

                return result.Succeeded
                    ? Result.Success("Успешно зарегистрирован")
                    : Result.Failure(result.Errors.Select(x => x.Description).ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"User account create failed with error");
                return Result.Failure("Возникли ошибки при регистрации");
            }
        }
    }
}