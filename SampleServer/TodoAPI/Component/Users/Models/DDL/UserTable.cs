using BCrypt.Net;
using Dapper;
using System.Data;
using System.Text;
using TodoAPI.Core.DomainLayer.Data;

namespace TodoAPI.Component.DomainLayer.Models.DDL
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context"></param>
    public class UserTable(DataContext context)
    {
        #region Property
        private readonly DataContext _context = context;

        #endregion

        /// <summary>
        /// Create Database
        /// </summary>
        public void CreateTable()
        {
            try
            {
                using var connection = _context.CreateConnection();
                // Create User table
                // Build the SQL string
                StringBuilder sql = new();
                sql.AppendLine("CREATE TABLE IF NOT EXISTS ");
                sql.AppendLine("       M_USER ( ");
                sql.AppendLine("               Id SERIAL PRIMARY KEY ");
                sql.AppendLine("             , UserName VARCHAR ");
                sql.AppendLine("             , Email VARCHAR ");
                sql.AppendLine("             , Role INTEGER ");
                sql.AppendLine("             , PasswordHash VARCHAR ");
                sql.AppendLine("             ) ");
                if (connection.Execute(sql.ToString()) > 0)
                {
                    // Create Admin user
                    sql = new();
                    sql.AppendLine("INSERT INTO M_USER ( ");
                    sql.AppendLine("            UserName ");
                    sql.AppendLine("          , Email ");
                    sql.AppendLine("          , Role ");
                    sql.AppendLine("          , PasswordHash ");
                    sql.AppendLine(" ) VALUES ( ");
                    sql.AppendLine("            @UserName ");
                    sql.AppendLine("          , @Email ");
                    sql.AppendLine("          , @Role ");
                    sql.AppendLine("          , @PasswordHash ");
                    sql.AppendLine("          ) ");
                    var parameters = new DynamicParameters();
                    parameters.Add("UserName", "admin", DbType.String);
                    parameters.Add("Email", "admin@example.com", DbType.String);
                    parameters.Add("Role", "Admin", DbType.String);
                    parameters.Add("PasswordHash", BCrypt.Net.BCrypt.EnhancedHashPassword("@Pas5word", HashType.SHA512), DbType.String);
                    connection.Execute(sql.ToString(), parameters);
                }
            }
            catch
            {
                // log errors or ...
                throw;
            }
        }
    }
}
