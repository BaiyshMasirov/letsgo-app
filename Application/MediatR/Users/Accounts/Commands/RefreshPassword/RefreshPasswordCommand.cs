using Application.Models;
using Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.Users.Accounts.Commands
{
    public class RefreshPasswordCommand : IRequest<Result>
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class RefreshPasswordCommandHandler : IRequestHandler<RefreshPasswordCommand, Result>
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<RefreshPasswordCommandHandler> _logger;

        public RefreshPasswordCommandHandler(UserManager<User> userManager,
                                             ILogger<RefreshPasswordCommandHandler> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<Result> Handle(RefreshPasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.Users
                    .Where(x => x.Email == request.Email && x.PhoneNumber == request.PhoneNumber)
                    .FirstOrDefaultAsync();

                if (user == null)
                    return Result.Failure($"Данная почта: {request.Email} не совпадает с указанным номером телефона: {request.PhoneNumber}");

                await _userManager.RemovePasswordAsync(user);
                await _userManager.AddPasswordAsync(user, request.Password);

                return Result.Success("Пароль успешно обновлен");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Password refresh");
                return Result.Failure("Возникли ошибки при собросе пароля");
            }
        }
    }
}