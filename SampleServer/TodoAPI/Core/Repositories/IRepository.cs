using TodoAPI.Core.DomainLayer.Models.Entities;

namespace TodoAPI.Core.RepositoryLayer.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(int id);
    }
}