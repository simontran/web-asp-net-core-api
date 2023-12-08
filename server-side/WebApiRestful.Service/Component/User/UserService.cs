using AutoMapper;
using BCrypt.Net;
using WebApiRestful.Data.Repositories.Component;
using WebApiRestful.Domain.Entities.Component;
using WebApiRestful.Domain.Models;
using WebApiRestful.Service.Common;

namespace WebApiRestful.Service.Component
{
    public class UserService(IUnitOfWork repository, IMapper mapper) : IUserService
    {
        #region Property
        private readonly IUserRepository _repository = repository.Users;
        private readonly IMapper _mapper = mapper;

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task AddNewUser(CreateUserModel model)
        {
            // Validate
            if (await this._repository.GetUserByUsernameAsync(model.UserName!) != null)
                throw new AppException("User with the username '" + model.UserName + "' already exists.");

            // Ｍap model to new user object
            var user = this._mapper.Map<User>(model);

            // Hash password
            user.Password = BCrypt.Net.BCrypt.EnhancedHashPassword(model.Password, HashType.SHA512);

            // Save user
            await this._repository.CreateAsync(user);
        }

        /// <summary>
        /// Check Login
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<User?> CheckLogin(string username, string password)
        {
            // Get Password of User
            var user = await this._repository.GetUserByUsernameAsync(username);
            if (user != null)
            {
                // Verifying Password
                if (BCrypt.Net.BCrypt.EnhancedVerify(password, user.Password, HashType.SHA512))
                    return user;
            }
            return null;
        }

        /// <summary>
        /// Get User by Username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<User> GetUser(string username)
        {
            return await _repository.GetUserByUsernameAsync(username);
        }
    }
}
