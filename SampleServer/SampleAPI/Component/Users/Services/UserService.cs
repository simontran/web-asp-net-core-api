using AutoMapper;
using SampleAPI.Core.ServiceLayer.Services;
using SampleAPI.Component.DomainLayer.Models.Entities;
using SampleAPI.Component.DomainLayer.Models;
using SampleAPI.Component.RepositoryLayer.Repository;
using SampleAPI.Core.ServiceLayer.Helpers;

namespace SampleAPI.Component.ServiceLayer.Services
{
    public class UserService : IService<User, CreateUser, UpdateUser>
    {
        #region Property
        private readonly UserRepository userRepository;
        private readonly IMapper userMapper;
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userRepository"></param>
        /// <param name="mapper"></param>
        public UserService(UserRepository repository, IMapper mapper)
        {
            userRepository = repository;
            userMapper = mapper;
        }

        /// <summary>
        /// GetAll
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<User>> GetAll()
        {
            return await userRepository.GetAll();
        }

        /// <summary>
        /// GetById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task<User> GetById(int id)
        {
            var user = await userRepository.GetById(id);

            return user ?? throw new KeyNotFoundException("User not found.");
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="AppException"></exception>
        public async Task Create(CreateUser model)
        {
            // Validate
            if (await userRepository.GetByEmail(model.Email!) != null)
                throw new AppException("User with the email '" + model.Email + "' already exists.");

            // Ｍap model to new user object
            var user = userMapper.Map<User>(model);

            // Hash password
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

            // Save user
            await userRepository.Create(user);
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task Update(int id, UpdateUser model)
        {
            var user = await userRepository.GetById(id) ?? throw new KeyNotFoundException("User not found.");

            // Validate
            var emailChanged = !string.IsNullOrEmpty(model.Email) && user.Email != model.Email;
            if (emailChanged && await userRepository.GetByEmail(model.Email!) != null)
                throw new AppException("User with the email '" + model.Email + "' already exists.");

            // Hash password if it was entered
            if (!string.IsNullOrEmpty(model.Password))
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

            // Copy model props to user
            userMapper.Map(model, user);

            // Save user
            await userRepository.Update(user);
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Delete(int id)
        {
            await userRepository.Delete(id);
        }
    }
}
