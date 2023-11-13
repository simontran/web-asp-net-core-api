using System.Data;
using Dapper;
using Npgsql;
using Microsoft.Extensions.Options;
using System.Text;

namespace SampleAPI.Core.DomainLayer.Data
{
    public class DataContext
    {
        #region Property
        private readonly DbSettings postgresDbSettings;
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbSettings"></param>
        public DataContext(IOptions<DbSettings> dbSettings)
        {
            postgresDbSettings = dbSettings.Value;
        }

        /// <summary>
        /// CreateConnection
        /// </summary>
        /// <returns></returns>
        public IDbConnection CreateConnection()
        {
            var connectionString = $"Host={postgresDbSettings.Server}; Database={postgresDbSettings.Database}; Username={postgresDbSettings.UserId}; Password={postgresDbSettings.Password};";
            return new NpgsqlConnection(connectionString);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task Init()
        {
            await InitDatabase();
            await InitTables();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task InitDatabase()
        {
            // Create database if it doesn't exist
            var connectionString = $"Host={postgresDbSettings.Server}; Database=template1; Username={postgresDbSettings.UserId}; Password={postgresDbSettings.Password};";
            using var connection = new NpgsqlConnection(connectionString);
            var sqlDbCount = $"SELECT COUNT(*) FROM pg_database WHERE datname = '{postgresDbSettings.Database}';";
            var dbCount = await connection.ExecuteScalarAsync<int>(sqlDbCount);
            if (dbCount == 0)
            {
                var sql = $"CREATE DATABASE \"{postgresDbSettings.Database}\"";
                await connection.ExecuteAsync(sql);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task InitTables()
        {
            // Create tables if they don't exist
            using var connection = CreateConnection();
            await InitUsers();

            async Task InitUsers()
            {
                // Build the SQL string
                StringBuilder sql = new();
                sql.AppendLine("CREATE TABLE IF NOT EXISTS ");
                sql.AppendLine("       Users ( ");
                sql.AppendLine("               Id SERIAL PRIMARY KEY ");
                sql.AppendLine("             , Title VARCHAR ");
                sql.AppendLine("             , FirstName VARCHAR ");
                sql.AppendLine("             , LastName VARCHAR ");
                sql.AppendLine("             , Email VARCHAR ");
                sql.AppendLine("             , Role INTEGER ");
                sql.AppendLine("             , PasswordHash VARCHAR ");
                sql.AppendLine("             ) ");
                await connection.ExecuteAsync(sql.ToString());
            }
        }
    }
}
