using Dapper;
using System.Text;
using TodoAPI.Core.DomainLayer.Data;
using TodoAPI.Core.DomainLayer.Models.Entities;
using static Dapper.SqlMapper;

namespace TodoAPI.Core.RepositoryLayer.Repositories
{
    /// <summary>
    /// Constructor get instance of context
    /// </summary>
    /// <param name="context"></param>
    public class Repository<T>(DataContext context) : IRepository<T> where T : BaseEntity
    {
        #region Property
        private readonly DataContext _context = context;

        #endregion

        /// <summary>
        /// Get All
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetAll()
        {
            try
            {
                using var connection = this._context.CreateConnection();
                // Build the SQL string
                StringBuilder sql = new();
                return await connection.QueryAsync<T>(sql.ToString());
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Get By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> GetById(int id)
        {
            try
            {
                using var connection = this._context.CreateConnection();
                // Build the SQL string
                StringBuilder sql = new();
                return await connection?.QuerySingleOrDefaultAsync<T>(sql.ToString(), new { id });
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Insert new data
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task Create(T entity)
        {
            try
            {
                using var connection = this._context.CreateConnection();
                // Build the SQL string
                StringBuilder sql = new();
                await connection.ExecuteAsync(sql.ToString(), entity);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Update data
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task Update(T entity)
        {
            try
            {
                using var connection = this._context.CreateConnection();
                // Build the SQL string
                StringBuilder sql = new();
                await connection.ExecuteAsync(sql.ToString(), entity);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Delete data
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Delete(int id)
        {
            try
            {
                using var connection = this._context.CreateConnection();
                // Build the SQL string
                StringBuilder sql = new();
                await connection.ExecuteAsync(sql.ToString(), new { id });
            }
            catch
            {
                throw;
            }
        }
    }
}