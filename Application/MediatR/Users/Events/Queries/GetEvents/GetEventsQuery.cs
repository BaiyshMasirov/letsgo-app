using Application.Common.Interfaces;
using Application.Constants;
using Application.Extensions;
using Application.Extensions.Helpers;
using Dapper;
using Domain.Enums;
using MediatR;
using P.Pager;
using System.Data;

namespace Application.MediatR.Users.Events.Queries.GetEvents
{
    public record GetEventsQuery(string Name, EventStatus Status, int Page = 1) : IRequest<IPager<EventDto>>;

    public class GetEventsQueryHandler : IRequestHandler<GetEventsQuery, IPager<EventDto>>
    {
        private readonly IApplicationDapperContext _dapper;

        public GetEventsQueryHandler(IApplicationDapperContext dapper)
        {
            _dapper = dapper;
        }

        public async Task<IPager<EventDto>> Handle(GetEventsQuery request, CancellationToken cancellationToken)
        {
            string sql = @"SELECT /**select**/ FROM /**from**/ /**join**/ /**where**/ /**groupby**/ /**orderby**/ ";

            DynamicParameters dp = new();
            dp.Add("@Name", request.Name, DbType.String);
            dp.Add("@Status", request.Status, DbType.Int16);

            SqlBuilder builder = new SqlBuilder()
                .Select(@"ev.""Id"", ev.""Name"", ev.""Description"",  ev.""StartDate"", ev.""EndDate""")
                .Select(@"ev.""Status"", ev.""ImagePath"" as Image, ev.""AgeLimit"", ev.""MinPrice"", ev.""Sold"", ev.""Count"",loc.""Name"" as LocationName")
                .From(@"""Events"" ev")
                .Join(@"""Locations"" loc ON loc.""Id"" = ev.""LocationId""")
                .WhereIf(@"(ev.""Name"" ILIKE CONCAT('%', @Name, '%'))", !string.IsNullOrWhiteSpace(request.Name))
                .WhereIf(@"ev.""Address"" = @Status ", request.Status != default)
                .OrderBy(@"ev.""Created"" desc");

            builder
             .GroupBy(@"ev.""Id"", loc.""Name""")
             .WithPagination(request.Page)
             .AddParameters(dp);

            var countTemplate = builder.CountTemplate(@"""Events"" ev");
            var rowsTemplate = builder.AddTemplate(sql);

            var count = await _dapper.GetOne<int>(countTemplate.RawSql, (DynamicParameters)countTemplate.Parameters);
            var rows = await _dapper.Query<EventDto>(rowsTemplate.RawSql, (DynamicParameters)rowsTemplate.Parameters);

            return rows.ToPagerList(count, request.Page, Pagination.PAGE_SIZE);
        }
    }
}
}