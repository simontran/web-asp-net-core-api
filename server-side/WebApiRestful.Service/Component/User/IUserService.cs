using WebApiRestful.Domain.Entities.Component;
using WebApiRestful.Domain.Models;

namespace WebApiRestful.Service.Component
{
    public interface IUserService
    {
        Task AddNewUser(CreateUserModel model);
        Task<User> GetUser(string username);
        Task<User?> CheckLogin(string username, string password);
    }
}