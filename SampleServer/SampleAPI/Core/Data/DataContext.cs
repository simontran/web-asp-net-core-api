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
        /// Create Connection
        /// </summary>
        /// <returns></returns>
        public IDbConnection CreateConnection()
        {
            var connectionString = $"Host={postgresDbSettings.Server}; Port={postgresDbSettings.Port}; Database={postgresDbSettings.Database}; Username={postgresDbSettings.UserId}; Password={postgresDbSettings.Password};";
            return new NpgsqlConnection(connectionString);
        }
    }
}