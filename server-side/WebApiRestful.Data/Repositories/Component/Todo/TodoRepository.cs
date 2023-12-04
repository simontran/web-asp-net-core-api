using Dapper;
using System.Data;
using System.Text;
using WebApiRestful.Data.DbContext;
using WebApiRestful.Domain.Entities.Component;

namespace WebApiRestful.Data.Repositories.Component
{
    public class TodoRepository(NpgDbContext context) : ITodoRepository
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
            sql.AppendLine("       T_TODO ( ");
            sql.AppendLine("               Id SERIAL PRIMARY KEY ");
            sql.AppendLine("             , Name VARCHAR ");
            sql.AppendLine("             , Description VARCHAR ");
            sql.AppendLine("             , IsComplete INTEGER ");
            sql.AppendLine("             , CreatedUser VARCHAR ");
            sql.AppendLine("             , CreatedDate TIMESTAMP ");
            sql.AppendLine("             , ModifiedUser VARCHAR ");
            sql.AppendLine("             , ModifiedDate TIMESTAMP ");
            sql.AppendLine("             ) ");
            using (var connection = this._context.CreateConnection())
            {
                await connection.ExecuteAsync(sql.ToString());
            }
        }

        /// <summary>
        /// Get All data
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Todo>> GetAllAsync()
        {
            // Build the SQL string
            StringBuilder sql = new();
            sql.AppendLine("SELECT * ");
            sql.AppendLine("  FROM T_TODO ");
            using (var connection = this._context.CreateConnection())
            {
                return await connection.QueryAsync<Todo>(sql.ToString());
            }
        }

        /// <summary>
        /// Get data By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Todo> GetByIdAsync(int id)
        {
            // Build the SQL string
            StringBuilder sql = new();
            sql.AppendLine("SELECT * ");
            sql.AppendLine("  FROM T_TODO ");
            sql.AppendLine(" WHERE Id = @id ");
            using (var connection = this._context.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<Todo>(sql.ToString(), new { id });
            }
        }

        /// <summary>
        /// Get data By Name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<Todo> GetByNameAsync(string name)
        {
            // Build the SQL string
            StringBuilder sql = new();
            sql.AppendLine("SELECT * ");
            sql.AppendLine("  FROM T_TODO ");
            sql.AppendLine(" WHERE Name = @name ");
            using (var connection = this._context.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<Todo>(sql.ToString(), new { name });
            }
        }

        /// <summary>
        /// Insert new data
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="dbTransaction"></param>
        /// <returns></returns>
        public async Task CreateAsync(Todo entity, IDbTransaction? dbTransaction = null)
        {
            // Build the SQL string
            StringBuilder sql = new();
            sql.AppendLine("INSERT INTO T_TODO ( ");
            sql.AppendLine("            Name ");
            sql.AppendLine("          , Description ");
            sql.AppendLine("          , IsComplete ");
            sql.AppendLine(" ) VALUES ( ");
            sql.AppendLine("            @Name ");
            sql.AppendLine("          , @Description ");
            sql.AppendLine("          , @IsComplete ");
            sql.AppendLine("          ) ");
            using (var connection = this._context.CreateConnection())
            {
                await connection.ExecuteAsync(sql.ToString(), entity, dbTransaction);
            }
        }

        /// <summary>
        /// Update data
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="dbTransaction"></param>
        /// <returns></returns>
        public async Task UpdateAsync(Todo entity, IDbTransaction? dbTransaction = null)
        {
            // Build the SQL string
            StringBuilder sql = new();
            sql.AppendLine("UPDATE T_TODO ");
            sql.AppendLine("   SET Name = @Name ");
            sql.AppendLine("     , Description = @Description ");
            sql.AppendLine("     , IsComplete = @IsComplete ");
            sql.AppendLine(" WHERE Id = @Id ");
            using (var connection = this._context.CreateConnection())
            {
                await connection.ExecuteAsync(sql.ToString(), entity, dbTransaction);
            }
        }

        /// <summary>
        /// Delete data
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dbTransaction"></param>
        /// <returns></returns>
        public async Task DeleteAsync(int id, IDbTransaction? dbTransaction = null)
        {
            // Build the SQL string
            StringBuilder sql = new();
            sql.AppendLine("DELETE FROM T_TODO ");
            sql.AppendLine(" WHERE Id = @Id ");
            using (var connection = this._context.CreateConnection())
            {
                await connection.ExecuteAsync(sql.ToString(), new { id }, dbTransaction);
            }
        }
    }
}
