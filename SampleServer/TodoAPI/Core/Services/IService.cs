namespace TodoAPI.Core.ServiceLayer.Services
{
    public interface IService<T1, T2, T3>
        where T1 : class
        where T2 : class
        where T3 : class
    {
        Task<IEnumerable<T1>> GetAll();
        Task<T1> GetById(int id);
        Task Create(T2 model);
        Task Update(int id, T3 model);
        Task Delete(int id);
    }
}