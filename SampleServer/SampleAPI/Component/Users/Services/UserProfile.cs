using AutoMapper;
using SampleAPI.Component.Users.Models.DTO;
using SampleAPI.Component.Users.Models.Entities;

namespace SampleAPI.Component.ServiceLayer.Services
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {            
            #region CreateRequest -> User
            CreateMap<CreateUser, User>();

            #endregion

            #region UpdateRequest -> User
            CreateMap<UpdateUser, User>()
                .ForAllMembers(x => x.Condition(
                    (src, dest, prop) =>
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
        }
    }
}
