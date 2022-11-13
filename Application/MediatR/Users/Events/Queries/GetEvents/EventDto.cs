using Domain.Enums;

namespace Application.MediatR.Users.Events.Queries.GetEvents
{
    public record EventDto(Guid Id, string Name, string Description, DateTime StartDate, DateTime EndDate,
                           EventStatus Status, string Image, int AgeLimit, decimal MinPrice, int Sold, int Count, string LocationName);
}