using Application.Constants;
using Application.Extensions.Helpers;

namespace Application.Extensions
{
    public static class SqlBuilderExtensions
    {
        public static SqlBuilder WhereIf(this SqlBuilder builder, string sql, bool expression)
        {
            return expression
                ? builder.Where(sql)
                : builder;
        }

        public static SqlBuilder WithPagination(this SqlBuilder builder, int pageNumber)
        {
            return builder
                .Offset("@Skip", new { Skip = (pageNumber - 1) * Pagination.PAGE_SIZE })
                .Limit("@Take", new { Take = Pagination.PAGE_SIZE });
        }

        public static SqlBuilder.Template CountTemplate(this SqlBuilder builder, string table)
        {
            return builder.AddTemplate(@$"SELECT COUNT(*) FROM {table} /**join**/ /**leftjoin**/ /**rightjoin**/ /**where**/");
        }
    }
}