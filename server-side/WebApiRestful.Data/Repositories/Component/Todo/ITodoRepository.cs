using WebApiRestful.Data.Repositories.Common;
using WebApiRestful.Domain.Entities.Component;

namespace WebApiRestful.Data.Repositories.Component
{
    public interface ITodoRepository : IGenericRepository<Todo>
    {
        Task<Todo> GetByNameAsync(string name);
    }
}
