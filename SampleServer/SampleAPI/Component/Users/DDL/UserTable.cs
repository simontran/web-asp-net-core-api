using Dapper;
using SampleAPI.Core.DomainLayer.Data;
using System.Text;

namespace SampleAPI.Component.DDL
{
    public class UserTable
    {
        #region Property
        private readonly DataContext _context;
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public UserTable(DataContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// Create Database
        /// </summary>
        public void CreateTable()
        {
            using var connection = this._context.CreateConnection();
            // Build the SQL string
            StringBuilder sql = new();
            sql.AppendLine("CREATE TABLE IF NOT EXISTS ");
            sql.AppendLine("       Users ( ");
            sql.AppendLine("               Id SERIAL PRIMARY KEY ");
            sql.AppendLine("             , Title VARCHAR ");
            sql.AppendLine("             , FirstName VARCHAR ");
            sql.AppendLine("             , LastName VARCHAR ");
            sql.AppendLine("             , Email VARCHAR ");
            sql.AppendLine("             , Role INTEGER ");
            sql.AppendLine("             , PasswordHash VARCHAR ");
            sql.AppendLine("             ) ");
            connection.Execute(sql.ToString());
        }
    }
}
