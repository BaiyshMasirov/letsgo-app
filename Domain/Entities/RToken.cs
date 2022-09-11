using Domain.Common;
using Domain.Identity;

namespace Domain.Entities
{
    public class RToken : BaseEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public string RefreshToken { get; set; }
        public bool IsStop { get; set; }
    }
}