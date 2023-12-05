using WebApiRestful.Domain.Entities.Component;

namespace WebApiRestful.Data.Repositories.Component
{
    public interface IUserRepository
    {
        Task InitAsync();
        Task<User> GetUserByUsernameAsync(string username);
    }
}