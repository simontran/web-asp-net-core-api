using AutoMapper;
using WebApiRestful.Data.Repositories.Component;
using WebApiRestful.Domain.Entities.Component;
using WebApiRestful.Domain.Models;
using WebApiRestful.Service.Common;

namespace WebApiRestful.Service.Component
{
    public class TodoService(IUnitOfWork repository, IMapper mapper) : ITodoService
    {
        #region Property
        private readonly ITodoRepository _repository = repository.Todos;
        private readonly IMapper _mapper = mapper;

        #endregion

        /// <summary>
        /// Get all Todo
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Todo>> GetTodoAll()
        {
            return await _repository.GetAllAsync();
        }

        /// <summary>
        /// Get Todo by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task<Todo> GetTodoById(int id)
        {
            var todo = await _repository.GetByIdAsync(id);
            return todo ?? throw new KeyNotFoundException("Todo not found.");
        }

        /// <summary>
        /// Insert new Todo
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="AppException"></exception>
        public async Task AddNewTodo(TodoCreate model)
        {
            // Validate
            if (await _repository.GetByNameAsync(model.Name!) != null)
                throw new AppException("Todo with the name '" + model.Name + "' already exists.");

            // Map model to new todo object
            var todo = _mapper.Map<Todo>(model);

            // Save todo
            await _repository.CreateAsync(todo);
        }

        /// <summary>
        /// Update Todo
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task UpdateTodo(int id, TodoUpdate model)
        {
            // Check is exist
            var todo = await this._repository.GetByIdAsync(id) ?? throw new KeyNotFoundException("Todo not found.");

            // Copy model props to todo
            _mapper.Map(model, todo);

            // Save todo
            await _repository.UpdateAsync(todo);
        }

        /// <summary>
        /// Delete Todo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteTodo(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
