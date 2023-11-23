using AutoMapper;
using TodoAPI.Component.DomainLayer.Models.DTO;
using TodoAPI.Component.DomainLayer.Models.Entities;

namespace TodoAPI.Component.ServiceLayer.Services
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            #region CreateRequest -> User
            CreateMap<UserCreate, User>();

            #endregion

            #region UpdateRequest -> User
            CreateMap<UserUpdate, User>()
                .ForAllMembers(x => x.Condition((src, dest, prop) =>
                {
                    // Ignore both null & empty string properties
                    if (prop == null) return false;
                    if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                    // Ignore null role
                    if (x.DestinationMember.Name == "Role" && src.Role == null) return false;

                    return true;
                }
                ));

            #endregion

            #region LoginRequest -> User
            CreateMap<UserLogin, User>()
                .ForAllMembers(x => x.Condition((src, dest, prop) =>
                {
                    // Ignore both null & empty string properties
                    if (prop == null) return false;
                    if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                    return true;
                }
                ));

            #endregion
        }
    }
}
