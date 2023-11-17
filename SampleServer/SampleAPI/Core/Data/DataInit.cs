using Dapper;
using Microsoft.Extensions.Options;
using Npgsql;
using SampleAPI.Core.Common.Helpers;

namespace SampleAPI.Core.DomainLayer.Data
{
    public class DataInit
    {
        #region Property
        private readonly AppSettings _appSettings;
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="appSettings"></param>
        public DataInit(IOptions<AppSettings> appSettings)
        {
            this._appSettings = appSettings.Value;
        }

        /// <summary>
        /// Create Database
        /// </summary>
        public void CreateDatabase()
        {
            // Create database if it doesn't exist
            var connectionString = $"Host={this._appSettings.Server}; Port={this._appSettings.Port}; Database=template1; Username={this._appSettings.UserId}; Password={this._appSettings.Password};";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                var sql = $"SELECT COUNT(*) FROM pg_database WHERE datname = '{this._appSettings.Database}';";
                var dbCount = connection.ExecuteScalar<int>(sql);
                if (dbCount == 0)
                    connection.Execute($"CREATE DATABASE \"{this._appSettings.Database}\"");
            }
        }
    }
}