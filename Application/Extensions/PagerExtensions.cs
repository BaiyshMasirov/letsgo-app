using Application.Constants;
using P.Pager;

namespace Application.Extensions
{
    public static class PagerExtensions
    {
        public static IPager<T> ToPagerList<T>(this IEnumerable<T> items, int totalItemsCount, int pageIndex = 1, int pageSize = Pagination.PAGE_SIZE)
        {
            return new Pager<T>(items.AsQueryable(), pageIndex, pageSize, totalItemsCount);
        }
    }
}