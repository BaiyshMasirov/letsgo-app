using Application.Extensions;
using Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.MediatR.Admins.Accounts.Queries.GetAdmins
{
    public record GetAdminByIdQuery(Guid Id) : IRequest<AdminDto>;

    public class GetAdminByIdQueryHandler : IRequestHandler<GetAdminByIdQuery, AdminDto>
    {
        private readonly UserManager<User> _userManager;

        public GetAdminByIdQueryHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<AdminDto> Handle(GetAdminByIdQuery request, CancellationToken cancellationToken)
        {
            var applicationUser = await _userManager.FindByIdAsync(request.Id.ToString());
            return applicationUser switch
            {
                null => null,
                _ => applicationUser.AsDto()
            };
        }
    }
}