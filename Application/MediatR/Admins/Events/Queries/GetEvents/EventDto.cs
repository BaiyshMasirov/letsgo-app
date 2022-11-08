using Domain.Enums;

namespace Application.MediatR.Admins.Events.Queries.GetEvents
{
    public record EventDto(Guid Id, string Name, string Description, DateTime StartDate, DateTime EndDate,
                           EventStatus Status, string ImagePath, int AgeLimit, decimal MinPrice, int Sold, int Count, string LocationName);
}