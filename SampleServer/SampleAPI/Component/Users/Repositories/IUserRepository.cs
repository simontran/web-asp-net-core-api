using SampleAPI.Core.RepositoryLayer.Repositories;
using SampleAPI.Component.DomainLayer.Models.Entities;

namespace SampleAPI.Component.RepositoryLayer.Repository
{
    public interface IUserRepository : IRepository<User>
    {
    }
}