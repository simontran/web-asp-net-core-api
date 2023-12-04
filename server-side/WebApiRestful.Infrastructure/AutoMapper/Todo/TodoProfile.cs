using AutoMapper;
using WebApiRestful.Domain.Entities.Component;
using WebApiRestful.Domain.Models;

namespace WebApiRestful.Infrastructure.AutoMapper
{
    public class TodoProfile : Profile
    {
        public TodoProfile()
        {
            // CreateRequest -> Todo
            CreateMap<TodoCreate, Todo>();

            // UpdateRequest -> Todo
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
        }
    }
}
