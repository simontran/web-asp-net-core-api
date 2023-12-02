using Dapper;
using System.Data;
using TodoWebApiRestful.Common.DomainLayer.Data;

namespace TodoWebApiRestful.Common.PersistenceLayer.Repositories
{
    public class Repository(DataContext context) : IRepository
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
        public async Task<IEnumerable<T>> GetAll<T>(string sql)
        {
            using (var connection = this._context.CreateConnection())
            {
                return await connection.QueryAsync<T>(sql);
            }
        }

        /// <summary>
        /// Get data By Id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> GetById<T>(string sql, int id)
        {
            using (var connection = this._context.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<T>(sql, new { id });
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
        public async Task Create<T>(string sql, T entity, IDbTransaction? dbTransaction = null)
        {
            using (var connection = this._context.CreateConnection())
            {
                await connection.ExecuteAsync(sql, entity, dbTransaction);
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
        public async Task Update<T>(string sql, T entity, IDbTransaction? dbTransaction = null)
        {
            using (var connection = this._context.CreateConnection())
            {
                await connection.ExecuteAsync(sql, entity, dbTransaction);
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
        public async Task Delete<T>(string sql, int id, IDbTransaction? dbTransaction = null)
        {
            using (var connection = this._context.CreateConnection())
            {
                await connection.ExecuteAsync(sql, new { id }, dbTransaction);
            }
        }
    }
}
