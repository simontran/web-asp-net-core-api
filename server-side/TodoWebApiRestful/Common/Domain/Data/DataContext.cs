using Microsoft.Extensions.Options;
using Npgsql;
using System.Data;
using TodoWebApiRestful.Common.InfrastructureLayer.Helpers;

namespace TodoWebApiRestful.Common.DomainLayer.Data
{
    public class DataContext(IOptions<AppSettings> appSettings)
    {
        #region Property
        private readonly AppSettings _appSettings = appSettings.Value;

        #endregion

        /// <summary>
        /// Create Connection
        /// </summary>
        /// <returns></returns>
        public IDbConnection CreateConnection()
        {
            var connectionString = $"Host={_appSettings.Server}; Port={_appSettings.Port}; Database={_appSettings.Database}; Username={_appSettings.UserId}; Password={_appSettings.Password};";
            return new NpgsqlConnection(connectionString);
        }
    }
}