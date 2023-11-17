using System.Data;
using Npgsql;
using Microsoft.Extensions.Options;
using SampleAPI.Core.Common.Helpers;

namespace SampleAPI.Core.DomainLayer.Data
{
    public class DataContext
    {
        #region Property
        private readonly AppSettings _appSettings;
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="appSettings"></param>
        public DataContext(IOptions<AppSettings> appSettings)
        {
            this._appSettings = appSettings.Value;
        }

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