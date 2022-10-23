using Application.MediatR.Admins.Accounts.Queries.GetAdmins;
using Domain.Identity;

namespace Application.Extensions
{
    public static class MappingExtension
    {
        public static AdminDto AsDto(this User user)
        {
            return new AdminDto(
               user.Id, user.Email, user.FirstName, user.LastName, user.MiddleName, user.PhoneNumber, user.LockoutEnabled
            );
        }
    }
}