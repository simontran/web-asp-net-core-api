using Dapper;
using System.Text;
using TodoWebApiRestful.Common.DomainLayer.Data;

namespace TodoWebApiRestful.Component.ServiceLayer.Services
{
    public class TodoTblService(DataContext context)
    {
        #region Property
        private readonly DataContext _context = context;

        #endregion

        /// <summary>
        /// Create Table
        /// </summary>
        public void CreateTable()
        {
            using var connection = _context.CreateConnection();
            // Build the SQL string
            StringBuilder sql = new();
            sql.AppendLine("CREATE TABLE IF NOT EXISTS ");
            sql.AppendLine("       T_TODO ( ");
            sql.AppendLine("               Id SERIAL PRIMARY KEY ");
            sql.AppendLine("             , Name VARCHAR ");
            sql.AppendLine("             , Description VARCHAR ");
            sql.AppendLine("             , IsComplete INTEGER ");
            sql.AppendLine("             ) ");
            connection.Execute(sql.ToString());
        }
    }
}
