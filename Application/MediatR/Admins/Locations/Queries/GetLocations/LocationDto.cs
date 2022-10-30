using Domain.Enums;

namespace Application.MediatR.Admins.Locations.Queries.GetLocations
{
    public record LocationDto(Guid Id, string Name, string Description, string ImagePath, string Address, 
                              double XCordinate, double YCordinate, LocationStatus Status, DateTime Created);
}