using System.Data;

namespace TodoWebApiRestful.Common.PersistenceLayer.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        /// <summary>
        /// Get All data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Get data By Id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetByIdAsync(int id);

        /// <summary>
        /// Insert new data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="entity"></param>
        /// <param name="dbTransaction"></param>
        /// <returns></returns>
        Task CreateAsync(T entity, IDbTransaction? dbTransaction = null);

        /// <summary>
        /// Update data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="entity"></param>
        /// <param name="dbTransaction"></param>
        /// <returns></returns>
        Task UpdateAsync(T entity, IDbTransaction? dbTransaction = null);

        /// <summary>
        /// Delete data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="id"></param>
        /// <param name="dbTransaction"></param>
        /// <returns></returns>
        Task DeleteAsync(int id, IDbTransaction? dbTransaction = null);
    }
}
