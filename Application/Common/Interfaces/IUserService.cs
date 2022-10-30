namespace Application.Common.Interfaces
{
    public interface IUserService
    {
        public Guid UserId { get; }

        public string FullName { get; }

        public string Email { get; }
    }
}