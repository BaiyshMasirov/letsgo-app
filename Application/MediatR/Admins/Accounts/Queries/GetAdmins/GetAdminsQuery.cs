using Application.Constants;
using Application.Extensions;
using Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using P.Pager;

namespace Application.MediatR.Admins.Accounts.Queries.GetAdmins
{
    public record GetAdminsQuery(string Email, string PhoneNumber, string Name, int Page = 1) : IRequest<IPager<AdminDto>>;

    public class GetAdminsQueryHandler : IRequestHandler<GetAdminsQuery, IPager<AdminDto>>
    {
        private readonly UserManager<User> _userManager;

        public GetAdminsQueryHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IPager<AdminDto>> Handle(GetAdminsQuery request, CancellationToken cancellationToken)
        {
            var query = _userManager.Users.Where(x => x.UserName != "Admin").AsNoTracking();

            if (!string.IsNullOrWhiteSpace(request.Email))
            {
                query = query.Where(q => EF.Functions.Like(q.Email, $"%{request.Email}%"));
            }

            if (!string.IsNullOrWhiteSpace(request.PhoneNumber))
            {
                query = query.Where(q => EF.Functions.Like(q.PhoneNumber, $"%{request.PhoneNumber}%"));
            }

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                query = query.Where(q => EF.Functions.Like(q.FullName, $"%{request.Name}%"));
            }

            return await query.OrderByDescending(q => q.Status)
                              .ThenBy(q => q.LockoutEnabled)
                              .Select(x => x.AsDto())
                              .ToPagerListAsync(request.Page, Pagination.PAGE_SIZE);
        }
    }
}