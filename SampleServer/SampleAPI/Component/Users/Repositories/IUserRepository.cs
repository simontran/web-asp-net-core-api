using SampleAPI.Core.RepositoryLayer.Repositories;
using SampleAPI.Component.Users.Models.Entities;

namespace SampleAPI.Component.RepositoryLayer.Repository
{
    public interface IUserRepository : IRepository<User>
    {
    }
}