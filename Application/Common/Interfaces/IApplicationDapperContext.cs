using Dapper;
using System.Data;

namespace Application.Common.Interfaces
{
    public interface IApplicationDapperContext
    {
        Task<T> GetOne<T>(string sql, DynamicParameters dp, CommandType commandType = CommandType.Text);

        Task<IEnumerable<T>> Query<T>(string sql, DynamicParameters dp, CommandType commandType = CommandType.Text);
    }
}