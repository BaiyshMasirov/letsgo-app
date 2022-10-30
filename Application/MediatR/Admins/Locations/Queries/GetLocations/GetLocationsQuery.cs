using Application.Common.Interfaces;
using Application.Constants;
using Application.Extensions;
using Application.Extensions.Helpers;
using Dapper;
using MediatR;
using P.Pager;
using System.Data;

namespace Application.MediatR.Admins.Locations.Queries.GetLocations
{
    public record GetLocationsQuery(string Name, string Address, int Page = 1) : IRequest<IPager<LocationDto>>;

    public class GetLocationsQueryHandler : IRequestHandler<GetLocationsQuery, IPager<LocationDto>>
    {
        private readonly IApplicationDapperContext _dapper;

        public GetLocationsQueryHandler(IApplicationDapperContext dapper)
        {
            _dapper = dapper;
        }

        public async Task<IPager<LocationDto>> Handle(GetLocationsQuery request, CancellationToken cancellationToken)
        {
            string sql = @"SELECT /**select**/ FROM /**from**/ /**where**/ /**groupby**/ /**orderby**/ ";

            DynamicParameters dp = new();
            dp.Add("@Name", request.Name, DbType.String);
            dp.Add("@Address", request.Address, DbType.String);

            SqlBuilder builder = new SqlBuilder()
                .Select(@"loc.""Id"", loc.""Name"", loc.""Description"",  loc.""ImagePath"", loc.""Address""")
                .Select(@"loc.""XCordinate"", loc.""YCordinate"",loc.""Status"", loc.""Created""")
                .From(@"""Locations"" loc")
                .WhereIf(@"(loc.""Name"" ILIKE CONCAT('%', @Name, '%'))", !string.IsNullOrWhiteSpace(request.Name))
                .WhereIf(@"(loc.""Address"" ILIKE CONCAT('%', @Address, '%'))", !string.IsNullOrWhiteSpace(request.Address))
                .OrderBy(@"loc.""Created"" desc");

            builder
             .GroupBy(@"loc.""Id""")
             .WithPagination(request.Page)
             .AddParameters(dp);

            var countTemplate = builder.CountTemplate(@"""Locations"" loc");
            var rowsTemplate = builder.AddTemplate(sql);

            var count = await _dapper.GetOne<int>(countTemplate.RawSql, (DynamicParameters)countTemplate.Parameters);
            var rows = await _dapper.Query<LocationDto>(rowsTemplate.RawSql, (DynamicParameters)rowsTemplate.Parameters);

            return rows.ToPagerList(count, request.Page, Pagination.PAGE_SIZE);
        }
    }
}