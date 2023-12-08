using AutoMapper;
using WebApiRestful.Domain.Entities.Component;
using WebApiRestful.Domain.Models;

namespace WebApiRestful.Infrastructure.AutoMapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            // CreateUserModel -> User
            CreateMap<CreateUserModel, User>();
        }
    }
}
