using System.Data;

namespace TodoWebApiRestful.Common.PersistenceLayer.Repositories
{
    public interface IRepository
    {
        /// <summary>
        /// Get All data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAll<T>(string sql);

        /// <summary>
        /// Get data By Id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetById<T>(string sql, int id);

        /// <summary>
        /// Insert new data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="entity"></param>
        /// <param name="dbTransaction"></param>
        /// <returns></returns>
        Task Create<T>(string sql, T entity, IDbTransaction? dbTransaction = null);

        /// <summary>
        /// Update data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="entity"></param>
        /// <param name="dbTransaction"></param>
        /// <returns></returns>
        Task Update<T>(string sql, T entity, IDbTransaction? dbTransaction = null);

        /// <summary>
        /// Delete data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="id"></param>
        /// <param name="dbTransaction"></param>
        /// <returns></returns>
        Task Delete<T>(string sql, int id, IDbTransaction? dbTransaction = null);
    }
}