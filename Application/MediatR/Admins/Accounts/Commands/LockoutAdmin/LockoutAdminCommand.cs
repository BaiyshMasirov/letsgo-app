using Application.Models;
using Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.Admins.Accounts.Commands
{
    public record LockoutAdminCommand(Guid Id) : IRequest<Result>;

    public class LockoutUserCommandHandler : IRequestHandler<LockoutAdminCommand, Result>
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<LockoutUserCommandHandler> _logger;

        public LockoutUserCommandHandler(UserManager<User> userManager, ILogger<LockoutUserCommandHandler> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<Result> Handle(LockoutAdminCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var applicationUser = await _userManager.FindByIdAsync(request.Id.ToString());
                if (applicationUser != null)
                {
                    applicationUser.LockoutEnabled = true;
                    applicationUser.LockoutEnd = DateTime.MaxValue;

                    var identityResult = await _userManager.UpdateAsync(applicationUser);

                    return identityResult.Succeeded
                   ? Result.Success("Успешно заблокирован")
                   : Result.Failure(identityResult.Errors.Select(x => x.Description).ToList());
                }

                return Result.Failure("Пользователь по данному идентификатору не найден");
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"User lockout failed with error");
                return Result.Failure("Возникли ошибки при заблокировании пользователся");
            }
        }
    }
}