using AutoMapper;
using System.Text;
using TodoWebApiRestful.Common.PersistenceLayer.Repositories;
using TodoWebApiRestful.Common.ServiceLayer.Services;
using TodoWebApiRestful.Component.DomainLayer.Dto;
using TodoWebApiRestful.Component.DomainLayer.Entities;

namespace TodoWebApiRestful.Component.ServiceLayer.Services
{
    public class TodoService(IRepository repository, IMapper mapper) : IService<Todo, TodoCreate, TodoUpdate>
    {
        #region Property
        private readonly IRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        #endregion

        /// <summary>
        /// Get All data
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Todo>> GetAll()
        {
            // Build the SQL string
            StringBuilder sql = new();
            sql.AppendLine("SELECT * ");
            sql.AppendLine("  FROM T_TODO ");

            return await this._repository.GetAll<Todo>(sql.ToString());
        }

        /// <summary>
        /// Get data By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task<Todo> GetById(int id)
        {
            // Build the SQL string
            StringBuilder sql = new();
            sql.AppendLine("SELECT * ");
            sql.AppendLine("  FROM T_TODO ");
            sql.AppendLine(" WHERE Id = @id ");
            var user = await this._repository.GetById<Todo>(sql.ToString(), id);

            return user ?? throw new KeyNotFoundException("Todo not found.");
        }

        /// <summary>
        /// Insert new data
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task Create(TodoCreate model)
        {
            // Build the SQL string
            StringBuilder sql = new();
            sql.AppendLine("INSERT INTO T_TODO ( ");
            sql.AppendLine("            Name ");
            sql.AppendLine("          , Description ");
            sql.AppendLine("          , IsComplete ");
            sql.AppendLine(" ) VALUES ( ");
            sql.AppendLine("            @Name ");
            sql.AppendLine("          , @Description ");
            sql.AppendLine("          , @IsComplete ");
            sql.AppendLine("          ) ");

            // Ｍap model to new todo object
            var todo = this._mapper.Map<Todo>(model);

            // Save todo
            await this._repository.Create(sql.ToString(), todo);
        }

        /// <summary>
        /// Update data
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task Update(int id, TodoUpdate model)
        {
            // Build the SQL string
            StringBuilder sql = new();
            sql.AppendLine("SELECT * ");
            sql.AppendLine("  FROM T_TODO ");
            sql.AppendLine(" WHERE Id = @id ");

            // Check is exist
            var todo = await this._repository.GetById<Todo>(sql.ToString(), id) ?? throw new KeyNotFoundException("Todo not found.");

            // Copy model props to user
            this._mapper.Map(model, todo);

            // Build the SQL string
            sql = new();
            sql.AppendLine("UPDATE T_TODO ");
            sql.AppendLine("   SET Name = @Name ");
            sql.AppendLine("     , Description = @Description ");
            sql.AppendLine("     , IsComplete = @IsComplete ");
            sql.AppendLine(" WHERE Id = @Id ");

            // Save todo
            await this._repository.Update(sql.ToString(), todo);
        }

        /// <summary>
        /// Delete data
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Delete(int id)
        {
            // Build the SQL string
            StringBuilder sql = new();
            sql.AppendLine("DELETE FROM T_TODO ");
            sql.AppendLine(" WHERE Id = @Id ");

            // Delete todo
            await this._repository.Delete<Todo>(sql.ToString(), id);
        }
    }
}
