using AutoMapper;
using DomainLayer.Entities.Users;
using DomainLayer.Models.Users;
using RepositoryLayer;
using ServiceLayer.Helpers;

namespace ServiceLayer
{
    public class UserService : IService<User, CreateRequest, UpdateRequest>
    {
        #region Property
        private UserRepository _userRepository;
        private readonly IMapper _mapper;
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userRepository"></param>
        /// <param name="mapper"></param>
        public UserService(UserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// GetAll
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<User>> GetAll()
        {
            return await _userRepository.GetAll();
        }

        /// <summary>
        /// GetById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task<User> GetById(int id)
        {
            var user = await _userRepository.GetById(id);

            if (user == null)
                throw new KeyNotFoundException("User not found.");

            return user;
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="AppException"></exception>
        public async Task Create(CreateRequest model)
        {
            // Validate
            if (await _userRepository.GetByEmail(model.Email!) != null)
                throw new AppException("User with the email '" + model.Email + "' already exists.");

            // Ｍap model to new user object
            var user = _mapper.Map<User>(model);

            // Hash password
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

            // Save user
            await _userRepository.Create(user);
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task Update(int id, UpdateRequest model)
        {
            var user = await _userRepository.GetById(id);

            if (user == null)
                throw new KeyNotFoundException("User not found.");

            // Validate
            var emailChanged = !string.IsNullOrEmpty(model.Email) && user.Email != model.Email;
            if (emailChanged && await _userRepository.GetByEmail(model.Email!) != null)
                throw new AppException("User with the email '" + model.Email + "' already exists.");

            // Hash password if it was entered
            if (!string.IsNullOrEmpty(model.Password))
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

            // Copy model props to user
            _mapper.Map(model, user);

            // Save user
            await _userRepository.Update(user);
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Delete(int id)
        {
            await _userRepository.Delete(id);
        }
    }
}
