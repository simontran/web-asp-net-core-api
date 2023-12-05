using BCrypt.Net;
using Dapper;
using System.Data;
using System.Text;
using WebApiRestful.Data.DbContext;
using WebApiRestful.Domain.Entities.Component;

namespace WebApiRestful.Data.Repositories.Component
{
    public class UserRepository(NpgDbContext context) : IUserRepository
    {
        #region Property
        private readonly NpgDbContext _context = context;

        #endregion

        /// <summary>
        /// Create table
        /// </summary>
        /// <returns></returns>
        public async Task InitAsync()
        {
            // Build the SQL string
            StringBuilder sql = new();
            sql.AppendLine("CREATE TABLE IF NOT EXISTS ");
            sql.AppendLine("       T_USER ( ");
            sql.AppendLine("               Id SERIAL PRIMARY KEY ");
            sql.AppendLine("             , UserName VARCHAR ");
            sql.AppendLine("             , Password VARCHAR ");
            sql.AppendLine("             , Email VARCHAR ");
            sql.AppendLine("             , CreatedUser VARCHAR ");
            sql.AppendLine("             , CreatedDate TIMESTAMP ");
            sql.AppendLine("             , ModifiedUser VARCHAR ");
            sql.AppendLine("             , ModifiedDate TIMESTAMP ");
            sql.AppendLine("             ) ");
            using (var connection = this._context.CreateConnection())
            {
                if (await connection.ExecuteAsync(sql.ToString()) > 0)
                {
                    // Create Admin user
                    sql = new();
                    sql.AppendLine("INSERT INTO T_USER ( ");
                    sql.AppendLine("            UserName ");
                    sql.AppendLine("          , Password ");
                    sql.AppendLine("          , Email ");
                    sql.AppendLine(" ) VALUES ( ");
                    sql.AppendLine("            @UserName ");
                    sql.AppendLine("          , @Password ");
                    sql.AppendLine("          , @Email ");
                    sql.AppendLine("          ) ");
                    var parameters = new DynamicParameters();
                    parameters.Add("UserName", "admin", DbType.String);
                    parameters.Add("Password", BCrypt.Net.BCrypt.EnhancedHashPassword("@Pas5word", HashType.SHA512), DbType.String);
                    parameters.Add("Email", "admin@suntac.co.jp", DbType.String);
                    await connection.ExecuteAsync(sql.ToString(), parameters);
                }
            }
        }

        /// <summary>
        /// Get data By Username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<User> GetUserByUsernameAsync(string username)
        {
            // Build the SQL string
            StringBuilder sql = new();
            sql.AppendLine("SELECT * ");
            sql.AppendLine("  FROM T_USER ");
            sql.AppendLine(" WHERE UserName = @username ");
            using (var connection = this._context.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<User>(sql.ToString(), new { username });
            }
        }
    }
}
