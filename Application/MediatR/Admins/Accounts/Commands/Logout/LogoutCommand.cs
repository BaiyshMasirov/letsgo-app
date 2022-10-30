using Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.Admins.Accounts.Commands
{
    public record LogoutCommand : IRequest;

    public class LogOutCommandHandler : IRequestHandler<LogoutCommand>
    {
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<LogOutCommandHandler> _logger;

        public LogOutCommandHandler(SignInManager<User> signInManager, ILogger<LogOutCommandHandler> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        public async Task<Unit> Handle(LogoutCommand command, CancellationToken cancellationToken)
        {
            try
            {
                await _signInManager.SignOutAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Account logout failed with error");
            }
            return Unit.Value;
        }
    }
}