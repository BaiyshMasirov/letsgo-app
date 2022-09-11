using Application.Common.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace Infrastructure.Persistence
{
    public class ApplicationDapperContext : IApplicationDapperContext
    {
        private readonly IConfiguration _configuration;

        public ApplicationDapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private IDbConnection GetConnection()
        {
            return new NpgsqlConnection(_configuration.GetConnectionString("ApplicationConnectionRead"));
        }

        public async Task<T> GetOne<T>(string sql, DynamicParameters dp, CommandType commandType = CommandType.Text)
        {
            using IDbConnection db = GetConnection();

            if (db.State == ConnectionState.Closed)
                db.Open();

            try
            {
                return await db.QueryFirstOrDefaultAsync<T>(sql, dp, commandType: commandType).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                    db.Close();
            }
        }

        public async Task<IEnumerable<T>> Query<T>(string sql, DynamicParameters dp,
            CommandType commandType = CommandType.Text)
        {
            using IDbConnection db = GetConnection();

            if (db.State == ConnectionState.Closed)
                db.Open();

            try
            {
                return await db.QueryAsync<T>(sql, dp, commandType: commandType).ConfigureAwait(false);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                    db.Close();
            }
        }
    }
}