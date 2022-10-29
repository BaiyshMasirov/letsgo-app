using Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.MediatR.Users.Accounts.Queries
{
    public record GetUsersCountQuery : IRequest<int>;

    public class GetUsersCountQueryHandler : IRequestHandler<GetUsersCountQuery, int>
    {
        private readonly UserManager<User> _userManager;

        public GetUsersCountQueryHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<int> Handle(GetUsersCountQuery request, CancellationToken cancellationToken)
        {
            return await _userManager.Users.CountAsync(x => !x.IsAdmin, cancellationToken);
        }
    }
}