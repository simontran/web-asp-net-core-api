namespace TodoWebApiRestful.Common.ServiceLayer.Services
{
    public interface IService<T1, T2, T3>
        where T1 : class
        where T2 : class
        where T3 : class
    {
        /// <summary>
        /// Get All data
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T1>> GetAll();

        /// <summary>
        /// Get data By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T1> GetById(int id);

        /// <summary>
        /// Insert new data
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task Create(T2 model);

        /// <summary>
        /// Update data
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        Task Update(int id, T3 model);

        /// <summary>
        /// Delete data
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Delete(int id);
    }
}
