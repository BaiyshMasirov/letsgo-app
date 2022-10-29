using Domain.Enums;

namespace Application.MediatR.Admins.Accounts.Queries.GetAdmins
{
    public record AdminDto(Guid Id, string Email, string FirstName, string LastName,
        string MiddleName, string PhoneNumber, bool LockoutEnabled, UserStatus Status
    )
    {
        public string FullName => $"{LastName} {FirstName} {MiddleName}";
    }
}