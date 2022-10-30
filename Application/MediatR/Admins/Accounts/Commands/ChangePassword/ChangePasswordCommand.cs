using Application.Common.Interfaces;
using Application.Models;
using Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.Admins.Accounts.Commands
{
    public class ChangePasswordCommand : IRequest<Result>
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Result>
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;
        private readonly ILogger<ChangePasswordCommandHandler> _logger;

        public ChangePasswordCommandHandler(UserManager<User> userManager,
                                            IUserService userService,
                                            ILogger<ChangePasswordCommandHandler> logger)
        {
            _userManager = userManager;
            _userService = userService;
            _logger = logger;
        }

        public async Task<Result> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var applicationUser = await _userManager.FindByIdAsync(_userService.UserId.ToString());
                if (applicationUser != null)
                {
                    var identityResult = await _userManager.ChangePasswordAsync(applicationUser, request.OldPassword, request.NewPassword);

                    return identityResult.Succeeded
                             ? Result.Success("Пароль успешно изменен")
                             : Result.Failure(identityResult.Errors.Select(x => x.Description).ToList());
                }

                return Result.Failure("Пользователь не найден");
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"User's password change failed with error");
                return Result.Failure("Возникли ошибки при изменении пароля");
            }
        }
    }
}