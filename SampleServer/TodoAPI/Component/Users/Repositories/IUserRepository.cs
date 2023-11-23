using TodoAPI.Component.DomainLayer.Models.Entities;
using TodoAPI.Core.RepositoryLayer.Repositories;

namespace TodoAPI.Component.RepositoryLayer.Repository
{
    public interface IUserRepository : IRepository<User>
    {
    }
}