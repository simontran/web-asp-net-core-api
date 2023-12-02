using AutoMapper;
using TodoWebApiRestful.Component.DomainLayer.Dto;
using TodoWebApiRestful.Component.DomainLayer.Entities;

namespace TodoWebApiRestful.Component.DomainLayer.Mapper
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
