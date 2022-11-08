using Domain.Common;
using Domain.Enums;
using Domain.Identity;

namespace Domain.Entities
{
    public class Ticket : AuditableEntity
    {
        public int? PlaceNumber { get; set; }
        public string? QRCode { get; set; }
        public TicketType Type { get; set; }
        public TicketStatus Status { get; set; }
        public decimal Price { get; set; }
        public Guid EventId { get; set; }
        public virtual Event Event { get; set; }
        public Guid? CustomerId { get; set; }
        public virtual User? Customer { get; set; }
    }
}