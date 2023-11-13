using System.Data;
using Dapper;
using Npgsql;
using Microsoft.Extensions.Options;
using System.Text;

namespace DomainLayer.Data
{
    public class DataContext
    {
        #region Property
        private DbSettings _dbSettings;
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbSettings"></param>
        public DataContext(IOptions<DbSettings> dbSettings)
        {
            _dbSettings = dbSettings.Value;
        }

        /// <summary>
        /// CreateConnection
        /// </summary>
        /// <returns></returns>
        public IDbConnection CreateConnection()
        {
            var connectionString = $"Host={_dbSettings.Server}; Database={_dbSettings.Database}; Username={_dbSettings.UserId}; Password={_dbSettings.Password};";
            return new NpgsqlConnection(connectionString);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task Init()
        {
            await _initDatabase();
            await _initTables();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task _initDatabase()
        {
            // Create database if it doesn't exist
            var connectionString = $"Host={_dbSettings.Server}; Database={_dbSettings.Database}; Username={_dbSettings.UserId}; Password={_dbSettings.Password};";
            using var connection = new NpgsqlConnection(connectionString);
            var sqlDbCount = $"SELECT COUNT(*) FROM pg_database WHERE datname = '{_dbSettings.Database}';";
            var dbCount = await connection.ExecuteScalarAsync<int>(sqlDbCount);
            if (dbCount == 0)
            {
                var sql = $"CREATE DATABASE \"{_dbSettings.Database}\"";
                await connection.ExecuteAsync(sql);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task _initTables()
        {
            // Create tables if they don't exist
            using var connection = CreateConnection();
            await _initUsers();

            async Task _initUsers()
            {
                // Build the SQL string
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("CREATE TABLE IF NOT EXISTS Users ( ");
                sql.AppendLine("                           Id SERIAL PRIMARY KEY ");
                sql.AppendLine("                         , Title VARCHAR ");
                sql.AppendLine("                         , FirstName VARCHAR ");
                sql.AppendLine("                         , LastName VARCHAR ");
                sql.AppendLine("                         , Email VARCHAR ");
                sql.AppendLine("                         , Role INTEGER ");
                sql.AppendLine("                         , PasswordHash VARCHAR ");
                sql.AppendLine("                         ) ");
                await connection.ExecuteAsync(sql.ToString());
            }
        }
    }
}
