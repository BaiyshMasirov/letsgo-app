using Domain.Common;

namespace Domain.Entities
{
    public class Location : AuditableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public string Address { get; set; }
        public double XCordinate { get; set; }
        public double YCordinate { get; set; }
    }
}