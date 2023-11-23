using AutoMapper;
using TodoAPI.Component.DomainLayer.Models.DTO;
using TodoAPI.Component.DomainLayer.Models.Entities;
using TodoAPI.Component.RepositoryLayer.Repository;
using TodoAPI.Core.Common.Helpers;
using TodoAPI.Core.ServiceLayer.Services;

namespace TodoAPI.Component.ServiceLayer.Services
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="userRepository"></param>
    /// <param name="mapper"></param>
    public class TodoService(TodoRepository repository, IMapper mapper) : IService<Todo, TodoCreate, TodoUpdate>
    {
        #region Property
        private readonly TodoRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        #endregion

        /// <summary>
        /// GetAll
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Todo>> GetAll()
        {
            return await this._repository.GetAll();
        }

        /// <summary>
        /// Get By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task<Todo> GetById(int id)
        {
            var user = await this._repository.GetById(id);

            return user ?? throw new KeyNotFoundException("Todo not found.");
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="AppException"></exception>
        public async Task Create(TodoCreate model)
        {
            // Ｍap model to new user object
            var todo = this._mapper.Map<Todo>(model);

            // Save user
            await this._repository.Create(todo);
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task Update(int id, TodoUpdate model)
        {
            var todo = await this._repository.GetById(id) ?? throw new KeyNotFoundException("Todo not found.");

            // Validate

            // Copy model props to user
            this._mapper.Map(model, todo);

            // Save todo
            await this._repository.Update(todo);
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Delete(int id)
        {
            await this._repository.Delete(id);
        }
    }
}
