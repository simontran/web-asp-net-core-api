using Dapper;
using System.Text;
using static Dapper.SqlMapper;
using SampleAPI.Core.DomainLayer.Data;
using SampleAPI.Core.DomainLayer.Models.Entities;

namespace SampleAPI.Core.RepositoryLayer.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        #region Property
        private readonly DataContext _context;
        #endregion

        /// <summary>
        /// Constructor get instance of context
        /// </summary>
        /// <param name="context"></param>
        public Repository(DataContext context)
        {
            this._context = context;
        }

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
