using Application.Extensions;
using Application.Models;
using Domain.Enums;
using Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.Accounts.Commands.Confirm
{
    public class ConfirmUserCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
    }

    public class ConfirmUserCommandHandler : IRequestHandler<ConfirmUserCommand, Result>
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<ConfirmUserCommandHandler> _logger;

        public ConfirmUserCommandHandler(UserManager<User> userManager, ILogger<ConfirmUserCommandHandler> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<Result> Handle(ConfirmUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var applicationUser = await _userManager.FindByIdAsync(request.Id.ToString());
                if (applicationUser != null)
                {
                    applicationUser.Status = UserStatus.Active;
                    applicationUser.LockoutEnabled = false;
                    applicationUser.LockoutEnd = null;

                    var identityResult = await _userManager.UpdateAsync(applicationUser);
                    return identityResult.ToApplicationResult("Пользователь подтвержден");
                }

                return Result.Failure("Пользователь по данному идентификатору не найден");
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"User confirm failed with error");
                return Result.Failure("Возникли ошибки при обновлении записи");
            }
        }
    }
}