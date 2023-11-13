using AutoMapper;
using SampleAPI.Component.DomainLayer.Models;
using SampleAPI.Component.DomainLayer.Models.Entities;

namespace SampleAPI.Component.ServiceLayer.Services
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {            
            #region CreateRequest -> User
            CreateMap<CreateRequest, User>();

            #endregion

            #region UpdateRequest -> User
            CreateMap<UpdateRequest, User>()
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
