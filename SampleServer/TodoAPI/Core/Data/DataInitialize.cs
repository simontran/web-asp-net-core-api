using Dapper;
using Microsoft.Extensions.Options;
using Npgsql;
using TodoAPI.Core.Common.Helpers;

namespace TodoAPI.Core.DomainLayer.Data
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="appSettings"></param>
    public class DataInitialize(IOptions<AppSettings> appSettings)
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
                var connectionString = $"Host={this._appSettings.Server}; Port={this._appSettings.Port}; Database=template1; Username={this._appSettings.UserId}; Password={this._appSettings.Password};";
                using var connection = new NpgsqlConnection(connectionString);
                var sql = $"SELECT COUNT(*) FROM pg_database WHERE datname = '{this._appSettings.Database}';";
                var dbCount = connection.ExecuteScalar<int>(sql);
                if (dbCount == 0)
                    connection.Execute($"CREATE DATABASE \"{this._appSettings.Database}\"");
            }
            catch
            {
                // log errors or ...
                throw;
            }
        }
    }
}