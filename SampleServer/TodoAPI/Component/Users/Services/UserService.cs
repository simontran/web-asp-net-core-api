using AutoMapper;
using BCrypt.Net;
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
    /// <param name="repository"></param>
    /// <param name="mapper"></param>
    public class UserService(UserRepository repository, IMapper mapper) : IService<User, UserCreate, UserUpdate>
    {
        #region Property
        private readonly UserRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        #endregion

        /// <summary>
        /// Get All User
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<User>> GetAll()
        {
            return await this._repository.GetAll();
        }

        /// <summary>
        /// Get User By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task<User> GetById(int id)
        {
            var user = await this._repository.GetById(id);

            return user ?? throw new KeyNotFoundException("User not found.");
        }

        /// <summary>
        /// Create new User
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="AppException"></exception>
        public async Task Create(UserCreate model)
        {
            // Validate
            if (await this._repository.GetByEmail(model.Email!) != null)
                throw new AppException("User with the email '" + model.Email + "' already exists.");

            // Ｍap model to new user object
            var user = this._mapper.Map<User>(model);

            // Hash password
            user.PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(model.Password, HashType.SHA512);

            // Save user
            await this._repository.Create(user);
        }

        /// <summary>
        /// Update User
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task Update(int id, UserUpdate model)
        {
            var user = await this._repository.GetById(id) ?? throw new KeyNotFoundException("User not found.");

            // Validate
            var emailChanged = !string.IsNullOrEmpty(model.Email) && user.Email != model.Email;
            if (emailChanged && await this._repository.GetByEmail(model.Email!) != null)
                throw new AppException("User with the email '" + model.Email + "' already exists.");

            // Hash password if it was entered
            if (!string.IsNullOrEmpty(model.Password))
                user.PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(model.Password, HashType.SHA512);

            // Copy model props to user
            this._mapper.Map(model, user);

            // Save user
            await this._repository.Update(user);
        }

        /// <summary>
        /// Delete User
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Delete(int id)
        {
            await this._repository.Delete(id);
        }
    }
}