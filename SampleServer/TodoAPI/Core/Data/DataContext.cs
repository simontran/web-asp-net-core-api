using Microsoft.Extensions.Options;
using Npgsql;
using System.Data;
using TodoAPI.Core.Common.Helpers;

namespace TodoAPI.Core.DomainLayer.Data
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="appSettings"></param>
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
            var connectionString = $"Host={this._appSettings.Server}; Port={this._appSettings.Port}; Database={this._appSettings.Database}; Username={this._appSettings.UserId}; Password={this._appSettings.Password};";
            return new NpgsqlConnection(connectionString);
        }
    }
}