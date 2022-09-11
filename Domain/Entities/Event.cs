using Domain.Common;
using Domain.Enums;
using Domain.Identity;

namespace Domain.Entities
{
    public class Event : AuditableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public EventStatus Status { get; set; }
        public string ImagePath { get; set; }
        public int AgeLimit { get; set; }
        public int TicketLimit { get; set; }
        public double MinPrice { get; set; }
        public int Sold { get; set; }
        public int Count { get; set; }
        public Guid LocationId { get; set; }
        public virtual Location Location { get; set; }
        public Guid OrganizerId { get; set; }
        public virtual User Organizer { get; set; }
    }
}