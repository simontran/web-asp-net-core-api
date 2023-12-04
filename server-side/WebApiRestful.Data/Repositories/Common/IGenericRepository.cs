using System.Data;

namespace WebApiRestful.Data.Repositories.Common
{
    public interface IGenericRepository<T> where T : class
    {
        /// <summary>
        /// Create table
        /// </summary>
        /// <returns></returns>
        Task InitAsync();

        /// <summary>
        /// Get All data
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Get data By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetByIdAsync(int id);

        /// <summary>
        /// Insert new data
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="dbTransaction"></param>
        /// <returns></returns>
        Task CreateAsync(T entity, IDbTransaction? dbTransaction = null);

        /// <summary>
        /// Update data
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="dbTransaction"></param>
        /// <returns></returns>
        Task UpdateAsync(T entity, IDbTransaction? dbTransaction = null);

        /// <summary>
        /// Delete data
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dbTransaction"></param>
        /// <returns></returns>
        Task DeleteAsync(int id, IDbTransaction? dbTransaction = null);
    }
}
