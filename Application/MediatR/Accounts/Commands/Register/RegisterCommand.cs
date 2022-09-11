using Application.Common.Interfaces;
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
        private readonly IApplicationEFContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IRtTokenService _rtTokenService;
        private readonly ILogger<RegisterCommandHandler> _logger;

        public RegisterCommandHandler(IApplicationEFContext context,
                                      UserManager<User> userManager,
                                      IRtTokenService rtTokenService,
                                      ILogger<RegisterCommandHandler> logger)
        {
            _context = context;
            _userManager = userManager;
            _rtTokenService = rtTokenService;
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

                string rToken = await _rtTokenService.GenerateRToken(user.Id);
                if (string.IsNullOrWhiteSpace(rToken))
                    return Result.Failure("Не удалось сформировать токен");

                var token = _rtTokenService.GenerateJwtToken(user);

                return Result.Success(new ApiUserToken(token, rToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Account creation failed with error");
                return Result.Failure("Возникли ошибки при регистрации");
            }
        }
    }
}