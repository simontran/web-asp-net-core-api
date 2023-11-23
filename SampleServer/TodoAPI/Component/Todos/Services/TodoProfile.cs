using AutoMapper;
using TodoAPI.Component.DomainLayer.Models.DTO;
using TodoAPI.Component.DomainLayer.Models.Entities;

namespace TodoAPI.Component.ServiceLayer.Services
{
    public class TodoProfile : Profile
    {
        public TodoProfile()
        {
            #region CreateRequest -> Todo
            CreateMap<TodoCreate, Todo>();

            #endregion

            #region UpdateRequest -> Todo
            CreateMap<TodoUpdate, Todo>()
                .ForAllMembers(x => x.Condition(
                    (src, dest, prop) =>
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
