using Dapper;
using System.Data;
using System.Text;
using TodoWebApiRestful.Common.DomainLayer.Data;
using TodoWebApiRestful.Component.DomainLayer.Entities;

namespace TodoWebApiRestful.Common.PersistenceLayer.Repositories
{
    public class TodoRepository(DataContext context) : ITodoRepository
    {
        #region Property
        private readonly DataContext _context = context;

        #endregion

        /// <summary>
        /// Get All data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
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
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
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
        /// Insert new data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
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
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
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
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
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
