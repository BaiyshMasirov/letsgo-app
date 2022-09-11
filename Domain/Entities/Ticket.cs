using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class Ticket : AuditableEntity
    {
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerName { get; set; }
        public string QRCode { get; set; }
        public TicketType Type { get; set; }
        public TicketStatus Status { get; set; }
    }
}