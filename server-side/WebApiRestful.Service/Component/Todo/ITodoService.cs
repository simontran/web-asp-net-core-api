using WebApiRestful.Domain.Entities.Component;
using WebApiRestful.Domain.Models;

namespace WebApiRestful.Service.Component
{
    public interface ITodoService
    {
        Task AddNewTodo(CreateTodoModel model);
        Task DeleteTodo(int id);
        Task<IEnumerable<Todo>> GetTodoAll();
        Task<Todo> GetTodoById(int id);
        Task UpdateTodo(int id, UpdateTodoModel model);
    }
}