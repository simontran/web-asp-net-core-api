using Dapper;
using System.Text;
using TodoAPI.Component.DomainLayer.Models.Entities;
using TodoAPI.Core.DomainLayer.Data;
using TodoAPI.Core.RepositoryLayer.Repositories;

namespace TodoAPI.Component.RepositoryLayer.Repository
{
    /// <summary>
    /// Constructor get instance of context
    /// </summary>
    /// <param name="context"></param>
    public class TodoRepository(DataContext context) : Repository<Todo>(context)
    {
        #region Property
        private readonly DataContext _context = context;

        #endregion

        /// <summary>
        /// Get All Todo
        /// </summary>
        /// <returns></returns>
        public new async Task<IEnumerable<Todo>> GetAll()
        {
            using var connection = this._context.CreateConnection();
            // Build the SQL string
            StringBuilder sql = new();
            sql.AppendLine("SELECT * ");
            sql.AppendLine("  FROM T_TODO ");
            return await connection.QueryAsync<Todo>(sql.ToString());
        }

        /// <summary>
        /// Get Todo By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public new async Task<Todo> GetById(int id)
        {
            using var connection = this._context.CreateConnection();
            // Build the SQL string
            StringBuilder sql = new();
            sql.AppendLine("SELECT * ");
            sql.AppendLine("  FROM T_TODO ");
            sql.AppendLine(" WHERE Id = @id ");
            return await connection?.QuerySingleOrDefaultAsync<Todo>(sql.ToString(), new { id });
        }

        /// <summary>
        /// Insert new Todo
        /// </summary>
        /// <param name="todo"></param>
        /// <returns></returns>
        public new async Task Create(Todo todo)
        {
            using var connection = this._context.CreateConnection();
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
            await connection.ExecuteAsync(sql.ToString(), todo);
        }

        /// <summary>
        /// Update Todo
        /// </summary>
        /// <param name="todo"></param>
        /// <returns></returns>
        public new async Task Update(Todo todo)
        {
            using var connection = this._context.CreateConnection();
            // Build the SQL string
            StringBuilder sql = new();
            sql.AppendLine("UPDATE T_TODO ");
            sql.AppendLine("   SET Name = @Name ");
            sql.AppendLine("     , Description = @Description ");
            sql.AppendLine("     , IsComplete = @IsComplete ");
            sql.AppendLine(" WHERE Id = @Id ");
            await connection.ExecuteAsync(sql.ToString(), todo);
        }

        /// <summary>
        /// Delete Todo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public new async Task Delete(int id)
        {
            using var connection = this._context.CreateConnection();
            // Build the SQL string
            StringBuilder sql = new();
            sql.AppendLine("DELETE FROM T_TODO ");
            sql.AppendLine(" WHERE Id = @Id ");
            await connection.ExecuteAsync(sql.ToString(), new { id });
        }
    }
}