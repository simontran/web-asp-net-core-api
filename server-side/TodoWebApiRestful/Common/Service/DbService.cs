using Dapper;
using Microsoft.Extensions.Options;
using Npgsql;
using TodoWebApiRestful.Common.InfrastructureLayer.Helpers;

namespace TodoWebApiRestful.Common.ServiceLayer.Services
{
    public class DbService(IOptions<AppSettings> appSettings)
    {
        #region Property
        private readonly AppSettings _appSettings = appSettings.Value;

        #endregion

        /// <summary>
        /// Create Database
        /// </summary>
        public void CreateDatabase()
        {
            try
            {
                // Create database if it doesn't exist
                var connectionString = $"Host={_appSettings.Server}; Port={_appSettings.Port}; Database=template1; Username={_appSettings.UserId}; Password={_appSettings.Password};";
                using var connection = new NpgsqlConnection(connectionString);
                var sql = $"SELECT COUNT(*) FROM pg_database WHERE datname = '{_appSettings.Database}';";
                var dbCount = connection.ExecuteScalar<int>(sql);
                if (dbCount == 0)
                    connection.Execute($"CREATE DATABASE \"{_appSettings.Database}\"");
            }
            catch
            {
                // log errors or ...
                throw;
            }
        }
    }
}