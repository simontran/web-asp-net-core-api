using System.Data;
using WebApiRestful.Domain.Entities.Component;

namespace WebApiRestful.Data.Repositories.Component
{
    public interface IUserRepository
    {
        Task InitAsync();
        Task CreateAsync(User entity, IDbTransaction? dbTransaction = null);
        Task<User> GetUserByUsernameAsync(string username);
    }
}