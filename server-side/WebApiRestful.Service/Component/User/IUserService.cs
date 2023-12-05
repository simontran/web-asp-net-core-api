using WebApiRestful.Domain.Entities.Component;

namespace WebApiRestful.Service.Component
{
    public interface IUserService
    {
        Task<User> GetUser(string username);
        Task<User?> CheckLogin(string username, string password);
    }
}