using Application.Common.Interfaces;
using Application.Extensions.Helpers;
using Dapper;
using Domain.Enums;
using MediatR;
using System.Data;

namespace Application.MediatR.Admins.Locations.Queries.GetLocations
{
    public record GetActiveLocationsQuery() : IRequest<List<LocationDto>>;

    public class GetActiveLocationsQueryHandler : IRequestHandler<GetActiveLocationsQuery, List<LocationDto>>
    {
        private readonly IApplicationDapperContext _dapper;

        public GetActiveLocationsQueryHandler(IApplicationDapperContext dapper)
        {
            _dapper = dapper;
        }

        public async Task<List<LocationDto>> Handle(GetActiveLocationsQuery request, CancellationToken cancellationToken)
        {
            string sql = @"SELECT /**select**/ FROM /**from**/ /**where**/ /**groupby**/ /**orderby**/ ";

            DynamicParameters dp = new();
            dp.Add("@Status", LocationStatus.Active, DbType.Int16);

            SqlBuilder builder = new SqlBuilder()
                .Select(@"loc.""Id"", loc.""Name"", loc.""Description"",  loc.""ImagePath"", loc.""Address""")
                .Select(@"loc.""XCordinate"", loc.""YCordinate"",loc.""Status"", loc.""Created""")
                .From(@"""Locations"" loc")
                .Where(@"loc.""Status"" = @Status")
                .OrderBy(@"loc.""Created"" desc")
                .AddParameters(dp);

            builder
             .GroupBy(@"loc.""Id""");

            var rowsTemplate = builder.AddTemplate(sql);
            var rows = await _dapper.Query<LocationDto>(rowsTemplate.RawSql, (DynamicParameters)rowsTemplate.Parameters);

            return rows.ToList();
        }
    }
}