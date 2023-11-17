using Dapper;
using Microsoft.Extensions.Options;
using Npgsql;

namespace SampleAPI.Core.DomainLayer.Data
{
    public class DbInit
    {
        #region Property
        private readonly DbSettings postgresDbSettings;
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbSettings"></param>
        public DbInit(IOptions<DbSettings> dbSettings)
        {
            postgresDbSettings = dbSettings.Value;
        }

        /// <summary>
        /// Create Database
        /// </summary>
        public void CreateDatabase()
        {
            // Create database if it doesn't exist
            var connectionString = $"Host={postgresDbSettings.Server}; Port={postgresDbSettings.Port}; Database=template1; Username={postgresDbSettings.UserId}; Password={postgresDbSettings.Password};";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                var sql = $"SELECT COUNT(*) FROM pg_database WHERE datname = '{postgresDbSettings.Database}';";
                var dbCount = connection.ExecuteScalar<int>(sql);
                if (dbCount == 0)
                    connection.Execute($"CREATE DATABASE \"{postgresDbSettings.Database}\"");
            }
        }
    }
}