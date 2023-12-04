using Dapper;
using Microsoft.Extensions.Options;
using Npgsql;
using System.Data;
using WebApiRestful.Domain.Entities.Common;

namespace WebApiRestful.Data.DbContext
{
    public class NpgDbContext(IOptions<Database> database)
    {
        #region Property
        private readonly Database _database = database.Value;

        #endregion

        /// <summary>
        /// Create Connection
        /// </summary>
        /// <returns></returns>
        public IDbConnection CreateConnection()
        {
            var connectionString = $"Host={_database.Server}; Port={_database.Port}; Database={_database.Name}; Username={_database.UserId}; Password={_database.Password};";
            return new NpgsqlConnection(connectionString);
        }

        /// <summary>
        /// Create Database
        /// </summary>
        /// <returns></returns>
        public async Task CreateDatabase()
        {
            // Create database if it doesn't exist
            var connectionString = $"Host={_database.Server}; Port={_database.Port}; Database=template1; Username={_database.UserId}; Password={_database.Password};";
            using var connection = new NpgsqlConnection(connectionString);
            var sql = $"SELECT COUNT(*) FROM pg_database WHERE datname = '{_database.Name}';";
            var dbCount = connection.ExecuteScalarAsync<int>(sql);
            if (await dbCount == 0)
                _ = connection.ExecuteAsync($"CREATE DATABASE \"{_database.Name}\"");
        }
    }
}
