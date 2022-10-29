using Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.MediatR.Admins.Accounts.Queries.GetAdmins
{
    public record GetAdminsCountQuery : IRequest<int>;

    public class GetAdminsCountQueryHandler : IRequestHandler<GetAdminsCountQuery, int>
    {
        private readonly UserManager<User> _userManager;

        public GetAdminsCountQueryHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<int> Handle(GetAdminsCountQuery request, CancellationToken cancellationToken)
        {
            return await _userManager.Users.CountAsync(x => x.IsAdmin, cancellationToken);
        }
    }
}